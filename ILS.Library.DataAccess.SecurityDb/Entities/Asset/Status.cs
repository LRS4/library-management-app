using System;
using System.Collections.Generic;

namespace ILS.Library.DataAccess.SecurityDb.Entities.Asset
{
    public partial class Status
    {
        public Status()
        {
            LibraryAsset = new HashSet<LibraryAsset>();
        }

        public int StatusId { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }

        public virtual ICollection<LibraryAsset> LibraryAsset { get; set; }
    }
}
