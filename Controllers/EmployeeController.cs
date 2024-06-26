﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StaffScroll.DAL;
using StaffScroll.Models;
using StaffScroll.Models.Entities;
using StaffScroll.Sevices;

namespace StaffScroll.Controllers
{
    public class EmployeeController : Controller
    {
      //  public EmployeeDbContext _context { get; }
        public ApplicationDbContext _applicationDbContext { get; }
        public EmployeeController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var employees = _applicationDbContext.Employees.ToList();
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
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var employee = new Employee()
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        DateOfBirth = model.DateOfBirth,
                        Salary = model.Salary,
                    };
                    await _applicationDbContext.Employees.AddAsync(employee);
                    await _applicationDbContext.SaveChangesAsync();
                    TempData["successMessage"] = "Created Successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["errorMessage"] = "Model data is not valid";
                }
                return View();
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }
    }
}
