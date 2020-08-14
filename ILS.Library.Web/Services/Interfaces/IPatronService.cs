using ILS.Library.DataAccess.SecurityDb.Entities.Asset;
using ILS.Library.DataAccess.SecurityDb.Entities.Users;
using System.Collections.Generic;

namespace ILS.Library.Web.Services.Interfaces
{
    public interface IPatronService
    {
        ApplicationUser Get(string id);
        IEnumerable<ApplicationUser> GetAll();
        void Add(Patron newPatron);
        IEnumerable<CheckoutHistory> GetCheckoutHistory(string patronId);
        IEnumerable<Hold> GetHolds(string patronId);
        IEnumerable<Checkout> GetCheckouts(string patronId);
    }
}
