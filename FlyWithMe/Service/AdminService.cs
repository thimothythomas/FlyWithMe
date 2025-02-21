using System.Threading.Tasks;
using FlyWithMe.Repository;

namespace FlyWithMe.Service
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;

        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            return await _adminRepository.ValidateAdminAsync(username, password);
        }
    }
}
