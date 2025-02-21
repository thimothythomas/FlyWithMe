using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using FlyWithMe.Utility;
using FlyWithMe.Model;
using ClassLibraryDataBaseConnection;
using System.Configuration;

namespace FlyWithMe.Repository
{
    public class FlightRepository : IFlightRepository
    {
        public async Task<List<Flight>> GetFlightsAsync()
        {
            List<Flight> flights = new List<Flight>();
            string inConnection = ConfigurationManager.ConnectionStrings["CsWin"].ConnectionString;

            using (SqlConnection conn = DatabaseConnection.OpenConnection(inConnection))
            {
                if (conn == null)
                    return flights;

                using (SqlCommand cmd = new SqlCommand("sp_GetAllFlights", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            flights.Add(new Flight
                            {
                                FlightId = reader.GetInt32(0),
                                DepAirport = reader.GetString(1),
                                ArrAirport = reader.GetString(2),
                                DepDate = reader.GetDateTime(3),
                                ArrDate = reader.GetDateTime(4),
                            });
                        }
                    }
                }
            }
            return flights;
        }

        public async Task<Flight> GetFlightByIdAsync(int id)
        {
            string inConnection = ConfigurationManager.ConnectionStrings["CsWin"].ConnectionString;

            using (SqlConnection conn = DatabaseConnection.OpenConnection(inConnection))
            {
                if (conn == null)
                    return null;

                using (SqlCommand cmd = new SqlCommand("sp_GetFlightById", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FlightId", id);

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {
                            return new Flight
                            {
                                FlightId = reader.GetInt32(0),
                                DepAirport = reader.GetString(1),
                                ArrAirport = reader.GetString(2),
                                DepDate = reader.GetDateTime(3),
                                ArrDate = reader.GetDateTime(4),
                            };
                        }
                    }
                }
            }
            return null;
        }

        public async Task<bool> AddFlightAsync(Flight flight)
        {
            string inConnection = ConfigurationManager.ConnectionStrings["CsWin"].ConnectionString;

            using (SqlConnection conn = DatabaseConnection.OpenConnection(inConnection))
            {
                if (conn == null)
                    return false;

                using (SqlCommand cmd = new SqlCommand("sp_AddFlight", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DepAirport", flight.DepAirport);
                    cmd.Parameters.AddWithValue("@ArrAirport", flight.ArrAirport);
                    cmd.Parameters.AddWithValue("@DepDate", flight.DepDate);
                    cmd.Parameters.AddWithValue("@ArrDate", flight.ArrDate);

                    int rowsAffected = await cmd.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }

        public async Task<bool> UpdateFlightAsync(Flight flight)
        {
            string inConnection = ConfigurationManager.ConnectionStrings["CsWin"].ConnectionString;

            using (SqlConnection conn = DatabaseConnection.OpenConnection(inConnection))
            {
                if (conn == null)
                    return false;

                using (SqlCommand cmd = new SqlCommand("sp_UpdateFlight", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FlightId", flight.FlightId);
                    cmd.Parameters.AddWithValue("@DepAirport", flight.DepAirport);
                    cmd.Parameters.AddWithValue("@ArrAirport", flight.ArrAirport);
                    cmd.Parameters.AddWithValue("@DepDate", flight.DepDate);
                    cmd.Parameters.AddWithValue("@ArrDate", flight.ArrDate);

                    int rowsAffected = await cmd.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
