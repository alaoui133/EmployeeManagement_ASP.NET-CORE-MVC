using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return  RedirectToAction("Index","Employee");
        }

        public IActionResult Register() 
        { 
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>  Register(AccountRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                string FullName = GenerateUserName(model.FirstName, model.LastName);
                IdentityUser user  = new IdentityUser { UserName=model.EmailAdress ,Email = model.EmailAdress};
              var result =  await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                   await _signInManager.SignInAsync(user , isPersistent:false);
                    return RedirectToAction("Index", "Employee");
                }

                foreach (var error in result.Errors)
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
        public async Task<IActionResult> Login(AccountLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
             var result = await _signInManager.PasswordSignInAsync(model.EmailAdress,model.Password,false,false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Employee");
                }
                ModelState.AddModelError(string.Empty, "Login Invalid .");
            }
            return View();
        }


        private string GenerateUserName(string FirstName , string LastNAme)
        {
            return FirstName.Trim().ToUpper()+" " + LastNAme.Trim().ToLower();
        }


    }
}
