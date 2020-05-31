using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ILS.Library.Web.Models.Catalog
{
    public class AssetModel
    {
        [Required]
        public string Title { get; set; }
        public string AuthorOrDirector { get; set; }
        public int Discriminator { get; set; }
        public int Year { get; set; }
        public string ISBN { get; set; }
        public string DeweyCallNumber { get; set; }
        public decimal Cost { get; set; }
        public int CurrentLocation { get; set; }
        public string ImageUrl { get; set; }
        public int NumberOfCopies { get; set; }
    }
}
