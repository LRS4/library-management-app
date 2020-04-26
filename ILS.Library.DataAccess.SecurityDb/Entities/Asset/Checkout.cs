using System;
using System.Collections.Generic;

namespace ILS.Library.DataAccess.SecurityDb.Entities.Asset
{
    public partial class Checkout
    {
        public int CheckoutId { get; set; }
        public int LibraryAssetId { get; set; }
        public int? LibraryCardId { get; set; }
        public DateTime? Since { get; set; }
        public DateTime? Until { get; set; }

        public virtual LibraryAsset LibraryAsset { get; set; }
        public virtual LibraryCard LibraryCard { get; set; }
    }
}
