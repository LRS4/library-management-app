using ILS.Library.DataAccess.SecurityDb.Entities.Branch;
using System;
using System.Collections.Generic;

namespace ILS.Library.DataAccess.SecurityDb.Entities.Asset
{
    public partial class CheckoutHistory
    {
        public int CheckoutHistoryId { get; set; }
        public DateTime? CheckedIn { get; set; }
        public DateTime CheckedOut { get; set; }
        public int LibraryAssetId { get; set; }
        public int LibraryCardId { get; set; }

        public virtual LibraryAsset LibraryAsset { get; set; }
        public virtual LibraryCard LibraryCard { get; set; }
    }
}
