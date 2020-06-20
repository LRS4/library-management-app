using ILS.Library.DataAccess.SecurityDb.Entities.Branch;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ILS.Library.DataAccess.SecurityDb.Entities.Users
{
    public class ApplicationUser : IdentityUser
    {
        public string Address { get; set; }
        public string DateOfBirth { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string TelephoneNumber { get; set; }
        public int HomeLibraryBranchId { get; set; }
        public int LibraryCardId { get; set; }

        public virtual BranchDetails HomeLibraryBranch { get; set; }
        public virtual LibraryCard LibraryCard { get; set; }
    }
}
