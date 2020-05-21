using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ILS.Library.Web.Models.Notices
{
    public class IndexViewModel
    {
        public IEnumerable<NoticeModel> Notices { get; set; }
    }
}
