using ILS.Library.DataAccess.SecurityDb.Entities.Comms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ILS.Library.Web.Services.Interfaces
{
    public interface ICommsService
    {
        IEnumerable<Notices> GetAllValidNotices();

        void Add(Notices newNotice);
    }
}
