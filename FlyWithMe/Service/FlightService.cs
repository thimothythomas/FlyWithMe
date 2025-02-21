using System.Collections.Generic;
using System.Threading.Tasks;
using FlyWithMe.Repository;
using FlyWithMe.Model;

namespace FlyWithMe.Service
{
    public class FlightService : IFlightService
    {
        private readonly IFlightRepository _flightRepository;

        public FlightService(IFlightRepository flightRepository)
        {
            _flightRepository = flightRepository;
        }

        public async Task<List<Flight>> GetFlightsAsync()
        {
            return await _flightRepository.GetFlightsAsync();
        }

        public async Task<Flight> GetFlightByIdAsync(int id)
        {
            return await _flightRepository.GetFlightByIdAsync(id);
        }

        public async Task<bool> AddFlightAsync(Flight flight)
        {
            return await _flightRepository.AddFlightAsync(flight);
        }

        public async Task<bool> UpdateFlightAsync(Flight flight)
        {
            return await _flightRepository.UpdateFlightAsync(flight);
        }
    }
}
