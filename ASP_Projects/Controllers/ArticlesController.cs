using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASP_Projects.Data;
using ASP_Projects.Models;
using Microsoft.AspNetCore.Authorization;

namespace ASP_Projects.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class ArticlesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArticlesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
              return _context.Articles != null ? 
                          View(await _context.Articles.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Articles'  is null.");
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Articles == null)
            {
                return NotFound();
            }

            var articles = await _context.Articles
                .FirstOrDefaultAsync(m => m.ID == id);
            if (articles == null)
            {
                return NotFound();
            }

            return View(articles);
        }

        // GET: Articles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Content,Title,Description")] Articles articles)
        {
            if (ModelState.IsValid)
            {
                _context.Add(articles);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(articles);
        }

        // GET: Articles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Articles == null)
            {
                return NotFound();
            }

            var articles = await _context.Articles.FindAsync(id);
            if (articles == null)
            {
                return NotFound();
            }
            return View(articles);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Content,Title,Description")] Articles articles)
        {
            if (id != articles.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(articles);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticlesExists(articles.ID))
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
            return View(articles);
        }

        // GET: Articles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Articles == null)
            {
                return NotFound();
            }

            var articles = await _context.Articles
                .FirstOrDefaultAsync(m => m.ID == id);
            if (articles == null)
            {
                return NotFound();
            }

            return View(articles);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Articles == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Articles'  is null.");
            }
            var articles = await _context.Articles.FindAsync(id);
            if (articles != null)
            {
                _context.Articles.Remove(articles);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticlesExists(int id)
        {
          return (_context.Articles?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
