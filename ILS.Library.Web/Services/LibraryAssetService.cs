using ILS.Library.DataAccess.SecurityDb.Entities;
using ILS.Library.DataAccess.SecurityDb.Entities.Asset;
using ILS.Library.DataAccess.SecurityDb.Entities.Branch;
using ILS.Library.Web.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace ILS.Library.Web.Services
{
    public class LibraryAssetService : ILibraryAssetService
    {

        #region Private Properties
        private readonly ILSContext _context;
        #endregion

        #region Constructor
        public LibraryAssetService(ILSContext context)
        {
            _context = context;
        }
        #endregion

        #region ILibraryAssetService
        /// <summary>
        /// Adds a new library asset
        /// </summary>
        /// <param name="newAsset">The new library asset to be added</param>
        public void Add(LibraryAsset newAsset)
        {
            _context.Add(newAsset);
            _context.SaveChanges();
        }

        public IEnumerable<LibraryAsset> GetAll()
        {
            return _context.LibraryAsset
                .Include(asset => asset.Status)
                .Include(asset => asset.Location);
        }

        public LibraryAsset GetById(int id)
        {
            return _context.LibraryAsset
                .Include(asset => asset.Status)
                .Include(asset => asset.Location)
                .FirstOrDefault(asset => asset.LibraryAssetId == id);
        }

        public BranchDetails GetCurrentLocation(int id)
        {
            return GetById(id).Location;
            // return _context.LibraryAsset.FirstOrDefault(asset => asset.LibraryAssetId == id).Location;  
        }

        public string GetDeweyIndex(int id)
        {
            var asset = _context.LibraryAsset
                .FirstOrDefault(asset => asset.LibraryAssetId == id);
            return asset.Discriminator == "Book" ?
                asset.DeweyIndex :
                "";
        }

        public string GetISBN(int id)
        {
            var asset = _context.LibraryAsset
                .FirstOrDefault(asset => asset.LibraryAssetId == id);
            return asset.Discriminator == "Book" ?
                asset.ISBN :
                "";
        }

        public string GetTitle(int id)
        {
            return _context.LibraryAsset
                .FirstOrDefault(asset => asset.LibraryAssetId == id).Title;
        }

        public string GetType(int id)
        {
            var book = _context.LibraryAsset
                .Where(b => b.LibraryAssetId == id);

            return book.Any() ? "Book" : "Video";
        }

        public string GetAuthorOrDirector(int id)
        {
            var asset = _context.LibraryAsset
                .FirstOrDefault(asset => asset.LibraryAssetId == id);
            return asset.Discriminator == "Book" ?
                asset.Author :
                asset.Director;
        }

        public Status GetStatusById(int id)
        {
            return _context.Status
                .FirstOrDefault(s => s.StatusId == id);
        }
    }
    #endregion
}
