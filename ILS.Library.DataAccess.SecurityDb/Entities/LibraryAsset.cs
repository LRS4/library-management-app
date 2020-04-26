using System;
using System.Collections.Generic;

namespace ILS.Library.DataAccess.SecurityDb.Entities
{
    public partial class LibraryAsset
    {
        public LibraryAsset()
        {
            Checkout = new HashSet<Checkout>();
            CheckoutHistory = new HashSet<CheckoutHistory>();
            Hold = new HashSet<Hold>();
        }

        public int LibraryAssetId { get; set; }
        public string Discriminator { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string DeweyIndex { get; set; }
        public string Isbn { get; set; }
        public int Year { get; set; }
        public decimal Cost { get; set; }
        public string ImageUrl { get; set; }
        public int NumberOfCopies { get; set; }
        public string Director { get; set; }
        public int? LibraryCardId { get; set; }
        public int? LocationId { get; set; }
        public int StatusId { get; set; }

        public virtual LibraryCard LibraryCard { get; set; }
        public virtual BranchDetails Location { get; set; }
        public virtual Status Status { get; set; }
        public virtual ICollection<Checkout> Checkout { get; set; }
        public virtual ICollection<CheckoutHistory> CheckoutHistory { get; set; }
        public virtual ICollection<Hold> Hold { get; set; }
    }
}
