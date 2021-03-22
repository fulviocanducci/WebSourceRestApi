using WebSourceRestApi.Models;
using WebSourceRestApi.Repositories.Bases;
using WebSourceRestApi.Services;

namespace WebSourceRestApi.Repositories
{
    public abstract class TodoRepositoryAbstract : Repository<Todo>, IRepository<Todo>
    {
        protected TodoRepositoryAbstract(DatabaseService service) : base(service)
        {
        }
    }
}
