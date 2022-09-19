using System.Collections.Generic;
using System.Threading.Tasks;
using TechnologyWK.Models;

namespace TechnologyWK.Repositories
{
    public interface ICategoriesRepository
    {
        Task<IEnumerable<Categories>> Get();

        Task<Categories> Get(int id);

        Task<Categories> Create(Categories category);

        Task Update(Categories category);

        Task Delete(int id);
    }
}
