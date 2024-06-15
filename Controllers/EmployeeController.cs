using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StaffScroll.DAL;
using StaffScroll.Models;

namespace StaffScroll.Controllers
{
    public class EmployeeController : Controller
    {
        public EmployeeDbContext _context { get; }
        public EmployeeController(EmployeeDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var employees = _context.Employees.ToList();
            List<EmployeeViewModel> retList = new List<EmployeeViewModel>();
            if (employees.Any())
            {
                foreach (var item in employees)
                {
                    var empData = new EmployeeViewModel
                    {
                        Id = item.Id,
                        FirstName = item.FirstName,
                        DateOfBirth = item.DateOfBirth,
                        LastName = item.LastName,
                        Email = item.Email,
                        Salary = item.Salary,
                    };
                    retList.Add(empData);
                }
            }
            return View(retList);
        }
    }
}
