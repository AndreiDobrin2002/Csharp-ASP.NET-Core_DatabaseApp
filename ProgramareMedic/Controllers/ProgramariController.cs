using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProgramareMedic.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramareMedic.Controllers
{
    public class ProgramariController : Controller
    {
        private readonly ProgramareMedicContext _context;

        public ProgramariController(ProgramareMedicContext context)
        {
            _context = context;
        }

        // GET: Programari
        public async Task<IActionResult> Index()
        {
            var programari = _context.Programari.Include(p => p.Pacient).Include(p => p.Medic);
            return View(await programari.ToListAsync());
        }

        // GET: Programari/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programare = await _context.Programari
                .Include(p => p.Pacient)
                .Include(p => p.Medic)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (programare == null)
            {
                return NotFound();
            }

            return View(programare);
        }

        // GET: Programari/Create
        public IActionResult Create()
        {
            ViewBag.PacientId = new SelectList(_context.Pacienti, "Id", "NumeComplet");
            ViewBag.MedicId = new SelectList(_context.Medici, "Id", "NumeComplet");
            return View();
        }

        // POST: Programari/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PacientId,MedicId,DataProgramare,CategorieServicii")] Programari programare)
        {
            try
            {
           
                foreach (var key in ModelState.Keys)
                {
                    var state = ModelState[key];
                    if (state.Errors.Any())
                    {
                        Debug.WriteLine($"Key: {key}");
                        foreach (var error in state.Errors)
                        {
                            Debug.WriteLine($"Error: {error.ErrorMessage}");
                        }
                    }
                }

                if (ModelState.IsValid)
                {
                    _context.Add(programare);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _context.Add(programare);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.PacientId = new SelectList(_context.Pacienti, "Id", "NumeComplet", programare.PacientId);
                ViewBag.MedicId = new SelectList(_context.Medici, "Id", "NumeComplet", programare.MedicId);
                return View(programare);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return View();
            }
        }

        // GET: Programari/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programare = await _context.Programari.FindAsync(id);
            if (programare == null)
            {
                return NotFound();
            }
            ViewBag.PacientId = new SelectList(_context.Pacienti, "Id", "NumeComplet", programare.PacientId);
            ViewBag.MedicId = new SelectList(_context.Medici, "Id", "NumeComplet", programare.MedicId);
            return View(programare);
        }

        // POST: Programari/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PacientId,MedicId,DataProgramare,CategorieServicii")] Programari programare)
        {
            if (id != programare.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(programare);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProgramareExists(programare.Id))
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

            ViewBag.PacientId = new SelectList(_context.Pacienti, "Id", "NumeComplet", programare.PacientId);
            ViewBag.MedicId = new SelectList(_context.Medici, "Id", "NumeComplet", programare.MedicId);
            return View(programare);
        }

        // GET: Programari/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programare = await _context.Programari
                .Include(p => p.Pacient)
                .Include(p => p.Medic)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (programare == null)
            {
                return NotFound();
            }

            return View(programare);
        }

        // POST: Programari/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var programare = await _context.Programari.FindAsync(id);
            _context.Programari.Remove(programare);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProgramareExists(int id)
        {
            return _context.Programari.Any(e => e.Id == id);
        }
    }
}
