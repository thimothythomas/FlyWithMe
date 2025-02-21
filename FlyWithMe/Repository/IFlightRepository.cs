using System.Collections.Generic;
using System.Threading.Tasks;
using FlyWithMe.Model;

namespace FlyWithMe.Repository
{
    public interface IFlightRepository
    {
        Task<List<Flight>> GetFlightsAsync();
        Task<Flight> GetFlightByIdAsync(int id);
        Task<bool> AddFlightAsync(Flight flight);
        Task<bool> UpdateFlightAsync(Flight flight);
    }
}
