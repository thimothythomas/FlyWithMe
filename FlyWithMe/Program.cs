using System;
using System.Threading.Tasks;
using FlyWithMe.Service;
using FlyWithMe.Repository;

namespace FlyWithMe
{
    class Program
    {
        static async Task Main()
        {
            // Dependency Injection
            IAdminRepository adminRepo = new AdminRepository();
            IAdminService adminService = new AdminService(adminRepo);




            // Login Authentication
            Console.Write("Enter Admin Username: ");
            string username = Console.ReadLine();

            Console.Write("Enter Admin Password: ");
            string password = ReadPassword(); // Hide password input

            bool isAuthenticated = await adminService.LoginAsync(username, password);

            if (!isAuthenticated)
            {
                Console.WriteLine("\nInvalid Credentials. Access Denied.");
                return; // Exit the application
            }

            Console.WriteLine("\nLogin Successful! Welcome, Admin.\n");

            // Dependency Injection for Flights
            IFlightRepository flightRepo = new FlightRepository();
            IFlightService flightService = new FlightService(flightRepo);

            while (true)
            {
                Console.WriteLine("\n===== Flight Management Menu =====");
                Console.WriteLine("1. List All Flights");
                Console.WriteLine("2. Search Flight by ID");
                Console.WriteLine("3. Add Flight");
                Console.WriteLine("4. Edit Flight");
                Console.WriteLine("5. Exit");
                Console.Write("Choose an option: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        var flights = await flightService.GetFlightsAsync();
                        flights.ForEach(f => Console.WriteLine($"{f.FlightId}: {f.DepAirport} -> {f.ArrAirport} on {f.DepDate}"));
                        break;

                    case 2:
                        Console.Write("Enter Flight ID: ");
                        if (int.TryParse(Console.ReadLine(), out int flightId))
                        {
                            var flight = await flightService.GetFlightByIdAsync(flightId);
                            if (flight != null)
                            {
                                Console.WriteLine($"Flight {flight.FlightId}: {flight.DepAirport} -> {flight.ArrAirport}, {flight.DepDate}");
                            }
                            else
                            {
                                Console.WriteLine("Flight not found.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid ID format.");
                        }
                        break;

                    case 3:
                        Console.Write("Enter Departure Airport: ");
                        string depAirport = Console.ReadLine();

                        Console.Write("Enter Arrival Airport: ");
                        string arrAirport = Console.ReadLine();

                        Console.Write("Enter Departure Date (YYYY-MM-DD): ");
                        string depDate = Console.ReadLine();

                        var newFlight = new Model.Flight
                        {
                            DepAirport = depAirport,
                            ArrAirport = arrAirport,
                            DepDate = DateTime.Parse(depDate)
                        };

                        bool added = await flightService.AddFlightAsync(newFlight);
                        Console.WriteLine(added ? "Flight Added Successfully!" : "Failed to Add Flight.");
                        break;

                    case 4:
                        Console.Write("Enter Flight ID to Edit: ");
                        if (int.TryParse(Console.ReadLine(), out int editFlightId))
                        {
                            var flightToUpdate = await flightService.GetFlightByIdAsync(editFlightId);
                            if (flightToUpdate != null)
                            {
                                Console.Write("Enter New Departure Airport: ");
                                flightToUpdate.DepAirport = Console.ReadLine();

                                Console.Write("Enter New Arrival Airport: ");
                                flightToUpdate.ArrAirport = Console.ReadLine();

                                bool updated = await flightService.UpdateFlightAsync(flightToUpdate);
                                Console.WriteLine(updated ? "Flight Updated Successfully!" : "Failed to Update Flight.");
                            }
                            else
                            {
                                Console.WriteLine("Flight not found.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid ID format.");
                        }
                        break;

                    case 5:
                        Console.WriteLine("Exiting... Goodbye!");
                        return;

                    default:
                        Console.WriteLine("Invalid choice! Please select a valid option.");
                        break;
                }
            }
        }

        // Method to Hide Password Input
        private static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*"); // Mask input with '*'
                }
            } while (key.Key != ConsoleKey.Enter);
            Console.WriteLine();
            return password;
        }
    }
}
