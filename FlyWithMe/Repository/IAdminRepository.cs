using System.Threading.Tasks;

namespace FlyWithMe.Repository
{
    public interface IAdminRepository
    {
        Task<bool> ValidateAdminAsync(string username, string password);
    }
}
