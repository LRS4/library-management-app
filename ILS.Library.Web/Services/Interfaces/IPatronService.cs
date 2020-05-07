using ILS.Library.DataAccess.SecurityDb.Entities.Asset;
using ILS.Library.DataAccess.SecurityDb.Entities.Users;
using System.Collections.Generic;

namespace ILS.Library.Web.Services.Interfaces
{
    public interface IPatronService
    {
        Patron Get(int id);
        IEnumerable<Patron> GetAll();
        void Add(Patron newPatron);
        IEnumerable<CheckoutHistory> GetCheckoutHistory(int patronId);
        IEnumerable<Hold> GetHolds(int patronId);
        IEnumerable<Checkout> GetCheckouts(int patronId);
    }
}
