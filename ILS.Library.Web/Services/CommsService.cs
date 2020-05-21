using ILS.Library.DataAccess.SecurityDb.Entities;
using ILS.Library.DataAccess.SecurityDb.Entities.Comms;
using ILS.Library.Web.Models.Notices;
using ILS.Library.Web.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ILS.Library.Web.Services
{
    public class CommsService : ICommsService
    {
        #region Private properties
        private readonly ILSContext _context;
        #endregion

        #region Constructor
        public CommsService(ILSContext context)
        {
            _context = context;
        }
        #endregion

        #region Comms Service
        public void Add(Notices newNotice)
        {
            _context.Add(newNotice);
            _context.SaveChanges();
        }

        public void Edit(int noticeId, NoticeModel updatedNotice)
        {
            var notice = GetNoticeById(noticeId);

            notice.NoticeId = updatedNotice.Id;
            notice.Title = updatedNotice.Title;
            notice.Content = updatedNotice.Content;
            notice.ValidFrom = updatedNotice.ValidFrom;
            notice.ValidTo = updatedNotice.ValidTo;

            _context.SaveChanges();
        }

        public IEnumerable<Notices> GetAllNotices()
        {
            return _context.Notices;
        }

        public IEnumerable<Notices> GetAllValidNotices()
        {
            var now = DateTime.Now;
            return _context.Notices
                .Where(notice => notice.ValidTo > now && notice.ValidFrom < now);
        }

        public Notices GetNoticeById(int noticeId)
        {
            return _context.Notices
                .FirstOrDefault(notice => notice.NoticeId == noticeId);
        }

        public void Remove(int noticeId)
        {
            var notice = _context.Notices
                .FirstOrDefault(notice => notice.NoticeId == noticeId);

            _context.Remove(notice);
            _context.SaveChanges();
        }
        #endregion
    }
}
