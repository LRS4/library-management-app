using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace ILS.Library.DataAccess.SecurityDb.Entities.Branch
{
    [Table("BranchHours", Schema="Branch")]
    public partial class BranchHours
    {
        [Column("BranchHoursId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BranchHoursId { get; set; }
        public int? BranchId { get; set; }
        public int CloseTime { get; set; }
        public int DayOfWeek { get; set; }
        public int OpenTime { get; set; }

        public virtual BranchDetails Branch { get; set; }
    }
}
