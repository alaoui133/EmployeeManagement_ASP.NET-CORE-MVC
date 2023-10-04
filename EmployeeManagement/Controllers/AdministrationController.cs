using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public AdministrationController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> CreateRole(AdministrationCreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = model.RoleName,

                };
                IdentityResult result = await _roleManager.CreateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(ListRoles));
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                TempData["Message"] = "Role added Successfully";

            }


            return View();
        }
        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }





        [HttpGet]
        public async Task<IActionResult> Update(string? id)
        {
            if (id is null)
            {
                return View("NotFound", "Please Add Role Id to url");
            }
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return View("NotFound", $"this Role Id :{id} Note Exist");
            }

            UpdateRoleViewModel model = new UpdateRoleViewModel()
            {
                Id = role.Id,
                RoleName = role.Name,
                Users = new List<string>()

            };
            var list = _userManager.Users.ToList();
            foreach (AppUser user in list)
            {

                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.Email);
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {


                IdentityRole role = await _roleManager.FindByIdAsync(model.Id);
                if (role == null)
                {
                    return View("NotFound", $"this Role Id :{model.Id} Note Exist");
                }

                role.Name = model.RoleName;
                IdentityResult result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(ListRoles));
                }

            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateUsersRole(string idRole)
        {

            IdentityRole role = await _roleManager.FindByIdAsync(idRole);
            if (role == null)
            {
                return View("NotFound", $"this Role Id :{role.Id} Note Exist");
            }

            List<UpdateUsersRoleViewModel> ModelList = new List<UpdateUsersRoleViewModel>();

            foreach (AppUser user in _userManager.Users.ToList())
            {
                UpdateUsersRoleViewModel model = new UpdateUsersRoleViewModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    IsSelected = false
                };

                if (await _userManager.IsInRoleAsync(user,role.Name))
                {
                    ModelList.Add(model);
                }
            }

            ViewBag.RoleId = idRole ;
            return View(ModelList);
        }






    }


}

