using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using StudentApplication.Data;
using StudentApplication.Models;

namespace StudentApplication.Controllers
{
    public class StudentController : Controller
    {

        private readonly ApplicationDbContext applicationDbContext;


        public StudentController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel addStudentViewModel)
        {
            var student = new Student
            {
                Name = addStudentViewModel.Name,
                EmailId = addStudentViewModel.EmailId,
                Remarks = addStudentViewModel.Remarks,
                Subscribed = addStudentViewModel.Subscribed
            };
            await applicationDbContext.Students.AddAsync(student);
            await applicationDbContext.SaveChangesAsync();
            return View();

        }


        [HttpGet]
        public async Task<IActionResult> StudentList()
        {
            var students = await applicationDbContext.Students.ToListAsync();
            return View(students);
        }


        [HttpGet]
        public async Task<IActionResult> EditStudent(Guid id)
        {
            var student = await applicationDbContext.Students.FindAsync(id);
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> EditStudent(Student viewModel)
        {
            var student = await applicationDbContext.Students.FindAsync(viewModel.Id);
            if (student is not null)
            {
                student.Name = viewModel.Name;
                student.EmailId = viewModel.EmailId;
                student.Remarks = viewModel.Remarks;
                student.Subscribed = viewModel.Subscribed;

                await applicationDbContext
                    .SaveChangesAsync();
            }

            return RedirectToAction("StudentList", "Student");

        }

        [HttpPost]

        public async Task<IActionResult> Delete(Student viewModel)
        {
            var student = await applicationDbContext.Students.FindAsync(viewModel.Id);
            if (student is not null)
            {
                applicationDbContext.Students.Remove(student);
                await applicationDbContext.SaveChangesAsync();
            }
            return RedirectToAction("StudentList", "Student");

        }
    }
}
