using DiplomMetod.Data.Entites;
using DiplomMetod.Models;
using DiplomMetod.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DiplomMetod.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel() { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(login.UserName, login.Password, login.RememberMe, false);
                if (result.Succeeded)
                {
                    if (login.ReturnUrl != null)
                    {
                        return Redirect(login.ReturnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Неверный логин или пароль");
                }

            }
            return View(login);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register(string returnUrl)
        {
            return View(new RegisterUserViewModel() { ReturnUrl = returnUrl });
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserViewModel register)
        {
            if (register.Password == register.Email)
            {
                ModelState.AddModelError("Name", "Имя и пароль не должны совпадать");
            }

            if (ModelState.IsValid)
            {
                var user = new User
                {
                    FirstName = register.FirstName,
                    LastName = register.LastName,
                    UserName = register.Email,
                    Email = register.Email,
                };

                var result = await _userManager.CreateAsync(user, register.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Const.UserRoleName);
                    await _signInManager.SignInAsync(user, false);

                    if (register.ReturnUrl != null)
                    {
                        return Redirect(register.ReturnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Email", "Пользователь с таким email уже зарегистрирован");
                }


            }
            return View(register);
        }


        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userProfileViewModel = user.ToUserProfileViewModel();
            
            return View(userProfileViewModel);
        }

        [Authorize]
        public async Task<IActionResult> EditProfile()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userProfileViewModel = user.ToUserProfileViewModel();
            
            return View(userProfileViewModel);
        }

        //[Authorize]
        //[HttpPost]
        //public async Task<IActionResult> EditProfile()
        //{


        //    return View();

        //}
    }
}
