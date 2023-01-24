using Anyar.DAL;
using Anyar.Models;
using Anyar.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Anyar.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin")]
    public class PositionController : Controller
    {
        public AppDbContext _context { get; }

        public PositionController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Delete(int? id)
        {
            if (id == null) return BadRequest();
            var position = _context.Positions.FirstOrDefault(p => p.Id == id);
            if (position == null) return BadRequest();
            _context.Positions.Remove(position);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Index()
        {
            return View(_context.Positions);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(PositionVM positionVM)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            Position position=new Position { Name= positionVM.Name };
            await _context.Positions.AddAsync(position);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int? id)
        {
            if (id == null) return BadRequest();
            var position= _context.Positions.FirstOrDefault(p=>p.Id==id);
            if (position == null) return BadRequest();
            PositionVM positionVM=new PositionVM { Name=position.Name};
            return View(positionVM);
        }
        [HttpPost]
        public IActionResult Update(int? id,PositionVM positionVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!ModelState.IsValid) return View();
            if (id is null) BadRequest();
            var exsited=_context.Positions.FirstOrDefault(p=>p.Id==id); 
            if(exsited==null) return NotFound();
            exsited.Name=positionVM.Name;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
    }
}
