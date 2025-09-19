using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyMvcApp.Models;

namespace MyWebApp.Controllers
{
    [Route("Admin/[controller]")]
    public class StudentController : Controller
    {
        // Sử dụng static để data được chia sẻ giữa các request
        private static readonly List<Student> listStudents = new List<Student>()
        {
            new Student() { Id = 101, Name = "Hải Nam", Branch = Branch.IT,
                Gender = Gender.Male, IsRegular = true,
                Address = "A1-2018", Email = "nam@g.com",
                DateOfBirth = new DateTime(1999, 11, 5),
                Avatar = "/images/myself.jpg",
                Score = 6
                 },
            new Student() { Id = 102, Name = "Minh Tú", Branch = Branch.BE,
                Gender = Gender.Female, IsRegular = true,
                Address = "A1-2019", Email = "tu@g.com",
                 DateOfBirth = new DateTime(2002, 7, 18),
                 Avatar = "/images/myself.jpg",
                Score = 6
                 },
            new Student() { Id = 103, Name = "Hoàng Phong", Branch = Branch.CE,
                Gender = Gender.Male, IsRegular = false,
                Address = "A1-2020", Email = "phong@g.com",
                DateOfBirth = new DateTime(2001, 3, 22),
                Avatar = "/images/myself.jpg",
                Score = 6
                },
            new Student() { Id = 104, Name = "Xuân Mai", Branch = Branch.EE,
                Gender = Gender.Female, IsRegular = false,
                Address = "A1-2021", Email = "mai@g.com",
                DateOfBirth = new DateTime(2001, 3, 22),
                Avatar = "/images/myself.jpg",
                Score = 6
                 }
        };

        private readonly IWebHostEnvironment _webHostEnvironment;

        public StudentController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("List")]
        public IActionResult List()
        {
            return View("~/Views/Admin/Student/List.cshtml", listStudents);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            // Lấy danh sách các giá trị Gender để hiển thị radio button trên form
            ViewBag.AllGenders = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();

            // Lấy danh sách các giá trị Branch để hiển thị select-option trên form
            ViewBag.AllBranches = new List<SelectListItem>()
            {
                new SelectListItem { Text = "IT", Value = "1" },
                new SelectListItem { Text = "BE", Value = "2" },
                new SelectListItem { Text = "CE", Value = "3" },
                new SelectListItem { Text = "EE", Value = "4" }
            };

            return View("~/Views/Admin/Student/Create.cshtml");
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(Student s, IFormFile avatarFile)
        {
            LoadViewData();

            if (!ModelState.IsValid)
            {
                return View("~/Views/Admin/Student/Create.cshtml", s);
            }

            if (avatarFile != null && avatarFile.Length > 0)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                var fileExtension = Path.GetExtension(avatarFile.FileName).ToLower();

                if (!allowedExtensions.Contains(fileExtension))
                {
                    ModelState.AddModelError("Avatar", "Chỉ chấp nhận file ảnh có định dạng: .jpg, .jpeg, .png, .gif");
                    return View("~/Views/Admin/Student/Create.cshtml", s);
                }

                if (avatarFile.Length > 5 * 1024 * 1024)
                {
                    ModelState.AddModelError("Avatar", "Kích thước file không được vượt quá 5MB");
                    return View("~/Views/Admin/Student/Create.cshtml", s);
                }

                var fileName = $"{Guid.NewGuid()}{fileExtension}";
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                Directory.CreateDirectory(uploadsFolder);

                var filePath = Path.Combine(uploadsFolder, fileName);
                using var fileStream = new FileStream(filePath, FileMode.Create);
                await avatarFile.CopyToAsync(fileStream);

                s.Avatar = $"/uploads/{fileName}";
            }
            else
            {
                s.Avatar = "/images/myself.jpg";
            }

            s.Id = listStudents.Count > 0 ? listStudents.Max(st => st.Id) + 1 : 1;
            listStudents.Add(s);

            return RedirectToAction("List");
        }
        private void LoadViewData()
        {
            ViewBag.AllGenders = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();
            ViewBag.AllBranches = new List<SelectListItem>
    {
        new() { Text = "IT", Value = "1" },
        new() { Text = "BE", Value = "2" },
        new() { Text = "CE", Value = "3" },
        new() { Text = "EE", Value = "4" }
    };
        }
    }
}