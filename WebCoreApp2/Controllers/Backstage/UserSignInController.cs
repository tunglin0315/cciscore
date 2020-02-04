using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CCISWebCore.Models;
using Microsoft.AspNetCore.Identity;

namespace CCISWebCore.Controllers.Backstage
{

    public class UserSignInController : Controller
    {
        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;
        public UserSignInController(UserManager<AppUser>
          userMgr, SignInManager<AppUser> signinMgr)
        {
            userManager = userMgr;
            signInManager = signinMgr;
        }
        // 預設登入網址
        //public IActionResult TobeSignIn(string returnUrl)
        //{
        //    ViewBag.returnUrl = returnUrl;
        //    return View();
        //}
        public IActionResult TobeSignIn()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TobeSignIn(LoginModel details, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await userManager.FindByNameAsync(details.Name);
                if (user != null)
                {
                    await signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result =
                        await signInManager.PasswordSignInAsync(user, details.IsAPwd, false, false);
                    if (result.Succeeded)
                    {
                        return Redirect(returnUrl ?? await RoleDefaultUrl(details.Name));
                    }
                }
                ModelState.AddModelError(nameof(LoginModel.Name)
                    , "Invalid user or password");
            }
            return View(details);
        }
        // 預設角色頁面
        private async Task<string> RoleDefaultUrl(string UserName)
        {
            var user = await userManager.FindByNameAsync(UserName);
            var role = await userManager.GetRolesAsync(user);
            // 指定角色成功登入後 預設的頁面
            //   if (role.Contains("Admin") && role.Contains("Reader")) { return Url.Content("~/admin"); }
            return Url.Content("/");
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "Home");
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}