using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ILS.Library.Web.Models.Branch
{
    public class IndexViewModel
    {
        public IEnumerable<BranchDetailModel> Branches { get; set; }
    }
}
