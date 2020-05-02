using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ILS.Library.DataAccess.SecurityDb.Entities.Branch;

namespace ILS.Library.DataAccess.SecurityDb.Entities.Asset
{
    [Table("LibraryAsset", Schema = "Asset")]
    public partial class LibraryAsset
    {
        public LibraryAsset()
        {
            Checkout = new HashSet<Checkout>();
            CheckoutHistory = new HashSet<CheckoutHistory>();
            Hold = new HashSet<Hold>();
        }

        [Column("LibraryAssetId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LibraryAssetId { get; set; }

        [Column("Discriminator")]
        public string Discriminator { get; set; }

        [Column("Title")]
        public string Title { get; set; }

        [Column("Author")]
        public string Author { get; set; }

        [Column("DeweyIndex")]
        public string DeweyIndex { get; set; }

        [Column("ISBN")]
        public string ISBN { get; set; }

        [Column("Year")]
        public int Year { get; set; }
        public decimal Cost { get; set; }
        public string ImageUrl { get; set; }
        public int NumberOfCopies { get; set; }
        public string Director { get; set; }
        public int? LibraryCardId { get; set; }
        public int? LocationId { get; set; }
        public int StatusId { get; set; }

        [ForeignKey("LibraryCardId")]
        public virtual LibraryCard LibraryCard { get; set; }

        [ForeignKey("LocationId")]
        public virtual BranchDetails Location { get; set; }

        [ForeignKey("StatusId")]
        public virtual Status Status { get; set; }
        public virtual ICollection<Checkout> Checkout { get; set; }
        public virtual ICollection<CheckoutHistory> CheckoutHistory { get; set; }
        public virtual ICollection<Hold> Hold { get; set; }
    }
}
