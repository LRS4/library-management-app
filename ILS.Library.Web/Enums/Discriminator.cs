using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;

namespace ILS.Library.Web.Enums
{
    public enum Discriminator
    {
        [Display(Name = "Book")]
        Book = 1,
        [Display(Name = "Video")]
        Video = 2
    }
}
