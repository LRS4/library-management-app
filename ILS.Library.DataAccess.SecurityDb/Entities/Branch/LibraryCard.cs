using System;
using System.Collections.Generic;

namespace ILS.Library.DataAccess.SecurityDb.Entities.Branch
{
    public partial class LibraryCard
    {
        public LibraryCard()
        {
            Checkout = new HashSet<Checkout>();
            CheckoutHistory = new HashSet<CheckoutHistory>();
            Hold = new HashSet<Hold>();
            LibraryAsset = new HashSet<LibraryAsset>();
            Patron = new HashSet<Patron>();
        }

        public int LibraryCardId { get; set; }
        public DateTime Created { get; set; }
        public decimal Fees { get; set; }

        public virtual ICollection<Checkout> Checkout { get; set; }
        public virtual ICollection<CheckoutHistory> CheckoutHistory { get; set; }
        public virtual ICollection<Hold> Hold { get; set; }
        public virtual ICollection<LibraryAsset> LibraryAsset { get; set; }
        public virtual ICollection<Patron> Patron { get; set; }
    }
}
