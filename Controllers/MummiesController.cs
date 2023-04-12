using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using INTEXII.Data;
using INTEXII.Models;

namespace INTEXII.Controllers
{
    public class MummiesController : Controller
    {
        private readonly RDSContext _context;

        public MummiesController(RDSContext context)
        {
            _context = context;
        }

        // GET: Mummies
        public async Task<IActionResult> Index()
        {
              return _context.Mummies != null ? 
                          View(await _context.Mummies.ToListAsync()) :
                          Problem("Entity set 'RDSContext.Mummies'  is null.");
        }

        // GET: Mummies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Mummies == null)
            {
                return NotFound();
            }

            var mummies = await _context.Mummies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mummies == null)
            {
                return NotFound();
            }

            return View(mummies);
        }

        // GET: Mummies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Mummies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Username,RoleType")] Mummies mummies)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mummies);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mummies);
        }

        // GET: Mummies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Mummies == null)
            {
                return NotFound();
            }

            var mummies = await _context.Mummies.FindAsync(id);
            if (mummies == null)
            {
                return NotFound();
            }
            return View(mummies);
        }

        // POST: Mummies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Username,RoleType")] Mummies mummies)
        {
            if (id != mummies.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mummies);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MummiesExists(mummies.Id))
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
            return View(mummies);
        }

        // GET: Mummies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Mummies == null)
            {
                return NotFound();
            }

            var mummies = await _context.Mummies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mummies == null)
            {
                return NotFound();
            }

            return View(mummies);
        }

        // POST: Mummies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Mummies == null)
            {
                return Problem("Entity set 'RDSContext.Mummies'  is null.");
            }
            var mummies = await _context.Mummies.FindAsync(id);
            if (mummies != null)
            {
                _context.Mummies.Remove(mummies);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MummiesExists(int id)
        {
          return (_context.Mummies?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
