using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechnologyWK.Data;
using TechnologyWK.Models;

namespace TechnologyWK.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ProductsController : Controller
    {
        private static readonly string[] Summaries = new[]
        {
            "Testando", "Teste"
        };
        private readonly Context _context;

        public ProductsController(Context context)
        {
            _context = context;
        }

        // GET: Products
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var ret = _context.Products.Include(e => e.CategoriaNavigation).AsNoTracking();
            return View(ret);      
        }

        // GET: Products/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // GET: Products/Create
        [HttpGet]
        public IActionResult Create()
        {
            var list = new SelectList(_context.Categories.ToList(), "Id", "NameCategory");
            ViewBag.List = list;
            return View();
        }

        // POST: Products/Create  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,Ncm,PriceCost,PriceSale,Categoria")] Products products)
        {
            _context.Add(products);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            return View(products);
        }

        // GET: Products/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null || _context.Products == null)
            {
                return NotFound();
            }


            var products = await _context.Products.FindAsync(id);

            var list = new SelectList(_context.Categories.ToList(), "Id", "NameCategory", products.IdCategory);
            ViewBag.List = list;

            if (products == null)
            {
                return NotFound();
            }
            return View(products);
        }

        // POST: Products/Edit/5    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,Ncm,PriceCost,PriceSale,Categoria")] Products products)
        {

            var list = new SelectList(_context.Categories.ToList(), "Id", "NameCategory", products.IdCategory);
            ViewBag.List = list;

            if (id != products.Id)
            {
                return NotFound();
            }


            try
            {
                _context.Update(products);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductsExists(products.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));

            return View(products);
        }

        // GET: Products/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'Context.Products'  is null.");
            }
            var products = await _context.Products.FindAsync(id);
            if (products != null)
            {
                _context.Products.Remove(products);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductsExists(int id)
        {
            return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
