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
    [Produces("application/json")]
    public class CategoriesController : Controller
    {
        #region Fields
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly Context _context;
        #endregion

        #region Constructor
        public CategoriesController(ICategoriesRepository categoriesRepository, Context context)
        {
            _categoriesRepository = categoriesRepository;
            _context = context;
        }

        #endregion

        #region CRUD WEB APP
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {

            return _context.Categories != null ?
                         View(await _context.Categories.ToListAsync()) :
                         Problem("Entity set 'Context.Categories'  is null.");
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var categories = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categories == null)
            {
                return NotFound();
            }

            return View(categories);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet]
        public IActionResult Create()
        {
            var list = new SelectList(_context.Categories.ToList(), "Id", "NameCategory");
            ViewBag.List = list;
            return View();
        }

        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NameCategory")] Categories categories)
        {
            _context.Add(categories);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            return View(categories);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var categories = await _context.Categories.FindAsync(id);

            if (categories == null)
            {
                return NotFound();
            }
            return View(categories);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NameCategory")] Categories categories)
        {
            if (id != categories.Id)
            {
                return NotFound();
            }

            try
            {
                _context.Update(categories);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriesExists(categories.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));

            return View(categories);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var categories = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categories == null)
            {
                return NotFound();
            }

            return View(categories);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Categories == null)
            {
                return Problem("Entity set 'Context.Categories'  is null.");
            }
            var categories = await _context.Categories.FindAsync(id);
            if (categories != null)
            {
                _context.Categories.Remove(categories);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool CategoriesExists(int id)
        {
            return (_context.Categories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        #endregion

        #region CRUD SWAGGER API
        [HttpGet]
        public async Task<IEnumerable<Categories>> GetCategories()
        {
            return await _categoriesRepository.Get();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Categories>> GetAllCategories(int id)
        {
            return await _categoriesRepository.Get(id);
        }

        [HttpPost]
        public async Task<ActionResult<Categories>> PostCategories([FromBody] Categories categories)
        {
            var newCategory = await _categoriesRepository.Create(categories);
            return categories;
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteCategories(int id)
        {
            var deleteCategory = await _categoriesRepository.Get(id);

            if(deleteCategory == null)
                return NotFound();
            
                
            await _categoriesRepository.Delete(deleteCategory.Id);
            return NoContent();

        }

        [HttpPut]
        public async Task<ActionResult> PutCategories(int id ,[FromBody] Categories categories)
        {
            if(id != categories.Id)
                return BadRequest();

            await _categoriesRepository.Update(categories);
            return NoContent();
        }
        #endregion
    }
}
