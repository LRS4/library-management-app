using System;
using System.Collections.Generic;
using ILS.Library.DataAccess.SecurityDb.Entities.Users;
using ILS.Library.DataAccess.SecurityDb.Entities.Asset;

namespace ILS.Library.DataAccess.SecurityDb.Entities.Branch
{
    public partial class BranchDetails
    {
        public BranchDetails()
        {
            BranchHours = new HashSet<BranchHours>();
            LibraryAsset = new HashSet<LibraryAsset>();
            Patron = new HashSet<Patron>();
        }

        public int BranchId { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public DateTime? OpenDate { get; set; }
        public string Telephone { get; set; }

        public virtual ICollection<BranchHours> BranchHours { get; set; }
        public virtual ICollection<LibraryAsset> LibraryAsset { get; set; }
        public virtual ICollection<Patron> Patron { get; set; }
    }
}
