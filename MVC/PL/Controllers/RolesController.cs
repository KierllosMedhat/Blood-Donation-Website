using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PL.Models;

namespace PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<RolesController> logger;

        public RolesController(
            RoleManager<ApplicationRole> roleManager,
            UserManager<ApplicationUser> userManager,
            ILogger<RolesController> logger)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var roles = await roleManager.Roles.ToListAsync();
            return View(roles);
        }

        public async Task<IActionResult> Create()
        {
            return View(new ApplicationRole());
        }

        [HttpPost]
        public async Task<IActionResult> Create(ApplicationRole role)
        {
            if (ModelState.IsValid)
            {
                var result = await roleManager.CreateAsync(role);

                if (result.Succeeded)
                    return RedirectToAction("Index");

                foreach (var error in result.Errors)
                {
                    logger.LogError(error.Description);
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(role);
        }

        public async Task<IActionResult> Details(string id, string viewName = "Details")
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var user = await roleManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();
            else
                return View(viewName, user);
        }

        public async Task<IActionResult> Update(string id)
        {
            return await Details(id, "Update");
        }

        [HttpPost]
        public async Task<IActionResult> Update(string id, ApplicationRole applcationRole)
        {
            if (id != applcationRole.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                var role = await roleManager.FindByIdAsync(id);

                role.Name = applcationRole.Name;
                role.NormalizedName = applcationRole.Name.ToUpper();

                var result = await roleManager.UpdateAsync(role);

                if (result.Succeeded)
                    return RedirectToAction("Index");

                foreach (var error in result.Errors)
                {
                    logger.LogError(error.Description);
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(applcationRole);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var user = await roleManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            var result = await roleManager.DeleteAsync(user);

            if (result.Succeeded)
                return RedirectToAction("Index");

            foreach (var error in result.Errors)
            {
                logger.LogError(error.Description);
                ModelState.AddModelError("", error.Description);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AddOrRemoveUsers(string Id)
        {
            var role = await roleManager.FindByIdAsync(Id);

            if (role is null)
                return NotFound();

            ViewBag.RoleId = Id;

            var usersInRole = new List<UserInRoleViewModel>();

            var users = await userManager.Users.ToListAsync();

            foreach (var user in users)
            {
                var userInRole = new UserInRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                };

                if (await userManager.IsInRoleAsync(user, role.Name))
                    userInRole.IsSelected = true;
                else
                    userInRole.IsSelected = false;

                usersInRole.Add(userInRole);
            }

            return View(usersInRole);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUsers(string Id, List<UserInRoleViewModel> users)
        {
            var role = await roleManager.FindByIdAsync(Id);

            if (role is null)
                return NotFound();

            if (ModelState.IsValid)
            {
                foreach (var user in users)
                {
                    var appUser = await userManager.FindByIdAsync(user.UserId);

                    if (appUser != null)
                    {
                        if (user.IsSelected && !await userManager.IsInRoleAsync(appUser, role.Name))
                            await userManager.AddToRoleAsync(appUser, role.Name);
                        else if (!user.IsSelected && await userManager.IsInRoleAsync(appUser, role.Name))
                            await userManager.RemoveFromRoleAsync(appUser, role.Name);
                    }
                }
                return RedirectToAction("Update", new { id = Id });
            }

            return View(users);

        }

    }
}
