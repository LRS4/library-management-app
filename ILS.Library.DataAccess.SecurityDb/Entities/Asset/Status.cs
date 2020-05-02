using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ILS.Library.DataAccess.SecurityDb.Entities.Asset
{
    [Table("Status", Schema = "Asset")]
    public partial class Status
    {
        public Status()
        {
            LibraryAsset = new HashSet<LibraryAsset>();
        }

        [Column("StatusId")]
        public int StatusId { get; set; }

        [Column("Description")]
        public string Description { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        public virtual ICollection<LibraryAsset> LibraryAsset { get; set; }
    }
}
