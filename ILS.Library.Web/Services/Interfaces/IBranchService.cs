using ILS.Library.DataAccess.SecurityDb.Entities.Asset;
using ILS.Library.DataAccess.SecurityDb.Entities.Branch;
using ILS.Library.DataAccess.SecurityDb.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ILS.Library.Web.Services.Interfaces
{
    public interface IBranchService
    {
        IEnumerable<BranchDetails> GetAll();
        IEnumerable<Patron> GetPatrons(int branchId);
        IEnumerable<LibraryAsset> GetAssets(int branchid);
        IEnumerable<string> GetBranchHours(int branchId);
        BranchDetails Get(int branchId);
        void Add(BranchDetails newBranch);
        void AddLibraryCard(LibraryCard newCard);
        bool IsBranchOpen(int branchId);

    }
}
