﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Resume.Data;
using Resume.Models;

namespace Resume.Controllers
{
    public class InformationController : Controller
    {
        private readonly ResumeContext _context;

        public InformationController(ResumeContext context)
        {
            _context = context;
        }

        // GET: Information
        public async Task<IActionResult> Index()
        {
              return _context.Information != null ? 
                          View(await _context.Information.ToListAsync()) :
                          Problem("Entity set 'ResumeContext.Information'  is null.");
        }

        // GET: Information/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Information == null)
            {
                return NotFound();
            }

            var information = await _context.Information
                .FirstOrDefaultAsync(m => m.info_id == id);
            if (information == null)
            {
                return NotFound();
            }

            return View(information);
        }

        // GET: Information/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Information/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("info_id,name,email,address,password,summary,designation,porfolio_name,portfolio_link,number")] Information information)
        {
            if (ModelState.IsValid)
            {
                _context.Add(information);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(information);
        }

        // GET: Information/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Information == null)
            {
                return NotFound();
            }

            var information = await _context.Information.FindAsync(id);
            if (information == null)
            {
                return NotFound();
            }
            return View(information);
        }

        // POST: Information/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("info_id,name,email,address,password,summary,designation,porfolio_name,portfolio_link,number")] Information information)
        {
            if (id != information.info_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(information);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InformationExists(information.info_id))
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
            return View(information);
        }

        // GET: Information/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Information == null)
            {
                return NotFound();
            }

            var information = await _context.Information
                .FirstOrDefaultAsync(m => m.info_id == id);
            if (information == null)
            {
                return NotFound();
            }

            return View(information);
        }

        // POST: Information/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Information == null)
            {
                return Problem("Entity set 'ResumeContext.Information'  is null.");
            }
            var information = await _context.Information.FindAsync(id);
            if (information != null)
            {
                _context.Information.Remove(information);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InformationExists(int id)
        {
          return (_context.Information?.Any(e => e.info_id == id)).GetValueOrDefault();
        }
    }
}
