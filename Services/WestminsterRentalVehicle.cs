using VehicleSystem.Data;
using VehicleSystem.Interfaces;
using VehicleSystem.Models;

namespace VehicleSystem.Services
{
    public class WestminsterRentalVehicle : IRentalManager, IRentalCustomer
    {
        private List<Vehicle> vehicles = new List<Vehicle>();
        private InMemoryData _inMemoryData;  // Instance of InMemoryData class to interact with data

        public WestminsterRentalVehicle(InMemoryData data)
        {
            _inMemoryData = data;
            vehicles = _inMemoryData.GetAllVehicles();
        }

        // Method to add a vehicle
        public bool AddVehicle(Vehicle v)
        {
            if (_inMemoryData.AddWestminsterRentalVehicle(v))
            {
                Console.WriteLine($"Vehicle with registration number {v.RegistrationNumber} added successfully.");
                Console.WriteLine($"Number of available parking lots: {50 - _inMemoryData.GetAllVehicles().Count}");
                return true;
            }
            else
            {
                Console.WriteLine($"Vehicle with registration number {v.RegistrationNumber} already exists.");
                return false;
            }
        }

        // Method to delete a vehicle
        public bool DeleteVehicle(string number)
        {
            var vehicleToDelete = _inMemoryData.GetVehicleByNumber(number);
            if (vehicleToDelete != null)
            {
                _inMemoryData.RemoveVehicle(vehicleToDelete);
                Console.WriteLine($"Vehicle with registration number {vehicleToDelete.RegistrationNumber}, Make: {vehicleToDelete.Make}, Model: {vehicleToDelete.Model} deleted successfully.");
                Console.WriteLine($"Number of available parking lots: {50 - _inMemoryData.GetAllVehicles().Count}");
                return true;
            }
            else
            {
                Console.WriteLine($"Vehicle with registration number {number} not found.");
                return false;
            }
        }


        // Method to generate a report of all vehicles and their bookings
        public void GenerateReport(string fileName)
        {
            string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "Reports");
            string filePath = Path.Combine(directoryPath, fileName);

            // Ensure the Reports folder exists in the project directory
            Directory.CreateDirectory(directoryPath);

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var vehicle in vehicles)
                {
                    // Sort the bookings for the current vehicle by start date in ascending order
                    var sortedBookings = vehicle.Reservations.OrderBy(b => b.Schedule.PickUpDate);

                    writer.WriteLine($"Vehicle Information - Registration Number: {vehicle.RegistrationNumber}, Make: {vehicle.Make}, Model: {vehicle.Model}");

                    writer.WriteLine("Bookings:");
                    foreach (var booking in sortedBookings)
                    {
                        writer.WriteLine($"  - Start Date: {booking.Schedule.PickUpDate}, End Date: {booking.Schedule.DropOffDate}, Driver: {booking.Driver.FirstName} {booking.Driver.SurName}");
                    }
                    writer.WriteLine();
                }
            }
            Console.WriteLine($"Report generated successfully and saved as {fileName}");
        }

        // Method to list vehicles ordered by make
        public void ListOrderedVehicles()
        {
            var orderedVehicles = _inMemoryData.GetOrderedVehicles();
            Console.WriteLine("Ordered Vehicles:");
            foreach (var vehicle in orderedVehicles)
            {
                Console.WriteLine($"Make: {vehicle.Make}, Model: {vehicle.Model}, Registration Number: {vehicle.RegistrationNumber}, Daily Rental Price: {vehicle.DailyRentalPrice}");
            }
        }

        // Method to list all vehicles
        public void ListVehicles()
        {
            foreach (var vehicle in _inMemoryData.GetAllVehicles())
            {
                Console.WriteLine($"Registration Number: {vehicle.RegistrationNumber}, Type: {vehicle.GetType().Name}, Model: {vehicle.Model}");

                if (vehicle.Reservations.Count > 0)
                {
                    Console.WriteLine("Reservation Schedules:");
                    foreach (var booking in vehicle.Reservations)
                    {
                        Console.WriteLine($"  - Start Date: {booking.Schedule.PickUpDate}, End Date: {booking.Schedule.DropOffDate}, Driver: {booking.Driver.FirstName} {booking.Driver.SurName}");
                    }
                }
                else
                {
                    Console.WriteLine("No reservations for this vehicle.");
                }
            }
        }


        // Method to list available vehicles for a specified schedule and type
        public void ListAvailableVehicles(Schedule wantedSchedule, Type type)
        {
            var availableVehicles = _inMemoryData.GetAvailableVehicles(wantedSchedule, type);
            if (availableVehicles.Count == 0)
            {
                Console.WriteLine("No vehicles available for the specified schedule and type.");
            }
            else
            {
                foreach (var vehicle in availableVehicles)
                {
                    vehicle.DisplayInfo();
                }
            }
        }

        // Method to calculate the total price for a given vehicle and schedule
        public decimal CalculateTotalPrice(Vehicle vehicle, Schedule schedule)
        {
            TimeSpan duration = schedule.DropOffDate - schedule.PickUpDate; 
            decimal totalPrice = (decimal)(duration.TotalDays * (double)vehicle.DailyRentalPrice); 
            return totalPrice;
        }

        // Method to add a reservation
        public bool AddReservation(string number, Schedule wantedSchedule, Type type, Driver driver)
        {
            var vehicle = vehicles.Find(v => v.RegistrationNumber == number && v.GetType() == type);
            if (vehicle != null)
            {
                if (vehicle.IsAvailable(wantedSchedule))
                {
                    var totalPrice = CalculateTotalPrice(vehicle, wantedSchedule);
                    var reservation = new Booking(vehicle, driver, wantedSchedule);
                    reservation.TotalPrice = (double)totalPrice; 
                    vehicle.Reservations.Add(reservation);
                    _inMemoryData.AddBooking(reservation);
                    Console.WriteLine("Reservation added successfully.");
                    Console.WriteLine($"Total Price is: {reservation.TotalPrice}");
                    Console.WriteLine($"Pick-up Date: {wantedSchedule.PickUpDate}, Drop-off Date: {wantedSchedule.DropOffDate}");
                    return true;
                }
                else
                {
                    Console.WriteLine("The vehicle is not available for the specified schedule.");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Invalid vehicle number or type.");
                return false;
            }
        }

        // Method to change a reservation
        public bool ChangeReservation(string number, Schedule oldSchedule, Schedule newSchedule)
        {
            var vehicle = vehicles.Find(v => v.RegistrationNumber == number);
            if (vehicle != null)
            {
                var reservation = vehicle.Reservations.Find(r => r.Schedule.PickUpDate == oldSchedule.PickUpDate && r.Schedule.DropOffDate == oldSchedule.DropOffDate);
                if (reservation != null)
                {
                    if (vehicle.IsAvailable(newSchedule))
                    {
                        reservation.Schedule = newSchedule;
                        reservation.TotalPrice = (double)CalculateTotalPrice(vehicle, newSchedule);
                        Console.WriteLine("Reservation updated successfully.");
                        Console.WriteLine($"Updated pick-up date: {newSchedule.PickUpDate}");
                        Console.WriteLine($"Updated drop-off date: {newSchedule.DropOffDate}");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("The new schedule overlaps with existing bookings.");
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine("No reservation found for the specified schedule.");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Invalid vehicle number.");
                return false;
            }
        }

        // Method to delete a reservation
        public bool DeleteReservation(string number, Schedule schedule)
        {
            var vehicle = vehicles.Find(v => v.RegistrationNumber == number);
            if (vehicle != null)
            {
                var reservationToRemove = vehicle.Reservations.Find(r => r.Schedule.PickUpDate == schedule.PickUpDate && r.Schedule.DropOffDate == schedule.DropOffDate);
                if (reservationToRemove != null)
                {
                    vehicle.Reservations.Remove(reservationToRemove);
                    Console.WriteLine("Reservation deleted successfully.");
                    return true;
                }
                else
                {
                    Console.WriteLine("No reservation found for the specified schedule.");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Invalid vehicle number.");
                return false;
            }
        }
    }
}
