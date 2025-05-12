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
        [HttpGet]
        public async Task<IActionResult> Profile(string returnUrl)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userProfileViewModel = user.ToUserProfileViewModel();

            userProfileViewModel.ReturnUrl = returnUrl;
            
            return View(userProfileViewModel);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> EditProfile(string userId, string returnUrl)
        {
            var user = userId == null ? await _userManager.FindByNameAsync(User.Identity.Name) : await _userManager.FindByIdAsync(userId);
            
            var userProfileViewModel = user.ToUserProfileViewModel();

            userProfileViewModel.ReturnUrl = returnUrl;
            
            return View(userProfileViewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditProfile(UserProfileViewModel userProfileViewModel)
        {

            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByIdAsync(userProfileViewModel.Id);
                
                var user = userProfileViewModel.ToUser(existingUser);

                await _userManager.UpdateAsync(user);

               return Redirect(userProfileViewModel.ReturnUrl);
            }

            return View(userProfileViewModel);

        }

        
            // GET: Страница сброса пароля
            [HttpGet]
            public IActionResult ResetPassword(string userId, string returnUrl)
            {
                var user = _userManager.FindByIdAsync(userId).Result;
                if (user == null)
                {
                    return NotFound();
                }

                var model = new ResetPasswordViewModel
                {
                    UserId = user.Id,
                    Email = user.Email,
                    ReturnUrl = returnUrl
                };

                return View(model);
            }

            // POST: Обработка сброса пароля
            [HttpPost]
            public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var user = await _userManager.FindByIdAsync(model.UserId);
                if (user == null)
                {
                    return NotFound();
                }

                // Сбрасываем пароль
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);

                if (result.Succeeded)
                {
                    return Redirect(model.ReturnUrl);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(model);
            }
  
    }
}
