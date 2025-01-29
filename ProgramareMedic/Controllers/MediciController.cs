using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProgramareMedic.Models;

namespace ProgramareMedic.Controllers
{
    public class MediciController : Controller
    {
        private readonly ProgramareMedicContext _context;

        public MediciController(ProgramareMedicContext context)
        {
            _context = context;
        }

        // GET: Medici
        public async Task<IActionResult> Index()
        {
            return View(await _context.Medici.ToListAsync());
        }

        // GET: Medici/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medic = await _context.Medici
                .FirstOrDefaultAsync(m => m.Id == id);
            if (medic == null)
            {
                return NotFound();
            }

            return View(medic);
        }

        // GET: Medici/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Medici/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NumeMedic,PrenumeMedic,Specializare,Clinica")] Medici medic)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(medic);
        }

        // GET: Medici/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medic = await _context.Medici.FindAsync(id);
            if (medic == null)
            {
                return NotFound();
            }
            return View(medic);
        }

        // POST: Medici/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NumeMedic,PrenumeMedic,Specializare,Clinica")] Medici medic)
        {
            if (id != medic.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicExists(medic.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(medic);
        }

        // GET: Medici/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medic = await _context.Medici
                .FirstOrDefaultAsync(m => m.Id == id);
            if (medic == null)
            {
                return NotFound();
            }

            return View(medic);
        }

        // POST: Medici/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medic = await _context.Medici.FindAsync(id);
            _context.Medici.Remove(medic);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicExists(int id)
        {
            return _context.Medici.Any(e => e.Id == id);
        }
    }
}
