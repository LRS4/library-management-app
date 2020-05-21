using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ILS.Library.DataAccess.SecurityDb.Entities.Comms;
using ILS.Library.Web.Models.Notices;
using ILS.Library.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ILS.Library.Web.Controllers
{
    public class NoticeController : Controller
    {
        #region Private properties
        private readonly ICommsService _commsService;
        #endregion

        #region Constructor
        public NoticeController(ICommsService commsService)
        {
            _commsService = commsService;
        }
        #endregion

        #region Route actions
        public IActionResult Index()
        {
            var notices = _commsService.GetAllNotices().ToList()
                .Select(notice => new NoticeModel
                {
                    Id = notice.NoticeId,
                    Title = notice.Title,
                    Content = notice.Content,
                    ValidFrom = notice.ValidFrom,
                    ValidTo = notice.ValidTo
                });

            var model = new IndexViewModel { Notices = notices };
            
            return View(model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(NoticeModel notice)
        {
            var newNotice = new Notices
            {
                Title = notice.Title,
                Content = notice.Content,
                ValidFrom = notice.ValidFrom,
                ValidTo = notice.ValidTo
            };

            _commsService.Add(newNotice);

            return RedirectToAction("Index");
        }

        [HttpGet("Notice/Edit/{noticeId}")]
        public IActionResult Edit(int noticeId)
        {
            var notice = _commsService.GetNoticeById(noticeId);
            var model = new NoticeModel
            {
                Id = notice.NoticeId,
                Title = notice.Title,
                Content = notice.Content,
                ValidFrom = notice.ValidFrom,
                ValidTo = notice.ValidTo
            };    

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(NoticeModel updatedNotice)
        {
            var noticeId = updatedNotice.Id;
            _commsService.Edit(noticeId, updatedNotice);
            return RedirectToAction("Index");
        }

        [HttpGet("Notice/Delete/{noticeId}")]
        public IActionResult Delete(int noticeId)
        {
            _commsService.Remove(noticeId);
            return RedirectToAction("Index");
        }
        #endregion
    }
}