using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ILS.Library.Web.Models.Notices
{
    public class NoticeModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Title cannot exceed 50 characters.")]
        public string Title { get; set; }
        [Required]
        [MaxLength(150)]
        public string Content { get; set; }
        [Required]
        public DateTimeOffset ValidFrom { get; set; }
        [Required]
        public DateTimeOffset ValidTo { get; set; }
    }
}
