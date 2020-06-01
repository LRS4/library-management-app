using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ILS.Library.Web.Models.Catalog
{
    public class AssetModel
    {
        public int AssetId { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Author or Director is required.")]
        [Display(Name = "Author or Director")]
        public string AuthorOrDirector { get; set; }

        [Required(ErrorMessage = "Type is required.")]
        [Display(Name = "Type")]
        public int Discriminator { get; set; }

        [Required(ErrorMessage = "Year is required.")]
        public int Year { get; set; }

        [Required(ErrorMessage = "ISBN is required.")]
        [StringLength(13)]
        public string ISBN { get; set; }

        [Required(ErrorMessage = "Dewey Index is required.")]
        [Display(Name = "Dewey Index Number")]
        public string DeweyCallNumber { get; set; }

        [Required(ErrorMessage = "Cost is required.")]
        [Range(0.00, 150.00)]
        public decimal Cost { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        [Display(Name = "Branch")]
        public int CurrentLocation { get; set; }

        [Required(ErrorMessage = "Image link is required.")]
        [Display(Name = "Image Link")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Number of copies is required.")]
        [Display(Name = "Number Of Copies")]
        public int NumberOfCopies { get; set; }
    }
}
