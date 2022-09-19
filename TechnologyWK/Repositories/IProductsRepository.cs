using System.Collections.Generic;
using System.Threading.Tasks;
using TechnologyWK.Models;

namespace TechnologyWK.Repositories
{
    public interface IProductsRepository
    {
        Task<IEnumerable<Products>> Get();

        Task<Products> Get(int id);

        Task<Products> Create(Products products);

        Task Update(Products products);

        Task Delete(int id);
    }
}
