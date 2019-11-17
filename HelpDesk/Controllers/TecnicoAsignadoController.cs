using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HelpDesk.Data;
using HelpDesk.Models;

namespace HelpDesk.Controllers
{
    public class TecnicoAsignadoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TecnicoAsignadoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TecnicoAsignado
        public async Task<IActionResult> Index()
        {
            return View(await _context.TecnicoAsignado.ToListAsync());
        }

        // GET: TecnicoAsignado/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tecnicoAsignado = await _context.TecnicoAsignado
                .SingleOrDefaultAsync(m => m.ID == id);
            if (tecnicoAsignado == null)
            {
                return NotFound();
            }

            return View(tecnicoAsignado);
        }

        // GET: TecnicoAsignado/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TecnicoAsignado/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,nombreTecnico")] TecnicoAsignado tecnicoAsignado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tecnicoAsignado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tecnicoAsignado);
        }

        // GET: TecnicoAsignado/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tecnicoAsignado = await _context.TecnicoAsignado.SingleOrDefaultAsync(m => m.ID == id);
            if (tecnicoAsignado == null)
            {
                return NotFound();
            }
            return View(tecnicoAsignado);
        }

        // POST: TecnicoAsignado/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,nombreTecnico")] TecnicoAsignado tecnicoAsignado)
        {
            if (id != tecnicoAsignado.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tecnicoAsignado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TecnicoAsignadoExists(tecnicoAsignado.ID))
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
            return View(tecnicoAsignado);
        }

        // GET: TecnicoAsignado/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tecnicoAsignado = await _context.TecnicoAsignado
                .SingleOrDefaultAsync(m => m.ID == id);
            if (tecnicoAsignado == null)
            {
                return NotFound();
            }

            return View(tecnicoAsignado);
        }

        // POST: TecnicoAsignado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tecnicoAsignado = await _context.TecnicoAsignado.SingleOrDefaultAsync(m => m.ID == id);
            _context.TecnicoAsignado.Remove(tecnicoAsignado);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TecnicoAsignadoExists(int id)
        {
            return _context.TecnicoAsignado.Any(e => e.ID == id);
        }
    }
}
