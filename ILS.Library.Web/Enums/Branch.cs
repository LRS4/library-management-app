using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ILS.Library.Web.Enums
{
    public enum Branch
    { 
        [Display(Name = "Lake Shore Branch")]
        LakeShore = 1,
        [Display(Name = "Mountain View Branch")]
        MountainView = 2,
        [Display(Name = "Pleasant Hill Branch")]
        PleasantHill = 3
    }
}
