using VehicleSystem.Models;

namespace VehicleSystem.Data
{
    public class InMemoryData
    {
        private List<Vehicle> _vehicles;
        private List<Booking> _bookings;
      
        public InMemoryData()
        {
            _vehicles = new List<Vehicle>();
            _bookings = new List<Booking>();
        }

        // Method to add a vehicle to the list
        public bool AddWestminsterRentalVehicle(Vehicle v)
        {
            // Check if a vehicle with the same registration number already exists
            if (_vehicles.Any(vehicle => vehicle.RegistrationNumber == v.RegistrationNumber))
            {
                return false; 
            }

            _vehicles.Add(v);
            return true;
        }

        // Method to remove a vehicle from the list
        public bool RemoveVehicle(Vehicle vehicle)
        {
            return _vehicles.Remove(vehicle);
        }

       // Method to get all vehicles
        public List<Vehicle> GetAllVehicles()
        {
            return _vehicles;
        }

        // Method to get vehicles ordered by "make"
        public List<Vehicle> GetOrderedVehicles()
        {
            return _vehicles.OrderBy(v => v.Make).ToList();
        }

        // Method to add bookings
        public bool AddBooking(Booking booking)
        {
            // Check if there are any overlapping bookings for the same vehicle
            if (_bookings.Any(b => b.Vehicle.RegistrationNumber == booking.Vehicle.RegistrationNumber
                                      && b.Schedule.Overlaps(booking.Schedule)))
            {
                return false;
            }
            _bookings.Add(booking);
            return true;
        }

        // Method to get available vehicles for a specified schedule and type
        public List<Vehicle> GetAvailableVehicles(Schedule wantedSchedule, Type type)
        {
            // Return vehicles that are of the specified type and not booked during the wanted schedule
            return _vehicles.Where(v => v.GetType().IsSubclassOf(type) && !_bookings.Any(b => b.Vehicle == v && b.Schedule.Overlaps(wantedSchedule))).ToList();
        }

        // Method to get a vehicle by its registration number
        public Vehicle GetVehicleByNumber(string number)
        {
            return _vehicles.FirstOrDefault(v => v.RegistrationNumber == number);
        }

    }
}
