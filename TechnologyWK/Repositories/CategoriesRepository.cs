using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnologyWK.Data;
using TechnologyWK.Models;

namespace TechnologyWK.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        public readonly Context _context;

        public CategoriesRepository(Context context)
        {
            _context = context; 
        }
        public async Task<Categories> Create(Categories categories)
        {
            _context.Add(categories);
            await _context.SaveChangesAsync();

            return  categories;
        }

        public async Task Delete(int id)
        {
            var categoryDelete = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(categoryDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Categories>> Get()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Categories> Get(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task Update(Categories category)
        {
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();  
        }
    }
}
