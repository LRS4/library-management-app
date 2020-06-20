using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ILS.Library.Web.Models;
using ILS.Library.Web.Services.Interfaces;
using ILS.Library.Web.Services;
using ILS.Library.Web.Models.Home;

namespace ILS.Library.Web.Controllers
{
    public class HomeController : Controller
    {
        #region Private properties
        private readonly ILogger<HomeController> _logger;
        private readonly ICommsService _commsService;
        #endregion

        #region Constructor
        public HomeController(
            ILogger<HomeController> logger,
            ICommsService commsService)
        {
            _logger = logger;
            _commsService = commsService;
        }
        #endregion

        #region Routes
        public IActionResult Index()
        {
            var notices = BuildNoticesModel();
            var model = new IndexViewModel
            {
                Notices = notices
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("/Home/Error/{statusCode}")]
        public IActionResult Error(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry, the resource you requested could not be found";
                    break;
            }

            return View();
        }
        #endregion

        #region Private methods
        private IEnumerable<NoticesModel> BuildNoticesModel()
        {
            return _commsService.GetAllValidNotices().ToList()
                .Select(notice => new NoticesModel()
                {
                    Title = notice.Title,
                    Content = notice.Content,
                    ValidFrom = notice.ValidFrom,
                    ValidTo = notice.ValidTo
                });
        }
        #endregion
    }
}
