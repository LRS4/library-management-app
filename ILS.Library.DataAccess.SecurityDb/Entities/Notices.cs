using System;
using System.Collections.Generic;

namespace ILS.Library.DataAccess.SecurityDb.Entities
{
    public partial class Notices
    {
        public int NoticeId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTimeOffset ValidFrom { get; set; }
        public DateTimeOffset ValidTo { get; set; }
    }
}
