using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using INTEXII.Data;
using INTEXII.Models;
using Microsoft.AspNetCore.Authorization;

namespace INTEXII.Controllers
{
    public class finalburialrecords2Controller : Controller
    {
        private readonly RDSContext _context;

        public finalburialrecords2Controller(RDSContext context)
        {
            _context = context;
        }

        [Authorize]
        // GET: Mummies
        public async Task<IActionResult> Index()
        {
            return _context.finalburialrecords2 != null ?
                        View(await _context.finalburialrecords2.ToListAsync()) :
                        Problem("Entity set 'RDSContext.finalburialrecords2'  is null.");
        }

        // GET: Mummies/Details/5
        public async Task<IActionResult> Details(Int64 id)
        {
            if (_context.finalburialrecords2 == null)
            {
                return NotFound();
            }

            var burialdata = await _context.finalburialrecords2
                .FirstOrDefaultAsync(m => m.id == id);
            if (burialdata == null)
            {
                return NotFound();
            }

            return View(burialdata);
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
        public async Task<IActionResult> Create([Bind("id,colorvalue, structurevalue, sex, depth, ageatdeath, headdirection, burialid, textilefunctionvalue, HairColor, facebundles, Notes, filename, southtohead, westtohead, southtofeet, westtofeet, length, Observations")] finalburialrecords2 burialdata)
        {
            if (ModelState.IsValid)
            {
                _context.Add(burialdata);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(burialdata);
        }

        // GET: Mummies/Edit/5
        public async Task<IActionResult> Edit(Int64 id)
        {
            if (_context.finalburialrecords2 == null)
            {
                return NotFound();
            }

            var burialdata = await _context.finalburialrecords2.FindAsync(id);
            if (burialdata == null)
            {
                return NotFound();
            }
            return View(burialdata);
        }


        // POST: Mummies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Int64 id, [Bind("id,colorvalue, structurevalue, sex, depth, ageatdeath, headdirection, burialid, textilefunctionvalue, HairColor, facebundles, Notes, filename, southtohead, westtohead, southtofeet, westtofeet, length, Observations")] finalburialrecords2 burialdata)
        {
            if (id != burialdata.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(burialdata);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!finalburialrecords2Exists(burialdata.id))
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
            return View(burialdata);
        }

        // GET: Mummies/Delete/5
        public async Task<IActionResult> Delete(Int64 id)
        {
            if (_context.finalburialrecords2 == null)
            {
                return NotFound();
            }

            var burialdata = await _context.finalburialrecords2
                .FirstOrDefaultAsync(m => m.id == id);
            if (burialdata == null)
            {
                return NotFound();
            }

            return View(burialdata);
        }

        // POST: Mummies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Int64 id)
        {
            if (_context.finalburialrecords2 == null)
            {
                return Problem("Entity set 'RDSContext.Mummies'  is null.");
            }
            var burialdata = await _context.finalburialrecords2.FindAsync(id);
            if (burialdata != null)
            {
                _context.finalburialrecords2.Remove(burialdata);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool finalburialrecords2Exists(Int64 id)
        {
            return (_context.finalburialrecords2?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
