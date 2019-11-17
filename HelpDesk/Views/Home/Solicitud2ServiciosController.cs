using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HelpDesk.Data;
using HelpDesk.Models;
using Microsoft.AspNetCore.Identity;

namespace HelpDesk.Controllers
{
    public class Solicitud2ServiciosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public Solicitud2ServiciosController(ApplicationDbContext context,
                                            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: SolicitudServicios
        public async Task<IActionResult> Index()
        {
            return View(await _context.SolicitudServicio.ToListAsync());
        }

        public async Task<IActionResult> SolicitudSinAsignar()
        {
            return View(await (from s in _context.SolicitudServicio
                               where s.tecnicoAsig == null
                               select s).ToListAsync());
        }

        public async Task<IActionResult> AsignarTecnico(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitudServicio = await _context.SolicitudServicio.SingleOrDefaultAsync(m => m.ID == id);
            solicitudServicio.getEstados(_context);
            solicitudServicio.getTecnico(_context);
            if (solicitudServicio == null)
            {
                return NotFound();
            }
            return View(solicitudServicio);
        }

        public async Task<IActionResult> AsignarOtroTecnico(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitudServicio = await _context.SolicitudServicio.SingleOrDefaultAsync(m => m.ID == id);
            solicitudServicio.getEstados(_context);
            solicitudServicio.getTecnico(_context);
            if (solicitudServicio == null)
            {
                return NotFound();
            }
            return View(solicitudServicio);
        }

        public async Task<IActionResult> ServicioTecnico()
        {
            
            return View(await (
                               from x in _context.SolicitudServicio
                               where x.tecnicoAsig
                               == _userManager.GetUserName(User)
                               select x).ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> ServicioCompletado(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitudServicio = await _context.SolicitudServicio.SingleOrDefaultAsync(m => m.ID == id);
            solicitudServicio.getEstados(_context);
            if (solicitudServicio == null)
            {
                return NotFound();
            }
            return View(solicitudServicio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ServicioCompleto(int id, [Bind("ID,FechaSolicitud,estadoS,comentarioAdicional")] SolicitudServicio solicitudServicio)
        {
            if (id != solicitudServicio.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(solicitudServicio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SolicitudServicioExists(solicitudServicio.ID))
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
            return View(solicitudServicio);
        }

        // GET: SolicitudServicios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitudServicio = await _context.SolicitudServicio
                .SingleOrDefaultAsync(m => m.ID == id);
            if (solicitudServicio == null)
            {
                return NotFound();
            }

            return View(solicitudServicio);
        }

        // GET: SolicitudServicios/Create
        public IActionResult Create()
        {
            SolicitudServicio solicitudServicio = new SolicitudServicio();
            solicitudServicio.getEstados(_context);
            solicitudServicio.getTecnico(_context);
            return View(solicitudServicio);
        }

        // POST: SolicitudServicios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Solicitante,FechaSolicitud,Descripcion,estadoS,tecnicoAsig,comentarioAdicional")] SolicitudServicio solicitudServicio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(solicitudServicio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(solicitudServicio);
        }

        // GET: SolicitudServicios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitudServicio = await _context.SolicitudServicio.SingleOrDefaultAsync(m => m.ID == id);
            if (solicitudServicio == null)
            {
                return NotFound();
            }
            return View(solicitudServicio);
        }

        // POST: SolicitudServicios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Solicitante,FechaSolicitud,Descripcion,estadoS,tecnicoAsig,comentarioAdicional")] SolicitudServicio solicitudServicio)
        {
            if (id != solicitudServicio.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(solicitudServicio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SolicitudServicioExists(solicitudServicio.ID))
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
            return View(solicitudServicio);
        }

        // GET: SolicitudServicios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitudServicio = await _context.SolicitudServicio
                .SingleOrDefaultAsync(m => m.ID == id);
            if (solicitudServicio == null)
            {
                return NotFound();
            }

            return View(solicitudServicio);
        }

        // POST: SolicitudServicios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var solicitudServicio = await _context.SolicitudServicio.SingleOrDefaultAsync(m => m.ID == id);
            _context.SolicitudServicio.Remove(solicitudServicio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SolicitudServicioExists(int id)
        {
            return _context.SolicitudServicio.Any(e => e.ID == id);
        }
    }
}
