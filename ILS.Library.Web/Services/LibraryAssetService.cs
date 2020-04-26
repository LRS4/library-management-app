using ILS.Library.DataAccess.SecurityDb.Entities;
using ILS.Library.DataAccess.SecurityDb.Entities.Asset;
using ILS.Library.DataAccess.SecurityDb.Entities.Branch;
using ILS.Library.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ILS.Library.Web.Services
{
    public class LibraryAssetService : ILibraryAsset
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
            throw new NotImplementedException();
        }

        public string GetAuthorOrDirector(int id)
        {
            throw new NotImplementedException();
        }

        public LibraryAsset GetById(int id)
        {
            throw new NotImplementedException();
        }

        public BranchDetails GetCurrentLocation(int id)
        {
            throw new NotImplementedException();
        }

        public string GetDeweyIndex(int id)
        {
            throw new NotImplementedException();
        }

        public string GetISBN(int id)
        {
            throw new NotImplementedException();
        }

        public string GetTitle(int id)
        {
            throw new NotImplementedException();
        }

        public string GetType(int id)
        {
            throw new NotImplementedException();
        }
    }
    #endregion
}
