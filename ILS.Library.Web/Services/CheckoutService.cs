using ILS.Library.DataAccess.SecurityDb.Entities;
using ILS.Library.DataAccess.SecurityDb.Entities.Asset;
using ILS.Library.DataAccess.SecurityDb.Entities.Users;
using ILS.Library.Web.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ILS.Library.Web.Services
{
    public class CheckoutService : ICheckoutService
    {
        #region Private properties
        private readonly ILSContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        #endregion

        #region Constructor
        public CheckoutService(ILSContext context,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }


        #endregion

        #region CheckoutService
        public void Add(Checkout newCheckout)
        {
            _context.Add(newCheckout);
            _context.SaveChanges();
        }

        public IEnumerable<Checkout> GetAll()
        {
            return _context.Checkout;
        }

        public Checkout GetById(int checkoutId)
        {
            return GetAll()
                .FirstOrDefault(checkout => checkout.CheckoutId == checkoutId);
        }

        public Checkout GetLatestCheckout(int assetId)
        {
            return _context.Checkout
                .Where(c => c.LibraryAssetId == assetId)
                .OrderByDescending(c => c.Since)
                .FirstOrDefault();
        }

        public IEnumerable<CheckoutHistory> GetCheckoutHistory(int id)
        {
            return _context.CheckoutHistory
                .Include(h => h.LibraryAsset)
                .Include(h => h.LibraryCard)
                .Where(h => h.LibraryAsset.LibraryAssetId == id);
        }

        public string GetCurrentHoldPatronName(int holdId)
        {
            var hold = _context.Hold
                .Include(h => h.LibraryAsset)
                .Include(h => h.LibraryCard)
                .FirstOrDefault(h => h.HoldId == holdId);

            // ? is null conditional
            var cardId = hold?.LibraryCard.LibraryCardId;

            var patron = _context.ApplicationUser
                .Include(p => p.LibraryCard)
                .FirstOrDefault(p => p.LibraryCard.LibraryCardId == cardId);

            return $"{patron?.FirstName} {patron?.LastName}";   
        }

        public string GetCurrentCheckoutPatron(int assetId)
        {
            var checkout = GetCheckoutByAssetId(assetId);
            if (checkout == null)
            {
                return "Not checked out.";
            }

            var cardId = checkout.LibraryCard.LibraryCardId;

            var patron = _context.ApplicationUser
                .Include(p => p.LibraryCard)
                .FirstOrDefault(p => p.LibraryCardId == cardId);

            return $"{patron.FirstName} {patron.LastName}";
          

        }

        public DateTime GetCurrentHoldPlaced(int holdId)
        {
            return _context.Hold
                .Include(h => h.LibraryAsset)
                .Include(h => h.LibraryCard)
                .FirstOrDefault(h => h.HoldId == holdId)
                .HoldPlaced;
        }

        public IEnumerable<Hold> GetCurrentHolds(int id)
        {
            return _context.Hold
                .Include(h => h.LibraryAsset)
                .Where(h => h.LibraryAssetId == id);
        }

        public void MarkFound(int assetId)
        {
            var now = DateTime.Now;

            UpdateAssetStatus(assetId, "Available");

            RemoveExistingCheckouts(assetId);

            CloseExistingCheckoutHistory(assetId, now);

            _context.SaveChanges();
        }

        public void MarkLost(int assetId)
        {
            UpdateAssetStatus(assetId, "Lost");

            _context.SaveChanges();
        }

        public void PlaceHold(int assetId, int libraryCardId)
        {
            var now = DateTime.Now;

            var asset = _context.LibraryAsset
                .Include(a => a.Status)
                .FirstOrDefault(a => a.LibraryAssetId == assetId);

            var card = _context.LibraryCard
                .FirstOrDefault(c => c.LibraryCardId == libraryCardId);

            if (asset.Status.Name == "Available")
            {
                UpdateAssetStatus(assetId, "On Hold");
            }

            var hold = new Hold
            {
                HoldPlaced = now,
                LibraryAsset = asset,
                LibraryCard = card
            };

            _context.Add(hold);
            _context.SaveChanges();
        }

        public void CheckInItem(int assetId)
        {
            var now = DateTime.Now;

            var item = _context.LibraryAsset
                .FirstOrDefault(a => a.LibraryAssetId == assetId);

            // remove any existing checkouts
            RemoveExistingCheckouts(assetId);

            // close any existing checkout history
            CloseExistingCheckoutHistory(assetId, now);

            // look for existing holds
            var currentHolds = _context.Hold
                .Include(h => h.LibraryAsset)
                .Include(h => h.LibraryCard)
                .Where(h => h.LibraryAssetId == assetId);

            // if there are holds, checkout the item to the librarycard with the earliest hold
            if (currentHolds.Any())
            {
                CheckoutToEarliestHold(assetId, currentHolds);
                return;
            }

            // otherwise, update the item status to available
            UpdateAssetStatus(assetId, "Available");

            _context.SaveChanges();
        }

        public void CheckOutItem(int assetId, int libraryCardId)
        {
            if (IsCheckedOut(assetId))
            {
                return;
                // add logic to handle feedback to user
            }

            var item = _context.LibraryAsset
                .FirstOrDefault(a => a.LibraryAssetId == assetId);

            UpdateAssetStatus(assetId, "Checked Out");

            var libraryCard = _context.LibraryCard
                .Include(card => card.Checkout)
                .FirstOrDefault(card => card.LibraryCardId == libraryCardId);

            var now = DateTime.Now;

            var checkout = new Checkout
            {
                LibraryAsset = item,
                LibraryCard = libraryCard,
                Since = now,
                Until = GetDefaultCheckoutTime(now)
            };

            _context.Add(checkout);

            var checkoutHistory = new CheckoutHistory
            {
                CheckedOut = now,
                LibraryAsset = item,
                LibraryCard = libraryCard
            };

            _context.Add(checkoutHistory);

            _context.SaveChanges();
        }

        public bool IsCheckedOut(int assetId)
        {
            return _context.Checkout
                .Where(co => co.LibraryAssetId == assetId)
                .Any();
        }

        public bool CheckProvidedLibraryId(int libraryCardId)
        {
            var currentUser = _httpContextAccessor.HttpContext.User.Identity.Name;
            var confirmedCardId = _context.ApplicationUser
                .FirstOrDefault(u => u.UserName == currentUser).LibraryCardId;

            return confirmedCardId == libraryCardId;
        }


        #endregion

        #region Private Methods

        private DateTime? GetDefaultCheckoutTime(DateTime now)
        {
            return now.AddDays(30);
        }

        private void CheckoutToEarliestHold(int assetId, IQueryable<Hold> currentHolds)
        {
            var earliestHold = currentHolds
                .OrderBy(holds => holds.HoldPlaced)
                .FirstOrDefault();

            var card = earliestHold.LibraryCard;

            _context.Remove(earliestHold);
            _context.SaveChanges();

            CheckOutItem(assetId, card.LibraryCardId);
        }

        private void UpdateAssetStatus(int assetId, string newStatus)
        {
            var item = _context.LibraryAsset
                .FirstOrDefault(a => a.LibraryAssetId == assetId);

            _context.Update(item);

            item.Status = _context.Status
                .FirstOrDefault(status => status.Name == newStatus);
        }

        private void CloseExistingCheckoutHistory(int assetId, DateTime now)
        {
            var history = _context.CheckoutHistory
                .FirstOrDefault(h => h.LibraryAssetId == assetId
                    && h.CheckedIn == null);

            if (history != null)
            {
                _context.Update(history);
                history.CheckedIn = now;
            }
        }

        private void RemoveExistingCheckouts(int assetId)
        {
            // remove any existing checkouts on the item
            var checkout = _context.Checkout
                .FirstOrDefault(co => co.LibraryAssetId == assetId);

            if (checkout != null)
            {
                _context.Remove(checkout);
            }
        }

        private Checkout GetCheckoutByAssetId(int assetId)
        {
            return _context.Checkout
                .Include(co => co.LibraryAsset)
                .Include(co => co.LibraryCard)
                .FirstOrDefault(co => co.LibraryAssetId == assetId);
        }


        #endregion
    }
}
