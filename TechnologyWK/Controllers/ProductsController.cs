using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechnologyWK.Data;
using TechnologyWK.Models;
using TechnologyWK.Repositories;

namespace TechnologyWK.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ProductsController : Controller
    {
        #region Fields
        private readonly IProductsRepository _productsRepository;
        private readonly Context _context;
        #endregion

        #region Construtor
        public ProductsController(IProductsRepository productsRepository,Context context)
        {
            _productsRepository = productsRepository;
            _context = context;
        }
        #endregion

        #region CRUD WEB APP
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var  ret =  _context.Products.Include(e => e.CategoriaNavigation).AsNoTracking();
            return View(ret);      
        }

        [ApiExplorerSettings(IgnoreApi = true)]
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

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet]
        public IActionResult Create()
        {
            var list = new SelectList(_context.Categories.ToList(), "Id", "NameCategory");
            ViewBag.List = list;
            return View();
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,Ncm,PriceCost,PriceSale,Categoria")] Products products)
        {
            _context.Add(products);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            return View(products);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
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

        [ApiExplorerSettings(IgnoreApi = true)]
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

        [ApiExplorerSettings(IgnoreApi = true)]
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

        [ApiExplorerSettings(IgnoreApi = true)]
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
        #endregion

        #region CRUD SWAGGER API
        [HttpGet]
        public async Task<IEnumerable<Products>> GetProducts()
        {
            return await _productsRepository.Get();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Products>> GetAllProducts(int id)
        {
            return await _productsRepository.Get(id);
        }

        [HttpPost]
        public async Task<ActionResult<Products>> PostProducts([FromBody] Products products)
        {
            var newProduct = await _productsRepository.Create(products);
            return products;
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteProducts(int id)
        {
            var deleteProducts = await _productsRepository.Get(id);

            if (deleteProducts == null)
                return NotFound();


            await _productsRepository.Delete(deleteProducts.Id);
            return NoContent();

        }

        [HttpPut]
        public async Task<ActionResult> PutProducts(int id, [FromBody] Products products)
        {
            if (id != products.Id)
                return BadRequest();

            await _productsRepository.Update(products);
            return NoContent();
        }
        #endregion
    }
}
