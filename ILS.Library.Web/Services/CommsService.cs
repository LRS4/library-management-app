using ILS.Library.DataAccess.SecurityDb.Entities;
using ILS.Library.DataAccess.SecurityDb.Entities.Comms;
using ILS.Library.Web.Services.Interfaces;
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

        public IEnumerable<Notices> GetAllNotices()
        {
            return _context.Notices;
        }
        #endregion
    }
}
