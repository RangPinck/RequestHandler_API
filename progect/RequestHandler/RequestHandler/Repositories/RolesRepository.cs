using RequestHandler.Interfaces;
using RequestHandler.Models;

namespace RequestHandler.Repositories
{
    public class RolesRepository : IRolesRepository
    {
        private readonly RequestHandlerContext _context;

        public RolesRepository(RequestHandlerContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Role>> GetRoles()
        {
            return _context.Roles.OrderBy(r => r.Title).ToList();
        }
    }
}
