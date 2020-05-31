using ILS.Library.DataAccess.SecurityDb.Entities.Asset;
using ILS.Library.Web.Enums;
using ILS.Library.Web.Models.Catalog;
using ILS.Library.Web.Models.Checkouts;
using ILS.Library.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace ILS.Library.Web.Controllers
{
    public class CatalogController : Controller
    {
        #region Private properties

        private ILibraryAssetService _libraryAssetService;
        private ICheckoutService _checkoutService;
        private IBranchService _branchService;

        #endregion

        #region Constructor

        public CatalogController(
            ILibraryAssetService libraryAssetService,
            ICheckoutService checkoutService,
            IBranchService branchService)
        {
            _libraryAssetService = libraryAssetService;
            _checkoutService = checkoutService;
            _branchService = branchService;
        }

        #endregion

        #region Routes

        public IActionResult Index()
        {
            var assetModels = _libraryAssetService.GetAll().ToList();
            var listingResult = assetModels
                .Select(result => new AssetListModel
                {
                    Id = result.LibraryAssetId,
                    ImageURL = result.ImageUrl,
                    AuthorOrDirector = _libraryAssetService.GetAuthorOrDirector(result.LibraryAssetId),
                    DeweyCallNumber = _libraryAssetService.GetDeweyIndex(result.LibraryAssetId),
                    Title = result.Title,
                    Type = _libraryAssetService.GetType(result.LibraryAssetId)
                });

            var model = new IndexViewModel()
            {
                Assets = listingResult
            };

            return View(model);
        }

        public IActionResult Detail(int id)
        {
            var asset = _libraryAssetService.GetById(id);

            var currentHolds = _checkoutService.GetCurrentHolds(id).ToList()
                .Select(a => new AssetHoldModel
                {
                    HoldPlaced = _checkoutService.GetCurrentHoldPlaced(a.HoldId).ToString("d"),
                    PatronName = _checkoutService.GetCurrentHoldPatronName(a.HoldId)
                });

            var model = new AssetDetailModel
            {
                AssetId = id,
                Title = asset.Title,
                Year = asset.Year,
                Cost = Math.Round(asset.Cost, 2).ToString(),
                Status = asset.Status.Name,
                ImageUrl = asset.ImageUrl,
                AuthorOrDirector = _libraryAssetService.GetAuthorOrDirector(id),
                CurrentLocation = _libraryAssetService.GetCurrentLocation(id).Name,
                DeweyCallNumber = _libraryAssetService.GetDeweyIndex(id),
                ISBN = _libraryAssetService.GetISBN(id),
                CheckoutHistory = _checkoutService.GetCheckoutHistory(id),
                LatestCheckout = _checkoutService.GetLatestCheckout(id),
                PatronName = _checkoutService.GetCurrentCheckoutPatron(id),
                CurrentHolds = currentHolds 
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AssetModel asset)
        {
            var newAsset = createNewLibraryAsset(asset);
            _libraryAssetService.Add(newAsset);
            return RedirectToAction("Index");
        }

        public IActionResult Checkout(int id)
        {
            var asset = _libraryAssetService.GetById(id);

            var model = new CheckoutViewModel
            {
                AssetId = asset.LibraryAssetId,
                Title = asset.Title,
                ImageUrl = asset.ImageUrl,
                LibraryCardId = "",
                IsCheckedOut = _checkoutService.IsCheckedOut(id)
            };

            return View(model);
        }

        public IActionResult CheckIn(int id)
        {
            _checkoutService.CheckInItem(id);
            return RedirectToAction("Detail", new { id });
        }

        public IActionResult PlaceHold(int id)
        {
            var asset = _libraryAssetService.GetById(id);

            var model = new CheckoutViewModel
            {
                AssetId = asset.LibraryAssetId,
                Title = asset.Title,
                ImageUrl = asset.ImageUrl,
                LibraryCardId = "",
                IsCheckedOut = _checkoutService.IsCheckedOut(id),
                HoldCount = _checkoutService.GetCurrentHolds(id).Count()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult MarkLost(int assetId)
        {
            _checkoutService.MarkLost(assetId);
            return RedirectToAction("Detail", new { id = assetId });
        }

        [HttpPost]
        public IActionResult MarkFound(int assetId)
        {
            _checkoutService.MarkFound(assetId);
            return RedirectToAction("Detail", new { id = assetId });
        }

        [HttpPost]
        public IActionResult PlaceCheckout(int assetId, int libraryCardId)
        {
            _checkoutService.CheckOutItem(assetId, libraryCardId);
            return RedirectToAction("Detail", new { id = assetId });
        }

        [HttpPost]
        public IActionResult PlaceHold(int assetId, int libraryCardId)
        {
            _checkoutService.PlaceHold(assetId, libraryCardId);
            return RedirectToAction("Detail", new { id = assetId });
        }

        #endregion

        #region Private methods
        public LibraryAsset createNewLibraryAsset(AssetModel asset)
        {
            var discriminator = Enum.GetName(typeof(Discriminator), asset.Discriminator);
            var newAsset = new LibraryAsset
            {
                Title = asset.Title,
                Discriminator = discriminator,
                DeweyIndex = asset.DeweyCallNumber,
                NumberOfCopies = asset.NumberOfCopies,
                Year = asset.Year,
                ISBN = asset.ISBN,
                Cost = asset.Cost,
                ImageUrl = asset.ImageUrl,
                Location = _branchService.Get(asset.CurrentLocation),
                Status = _libraryAssetService.GetStatusById(2)
            };

            if (discriminator == "Book")
            {
                newAsset.Author = asset.AuthorOrDirector;
            }
            else
            {
                newAsset.Director = asset.AuthorOrDirector;
            }

            return newAsset;
        }

        #endregion
    }
}
