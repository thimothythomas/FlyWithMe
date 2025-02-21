using System.Collections.Generic;
using System.Threading.Tasks;
using FlyWithMe.Model;

namespace FlyWithMe.Service
{
    public interface IFlightService
    {
        Task<List<Flight>> GetFlightsAsync();
        Task<Flight> GetFlightByIdAsync(int id);
        Task<bool> AddFlightAsync(Flight flight);
        Task<bool> UpdateFlightAsync(Flight flight);
    }
}
