using Microsoft.AspNetCore.Mvc;

namespace DiplomMetod.Controllers
{
    using DiplomMetod.Data.Entites;
    using DiplomMetod.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Data;

    [Authorize(Roles = "Administrator")] // Доступ только для админов
    public class AdminController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // Главная страница админки (список пользователей)
        public async Task<IActionResult> Index()
        {
            var users = _userManager.Users.ToList();
            var userRolesViewModel = new List<UserRoleViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userRolesViewModel.Add(new UserRoleViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    LockoutEnd = user.LockoutEnd,
                    Roles = roles.ToList()
                });
            }

            return View(userRolesViewModel);
        }

        // Добавление нового пользователя (GET)
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Добавление нового пользователя (POST)
        [HttpPost]
        public async Task<IActionResult> Create(RegisterUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // По умолчанию добавляем роль "User"
                    await _userManager.AddToRoleAsync(user, "User");
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        // Управление ролями пользователя (GET)
        [HttpGet]
        public async Task<IActionResult> ManageRoles(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();

            var roles = _roleManager.Roles.ToList();
            var model = new ManageRolesViewModel
            {
                UserId = user.Id,
                Email = user.Email,
                UserRoles = roles.Select(role => new RoleViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name,
                    IsSelected = _userManager.IsInRoleAsync(user, role.Name).Result
                }).ToList()
            };

            return View(model);
        }

        // Управление ролями пользователя (POST)
        [HttpPost]
        public async Task<IActionResult> ManageRoles(ManageRolesViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
                return NotFound();

            var userRoles = await _userManager.GetRolesAsync(user);

            // Удаляем все текущие роли и добавляем выбранные
            await _userManager.RemoveFromRolesAsync(user, userRoles);
            await _userManager.AddToRolesAsync(
                user,
                model.UserRoles.Where(r => r.IsSelected).Select(r => r.RoleName)
            );

            return RedirectToAction("Index");
        }

        // Блокировка/разблокировка пользователя
        [HttpPost]
        public async Task<IActionResult> ToggleBlock(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();

            user.LockoutEnabled = true;
            user.LockoutEnd = user.LockoutEnd.HasValue ? null : DateTimeOffset.MaxValue;

            await _userManager.UpdateAsync(user);
            return RedirectToAction("Index");
        }

        // Удаление пользователя
        [HttpPost]
        public async Task<IActionResult> Delete(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();

            await _userManager.DeleteAsync(user);
            return RedirectToAction("Index");
        }
    }
}
