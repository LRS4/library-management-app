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
                OverdueFees = p.LibraryCard.Fees,
                HomeLibraryBranch = p.HomeLibraryBranch.Name
            }).ToList();

            var model = new IndexViewModel()
            {
                Patrons = patronModel
            };

            return View(model);
        }

        #endregion

        #region Private methods

        #endregion
    }
}
