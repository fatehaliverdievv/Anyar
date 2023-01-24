using Anyar.DAL;
using Anyar.Models;
using Anyar.Utilies.Extension;
using Anyar.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Anyar.Areas.Manage.Controllers
{
    [Authorize(Roles ="Admin")]
    [Area("Manage")]
    public class EmployeeController : Controller
    {
        public AppDbContext _context { get; }
        public IWebHostEnvironment _env { get; }
        public EmployeeController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            return View(_context.Employees.Include(p=>p.Position));
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Positions=new SelectList(_context.Positions,nameof(Position.Id),nameof(Position.Name));
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeCreateVM employeeVM)
        {
            string result = employeeVM.Image.CheckValidate("image/", 800);
            if(result.Length>0) 
            {
                ModelState.AddModelError("Image", result);
            }
            if(!_context.Positions.Any(p=>p.Id == employeeVM.PositionId)) ModelState.AddModelError("PositionId", "bele bir position yoxdu");
            if(!ModelState.IsValid) 
            {
                ViewBag.Positions=new SelectList(_context.Positions,nameof(Position.Id),nameof(Position.Name));
                return View(); 
            }
            Employee employee = new Employee
            {
                Name = employeeVM.Name,
                PositionId = employeeVM.PositionId,
                LinkEdinLink = employeeVM.LinkEdinLink,
                FacebookLink = employeeVM.FacebookLink,
                TwitterLink = employeeVM.TwitterLink,
                InstagramLink = employeeVM.InstagramLink,
                Bio = employeeVM.Bio,
                ImageUrl = employeeVM.Image.SaveFile(Path.Combine(_env.WebRootPath, "assets", "img"))
            };
            _context.Add(employee);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int? id)
        {
            if(id== null) { return BadRequest(); }
            var employee = await _context.Employees.FirstOrDefaultAsync(p=>p.Id == id);  
            if(employee == null) { return NotFound(); }
            EmployeeUpdateVM employeeVM= new EmployeeUpdateVM
            {
                Name= employee.Name,
                PositionId= employee.PositionId,
                Bio= employee.Bio,
                FacebookLink= employee.FacebookLink,
                TwitterLink= employee.TwitterLink,
                InstagramLink= employee.InstagramLink,
                LinkEdinLink= employee.LinkEdinLink,
            };
            ViewBag.Positions = new SelectList(_context.Positions, nameof(Position.Id), nameof(Position.Name));
            return View(employeeVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id,EmployeeUpdateVM employeeVM)
        {
            if (id == null) { return BadRequest(); }
            var existedemployee = await _context.Employees.FirstOrDefaultAsync(p => p.Id == id);
            if (existedemployee == null) { return NotFound(); }
            if (!_context.Positions.Any(p => p.Id == employeeVM.PositionId)) ModelState.AddModelError("PositionId", "bele bir position yoxdu");
            if (employeeVM.Image!=null) 
            {
                 string result = employeeVM.Image.CheckValidate("image/", 800);
                 if (result.Length > 0)
                 {
                     ModelState.AddModelError("Image", result);
                 }
                else
                {
                    existedemployee.ImageUrl.DeleteFile(_env.WebRootPath, "assets/img");
                }
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Positions = new SelectList(_context.Positions, nameof(Position.Id), nameof(Position.Name));
                return View();
            }
            existedemployee.Name = employeeVM.Name;
            existedemployee.PositionId = employeeVM.PositionId;
            existedemployee.Bio = employeeVM.Bio;
            existedemployee.FacebookLink = employeeVM.FacebookLink;
            existedemployee.TwitterLink = employeeVM.TwitterLink;
            existedemployee.InstagramLink = employeeVM.InstagramLink;
            existedemployee.LinkEdinLink = employeeVM.LinkEdinLink;
            existedemployee.ImageUrl = employeeVM.Image.SaveFile(Path.Combine(_env.WebRootPath, "assets", "img"));
            ViewBag.Positions = new SelectList(_context.Positions, nameof(Position.Id), nameof(Position.Name));
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
