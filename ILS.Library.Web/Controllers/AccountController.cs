using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ILS.Library.DataAccess.SecurityDb.Entities.Branch;
using ILS.Library.DataAccess.SecurityDb.Entities.Users;
using ILS.Library.Web.Models.Account;
using ILS.Library.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ILS.Library.Web.Controllers
{
    public class AccountController : Controller
    {
        #region Private properties

        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IBranchService _branchService;

        #endregion

        #region Constructor

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IBranchService branchService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _branchService = branchService;
        }

        #endregion

        #region Route actions

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var libraryCard = new LibraryCard
                {
                    Created = DateTime.Now,
                    Fees = 0
                };

                _branchService.AddLibraryCard(libraryCard);

                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Address = model.Address,
                    DateOfBirth = model.DateOfBirth,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Gender = model.Gender,
                    TelephoneNumber = model.TelephoneNumber,
                    HomeLibraryBranch = _branchService.Get(model.Branch),
                    LibraryCard = libraryCard
                };

                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password,
                                                                     model.RememberMe, false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            
            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email {email} is already in use");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
        #endregion
    }
}