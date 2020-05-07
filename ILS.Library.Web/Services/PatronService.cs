using ILS.Library.DataAccess.SecurityDb.Entities;
using ILS.Library.DataAccess.SecurityDb.Entities.Asset;
using ILS.Library.DataAccess.SecurityDb.Entities.Users;
using ILS.Library.Web.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ILS.Library.Web.Services
{
    public class PatronService : IPatronService
    {

        #region Private properties
        private readonly ILSContext _context;

        #endregion

        #region Constructor
        public PatronService(ILSContext context)
        {
            _context = context;
        }

        #endregion

        #region PatronService

        public void Add(Patron newPatron)
        {
            _context.Add(newPatron);
            _context.SaveChanges();
        }

        public Patron Get(int id)
        {
            return _context.Patron
                .Include(patron => patron.LibraryCard)
                .Include(patron => patron.HomeLibraryBranch)
                .FirstOrDefault(patron => patron.PatronId == id);
        }

        public IEnumerable<Patron> GetAll()
        {
            return _context.Patron
                .Include(patron => patron.LibraryCard)
                .Include(patron => patron.HomeLibraryBranch);
        }

        public IEnumerable<CheckoutHistory> GetCheckoutHistory(int patronId)
        {
            var cardId = Get(patronId).LibraryCard.LibraryCardId;

            return _context.CheckoutHistory
                .Include(co => co.LibraryCard)
                .Include(co => co.LibraryAsset)
                .Where(co => co.LibraryCardId == cardId)
                .OrderByDescending(co => co.CheckedOut);
        }

        public IEnumerable<Checkout> GetCheckouts(int patronId)
        {
            var cardId = Get(patronId).LibraryCard.LibraryCardId;

            return _context.Checkout
                .Include(co => co.LibraryCard)
                .Include(co => co.LibraryAsset)
                .Where(co => co.LibraryCard.LibraryCardId == cardId);
        }

        public IEnumerable<Hold> GetHolds(int patronId)
        {
            var cardId = Get(patronId).LibraryCard.LibraryCardId;

            return _context.Hold
                .Include(h => h.LibraryCard)
                .Include(h => h.LibraryAsset)
                .Where(h => h.LibraryCard.LibraryCardId == cardId)
                .OrderByDescending(h => h.HoldPlaced);
        }

        #endregion

        #region Private methods

        #endregion
    }
}
