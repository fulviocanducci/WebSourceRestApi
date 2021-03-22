using WebSourceRestApi.Services;

namespace WebSourceRestApi.Repositories
{

    public class TodoRepository : TodoRepositoryAbstract
    {
        public TodoRepository(DatabaseService service) : base(service)
        {
        }
    }
}
