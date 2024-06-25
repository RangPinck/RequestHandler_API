using RequestHandler.Models;

namespace RequestHandler.Interfaces
{
    public interface IRolesRepository
    {
        //получнеие списка ролей
        Task<ICollection<Role>> GetRoles();
    }
}
