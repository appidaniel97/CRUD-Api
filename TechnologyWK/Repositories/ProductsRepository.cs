using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnologyWK.Data;
using TechnologyWK.Models;

namespace TechnologyWK.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        public readonly Context _context;

        public ProductsRepository(Context context)
        {
            _context = context;
        }
        public async Task<Products> Create(Products products)
        {
            _context.Add(products);
            await _context.SaveChangesAsync();

            return products;
        }

        public async Task Delete(int id)
        {
            var productsDelete = await _context.Products.FindAsync(id);
            _context.Products.Remove(productsDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Products>> Get()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Products> Get(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task Update(Products products)
        {
            _context.Entry(products).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
