using System;

namespace ILS.Library.Web.Models.Home
{
    public class NoticesModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTimeOffset ValidFrom { get; set; }
        public DateTimeOffset ValidTo { get; set; }
    }
}