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
            return View();
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

            return RedirectToAction("Add");
        }
        #endregion
    }
}