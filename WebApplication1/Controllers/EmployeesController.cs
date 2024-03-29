﻿using EmployeeCRUD.Data;
using EmployeeCRUD.Models;
using EmployeeCRUD.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeCRUD.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly MVCDemoDbContext mcvDemoDbContext;

        public EmployeesController(MVCDemoDbContext mcvDemoDbContext)
        {
            this.mcvDemoDbContext = mcvDemoDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await mcvDemoDbContext.Employees.ToListAsync();
            return View(employees);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeRequest.Name,
                Email = addEmployeeRequest.Email,
                Salary = addEmployeeRequest.Salary,
                Department = addEmployeeRequest.Department,
                DateOfBirth = addEmployeeRequest.DateOfBirth,
            };

            await mcvDemoDbContext.Employees.AddAsync(employee);
            await mcvDemoDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
           var employee = await mcvDemoDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if(employee != null)
            {
                var viewModel = new UpdateEmployeeViewModel()
                {
                    Id = Guid.NewGuid(),
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    Department = employee.Department,
                    DateOfBirth = employee.DateOfBirth,
                };

                return await Task.Run(() => View("View", viewModel));
            }
           return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeViewModel model)
        {
            var employee = await mcvDemoDbContext.Employees.FindAsync(model.Id);

            if(employee != null)
            {
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Salary = model.Salary;
                employee.DateOfBirth = model.DateOfBirth;
                employee.Department = model.Department;

                await mcvDemoDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");

        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel model)
        {
            var employee = await mcvDemoDbContext.Employees.FindAsync(model.Id);

            if(employee != null)
            {
                mcvDemoDbContext.Employees.Remove(employee);
                await mcvDemoDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");

        }

    }
}
