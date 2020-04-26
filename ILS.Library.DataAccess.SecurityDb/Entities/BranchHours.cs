using System;
using System.Collections.Generic;

namespace ILS.Library.DataAccess.SecurityDb.Entities
{
    public partial class BranchHours
    {
        public int BranchHoursId { get; set; }
        public int? BranchId { get; set; }
        public int CloseTime { get; set; }
        public int DayOfWeek { get; set; }
        public int OpenTime { get; set; }

        public virtual BranchDetails Branch { get; set; }
    }
}
