using ILS.Library.Web.Models.Branch;
using ILS.Library.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Globalization;
using System.Linq;

namespace ILS.Library.Web.Controllers
{
    public class BranchController : Controller
    {
        #region Private properties
        private readonly IBranchService _branchService;

        #endregion

        #region Constructor
        public BranchController(IBranchService branchService)
        {
            _branchService = branchService;
        }
        #endregion

        #region Routes
        public IActionResult Index()
        {
            var branches = _branchService.GetAll().ToList()
                .Select(branch => new BranchDetailModel
                {
                    Id = branch.BranchId,
                    Name = branch.Name,
                    IsOpen = _branchService.IsBranchOpen(branch.BranchId),
                    NumberOfAssets = _branchService.GetAssets(branch.BranchId).Count(),
                    NumberOfPatrons = _branchService.GetPatrons(branch.BranchId).Count()
                });

            var model = new IndexViewModel()
            {
                Branches = branches
            };

            return View(model);
        }

        public IActionResult Detail(int branchId)
        {
            var branch = _branchService.Get(branchId);

            var model = new BranchDetailModel
            {
                Id = branch.BranchId,
                Name = branch.Name,
                Address = branch.Address,
                Telephone = branch.Telephone,
                OpenDate = branch.OpenDate.Value.ToString("dd-MM-yyyy"),
                NumberOfAssets = _branchService.GetAssets(branch.BranchId).Count(),
                NumberOfPatrons = _branchService.GetPatrons(branch.BranchId).Count(),
                TotalAssetValue = Math.Round(_branchService.GetAssets(branchId).Sum(a => a.Cost), 2),
                ImageUrl = branch.ImageUrl,
                HoursOpen = _branchService.GetBranchHours(branchId)
            };

            return View(model);
        }

        #endregion

        #region Private methods

        #endregion
    }
}