using EmployeeManagement.Models;
using EmployeeManagement.Models.Repositories;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IWebHostEnvironment = Microsoft.AspNetCore.Hosting.IWebHostEnvironment;

namespace EmployeeManagement.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly ICompanyRepository<Employee> _CompanyRepository;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public IEnumerable<Employee> employees { get; }

        Employee employee { get; set; }




        public EmployeeController(ICompanyRepository<Employee> CompanyRepository
            , IWebHostEnvironment hostingEnvironment)

        {
            //_CompanyRepository = new EmployeeRepository();
            _CompanyRepository = CompanyRepository;
            _hostingEnvironment = hostingEnvironment;
        }



        [AllowAnonymous]
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            employee = _CompanyRepository.Get(id ?? 1);
            if (employee is null)
            {
                return View("NotFoundError", id);
            }

            //ViewBag.Title = "Detail Page  | welcome Alaoui";
            //Employee emp1 = _CompanyRepository.Get(1);

            return View(employee);

        }

        [AllowAnonymous]
        [Route("")]
        public ViewResult Index()
        {
            IEnumerable<Employee> employees = _CompanyRepository.GetAll();

            //Employee employee = _CompanyRepository.Get(1);
            //  ViewData["emp"] = employee;

            return View(employees);

        }

        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(EmployeeCreateViewModel model)
        {

            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (model.Image != null)
                {
                    string extFile = Path.GetExtension(model.Image.FileName);

                    if (extFile != ".jpg" && extFile != ".png")
                    {
                        ModelState.AddModelError("", "Invalid File Format ");
                        return View(model);
                    }

                    uniqueFileName = CreateImageFileEmployee(model);

                }

                Employee employee = new Employee()
                {
                    Departement = model.Departement,
                    Email = model.Email,
                    Name = model.Name,
                    Image = uniqueFileName
                };
                _CompanyRepository.Add(employee);

                TempData["Message"] = $"Employee '{employee.Name} Created Successfully";


                //return RedirectToAction("Details", new { id = employee.Id });
                return RedirectToAction("Index");


            }


            return View(model);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            Employee employee = _CompanyRepository.Get(id);

            if (employee is null)
            {
                return View("NotFoundError", id);
            }

            EmployeeEditViewModel model = new EmployeeEditViewModel()
            {
                Id = employee.Id,
                Name = employee.Name,
                Departement = (Departement)employee.Departement,
                Email = employee.Email,
                ImagPath = employee.Image
            };


            return View(model);
        }


        [HttpPost]
        public ActionResult Update(EmployeeEditViewModel model)
        {
            Employee employee = _CompanyRepository.Get(model.Id);
            employee.Departement = model.Departement;
            employee.Name = model.Name;
            employee.Email = model.Email;

            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (model.Image != null)
                {
                    string extFile = Path.GetExtension(model.Image.FileName);

                    if (extFile != ".jpg" && extFile != ".png")
                    {
                        ModelState.AddModelError("", "Invalid File Format ");
                        return View(model);
                    }
                    uniqueFileName = CreateImageFileEmployee(model);
                    // wwwroot/images/name.png
                    // delete old photo 
                    if (employee.Image != null)
                    {
                        string oldImage = Path.Combine(_hostingEnvironment.WebRootPath, "images", employee.Image);
                        System.IO.File.Delete(oldImage);
                    }
                    employee.Image = uniqueFileName;
                }


                _CompanyRepository.Update(employee);
                TempData["Message"] = $"Employee '{employee.Name} Updated Successfully";


                return RedirectToAction("Details", new { id = employee.Id });


            }


            return View(model);
        }


        [HttpGet]
        public IActionResult Delete(int id)

        {
           

            Employee employee = _CompanyRepository.Get(id);

            if (employee is null)
            {
                return View("NotFoundError", id);
            }

            EmployeeDeleteViewModel model = new EmployeeDeleteViewModel()
            {
                Id = employee.Id,
                Name = employee.Name,
                Departement = (Departement)employee.Departement,
                Email = employee.Email,
                ImagPath = employee.Image
            };


            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(EmployeeDeleteViewModel modelDel)
        {
            Employee employee = _CompanyRepository.Get(modelDel.Id);

            if (employee is null)
            {
                return View("NotFoundError", modelDel.Id);
            }
            else
            {
                _CompanyRepository.Delete(modelDel.Id);
               

                TempData["Message"] = $"Employee '{modelDel.Name} Deleted Successfully";


            }


            return RedirectToAction("Index");
        }

        private string CreateImageFileEmployee(EmployeeCreateViewModel model)
        {
            string uniqueFileName;
            string UploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
            uniqueFileName = Guid.NewGuid() + "_" + model.Image.FileName;
            string PathFile = Path.Combine(UploadFolder, uniqueFileName); //wwwroot/images/dskdsk.png
            using (var fileStream = new FileStream(PathFile, FileMode.Create))
            {
                model.Image.CopyTo(fileStream);
            }

            return uniqueFileName;
        }
    }
}

