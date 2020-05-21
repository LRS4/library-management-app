using ILS.Library.DataAccess.SecurityDb.Entities.Comms;
using ILS.Library.Web.Models.Home;
using ILS.Library.Web.Models.Notices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ILS.Library.Web.Services.Interfaces
{
    public interface ICommsService
    {
        IEnumerable<Notices> GetAllNotices();

        IEnumerable<Notices> GetAllValidNotices();

        Notices GetNoticeById(int noticeId);

        void Add(Notices newNotice);

        void Edit(int noticeId, NoticeModel updatedNotice);

        void Remove(int noticeId);
    }
}
