using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ILS.Library.Web.Models.Home
{
    public class IndexViewModel
    {
        public IEnumerable<NoticesModel> Notices { get; set; }
    }
}
