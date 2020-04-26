using System;
using System.Collections.Generic;

namespace ILS.Library.DataAccess.SecurityDb.Entities.Asset
{
    public partial class Hold
    {
        public int HoldId { get; set; }
        public DateTime HoldPlaced { get; set; }
        public int? LibraryAssetId { get; set; }
        public int? LibraryCardId { get; set; }

        public virtual LibraryAsset LibraryAsset { get; set; }
        public virtual LibraryCard LibraryCard { get; set; }
    }
}
