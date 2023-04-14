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
    public class UsersController : Controller
    {
        private readonly RDSContext _context;

        public UsersController(RDSContext context)
        {
            _context = context;
        }

        // GET: Mummies
        [Authorize]
        //public async Task<IActionResult> Index()
        //{
        //    return _context.Users != null ?
        //                View(await _context.Users.ToListAsync()) :
        //                Problem("Entity set 'RDSContext.Users'  is null.");
        //}

        public async Task<IActionResult> Index()
        {
            var users = await _context.Users.Select(u => new INTEXII.Models.Users
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email
            }).ToListAsync();

            return View(users);
        }

        // GET: Mummies/Details/5
        //public async Task<IActionResult> Details(string Id)
        //{
        //    if (Id == null || _context.Users == null)
        //    {
        //        return NotFound();
        //    }

        //    var users = await _context.Users
        //        .FirstOrDefaultAsync(m => m.Id == Id);
        //    if (users == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(use
        //    rs);
        //}

        public async Task<IActionResult> Details(string Id)
{
    if (Id == null)
    {
        return NotFound();
    }

    var user = await _context.Users.FindAsync(Id);

    if (user == null)
    {
        return NotFound();
    }

    var userModel = new INTEXII.Models.Users { 
        Id = user.Id, 
        UserName = user.UserName, 
        Email = user.Email 
    };

    return View(userModel);
}

        //// GET: Mummies/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Mummies/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,UserName,Email")] Users mummies)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(mummies);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(mummies);
        //}

        // GET: Mummies/Edit/5
        //public async Task<IActionResult> Edit(string Id)
        //{
        //    if (Id == null || _context.Users == null)
        //    {
        //        return NotFound();
        //    }

        //    var mummies = await _context.Users.FindAsync(Id);
        //    if (mummies == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(mummies);
        //}
        public async Task<IActionResult> Edit(string Id)
        {
            if (Id == null || _context.Users == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == Id);
            if (users == null)
            {
                return NotFound();
            }

            var userModel = new INTEXII.Models.Users
            {
                Id = users.Id,
                UserName = users.UserName,
                Email = users.Email
            };

            return View(userModel);
        }

        // POST: Mummies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(string Id, [Bind("Id,UserName,Email")] Users users)
        //{
        //    if (Id != users.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(users);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!MummiesExists(users.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(users);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string Id, [Bind("Id,UserName,Email")] Users model)
        {
            if (Id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _context.Users.FindAsync(Id);
                    user.UserName = model.UserName;
                    user.Email = model.Email;
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MummiesExists(model.Id))
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
            return View(model);
        }

        //// GET: Mummies/Delete/5
        //public async Task<IActionResult> Delete(string Id)
        //{
        //    if (Id == null || _context.Users == null)
        //    {
        //        return NotFound();
        //    }

        //    var users = await _context.Users
        //        .FirstOrDefaultAsync(m => m.Id == Id);
        //    if (users == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(users);
        //}
        // GET: Mummies/Delete/5
        public async Task<IActionResult> Delete(string Id)
        {
            if (Id == null || _context.Users == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == Id);

            if (users == null)
            {
                return NotFound();
            }

            var userModel = new INTEXII.Models.Users
            {
                Id = users.Id,
                UserName = users.UserName,
                Email = users.Email
            };

            return View(userModel);
        }



        //// POST: Mummies/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(string Id)
        //{
        //    if (_context.Users == null)
        //    {
        //        return Problem("Entity set 'RDSContext.users'  is null.");
        //    }
        //    var users = await _context.Users.FindAsync(Id);
        //    if (users != null)
        //    {
        //        _context.Users.Remove(users);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string Id)
        {
            if (Id == null || _context.Users == null)
            {
                return NotFound();
            }
            var users = await _context.Users.FindAsync(Id);
            if (users == null)
            {
                return NotFound();
            }

            _context.Users.Remove(users);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool MummiesExists(string Id)
        {
            return (_context.Users?.Any(e => e.Id == Id)).GetValueOrDefault();
        }
    }
}