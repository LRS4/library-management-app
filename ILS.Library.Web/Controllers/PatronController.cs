using ILS.Library.DataAccess.SecurityDb.Entities.Asset;
using ILS.Library.DataAccess.SecurityDb.Entities.Users;
using ILS.Library.Web.Models.Patron;
using ILS.Library.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ILS.Library.Web.Controllers
{
    public class PatronController : Controller
    {
        #region Private properties
        private readonly IPatronService _patronService;

        #endregion

        #region Constructor
        public PatronController(IPatronService patronService)
        {
            _patronService = patronService;
        }

        #endregion

        #region Routes
        public IActionResult Index()
        {
            var allPatrons = _patronService.GetAll();

            var patronModel = allPatrons.Select(p => new PatronDetailModel
            {
                Id = p.PatronId,
                FirstName = p.FirstName,
                LastName = p.LastName,
                LibraryCardId = p.LibraryCardId,
                OverdueFees = Math.Round(p.LibraryCard.Fees, 2),
                HomeLibraryBranch = p.HomeLibraryBranch.Name
            }).ToList();

            var model = new IndexViewModel()
            {
                Patrons = patronModel
            };

            return View(model);
        }

        public IActionResult Detail(int patronId)
        {
            var patron = _patronService.Get(patronId);

            var model = new PatronDetailModel()
            {
                FirstName = patron.FirstName,
                LastName = patron.LastName,
                Address = patron.Address,
                HomeLibraryBranch = patron.HomeLibraryBranch.Name,
                MemberSince = patron.LibraryCard.Created,
                OverdueFees = Math.Round(patron.LibraryCard.Fees, 2),
                LibraryCardId = patron.LibraryCard.LibraryCardId,
                Telephone = patron.TelephoneNumber,
                AssetsCheckedOut = _patronService.GetCheckouts(patronId).ToList() ?? new List<Checkout>(),
                CheckoutHistory = _patronService.GetCheckoutHistory(patronId),
                Holds = _patronService.GetHolds(patronId)
            };

            return View(model);
        }

        #endregion

        #region Private methods

        #endregion
    }
}
