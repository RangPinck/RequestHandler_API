using RequestHandler.Interfaces;
using RequestHandler.Models;

namespace RequestHandler.Repositories
{
    public class StatusRepository : IStatusRepository
    {
        private readonly RequestHandlerContext _context;

        public StatusRepository(RequestHandlerContext context) 
        {
            _context = context;
        }
        public async Task<ICollection<Status>> GetStatuses()
        {
            return _context.Statuses.OrderBy(s => s.Title).ToList();
        }
    }
}
