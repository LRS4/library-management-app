using ILS.Library.Web.Models.Catalog;
using ILS.Library.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace ILS.Library.Web.Controllers
{
    public class CatalogController : Controller
    {
        #region Private properties

        private ILibraryAsset _libraryAssetService;

        #endregion

        #region Constructor

        public CatalogController(
            ILibraryAsset libraryAssetService)
        {
            _libraryAssetService = libraryAssetService;
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

        #endregion

        #region Private methods
        #endregion
    }
}
