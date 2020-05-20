using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ILS.Library.DataAccess.SecurityDb.Entities.Comms
{
    [Table("Notices", Schema = "Comms")]
    public partial class Notices
    {
        [Column("NoticeId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NoticeId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTimeOffset ValidFrom { get; set; }
        public DateTimeOffset ValidTo { get; set; }
    }
}
