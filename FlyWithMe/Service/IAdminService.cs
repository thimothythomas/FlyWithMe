using System.Threading.Tasks;

namespace FlyWithMe.Service
{
    public interface IAdminService
    {
        Task<bool> LoginAsync(string username, string password);
    }
}
