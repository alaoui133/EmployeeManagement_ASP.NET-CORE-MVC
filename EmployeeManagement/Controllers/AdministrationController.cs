using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;


        public AdministrationController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
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


        [HttpPost]
        public IActionResult ListRoles(int x)
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }

        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            if (id is null)
            {
                return View("NotFound","Please Add Role Id to url");
            }
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return View("NotFound", $"this Role Id :{id} Note Exist");
            }

            UpdateRoleViewModel model = new UpdateRoleViewModel()
            {
                Id = role.Id,
                RoleName = role.Name

            };

            foreach (var user in _userManager.Users )
            {
                if (await _userManager.IsInRoleAsync(user , role.Name))
                {
                    model.Users.Add(user.Email);
                }
            }

            return View();
        }




       
    }
   

}

