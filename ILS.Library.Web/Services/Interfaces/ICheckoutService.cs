using ILS.Library.DataAccess.SecurityDb.Entities.Asset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ILS.Library.Web.Services.Interfaces
{
    public interface ICheckoutService
    {
        IEnumerable<Checkout> GetAll();
        IEnumerable<Hold> GetCurrentHolds(int id);
        IEnumerable<CheckoutHistory> GetCheckoutHistory(int id);

        Checkout GetById(int checkoutId);
        Checkout GetLatestCheckout(int assetId);

        DateTime GetCurrentHoldPlaced(int holdId);
        string GetCurrentCheckoutPatron(int assetId);
        string GetCurrentHoldPatronName(int holdId);
        bool IsCheckedOut(int assetId);

        void Add(Checkout newCheckout);
        void CheckOutItem(int assetId, int libraryCardId);
        void CheckInItem(int assetId);
        void MarkLost(int assetId);
        void MarkFound(int assetId);
        void PlaceHold(int assetId, int libraryCardId);
    }
}
