using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Compras.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Compras.Controllers
{
    public class ContaController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public ContaController (UserManager<IdentityUser> userManager,
                                SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        public IActionResult Login( string returnUrl)
        {
            return View(new LoginViewModel()
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }

            var user = await _userManager.FindByNameAsync(loginVM.UserName);
            if (user != null)
            {
                var password = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                if (password.Succeeded)
                {
                    if (string.IsNullOrEmpty(loginVM.ReturnUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        this.ModelState.AddModelError("Login", "Falha ao realizar o login, verifique o usuário/senha usados");
                    }
                    return Redirect(loginVM.ReturnUrl);
                }
            }
            ModelState.AddModelError("", "Usuário ou senha não cadastrado");
            return View(loginVM);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(LoginViewModel registroVM)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser() {UserName = registroVM.UserName};
                var password = await _userManager.CreateAsync(user, registroVM.Password);
                if (password.Succeeded)
                {
                    return RedirectToAction("LoggedIn", "Conta");
                }

            }
            return View(registroVM);
        }

        [AllowAnonymous]
        public ViewResult LoggedIn() => View();

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
