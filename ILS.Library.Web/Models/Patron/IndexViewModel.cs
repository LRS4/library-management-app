using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ILS.Library.Web.Models.Patron
{
    public class IndexViewModel
    {
        public IEnumerable<PatronDetailModel> Patrons { get; set; }
    }
}
