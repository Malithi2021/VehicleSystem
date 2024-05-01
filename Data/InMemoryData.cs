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

        public bool AddWestminsterRentalVehicle(Vehicle v)
        {
            if (_vehicles.Any(vehicle => vehicle.RegistrationNumber == v.RegistrationNumber))
            {
                return false; // Vehicle with the same registration number already exists
            }

            _vehicles.Add(v);
            return true;
        }

        public bool RemoveVehicle(Vehicle vehicle)
        {
            return _vehicles.Remove(vehicle);
        }

        public List<Vehicle> GetAllVehicles()
        {
            return _vehicles;
        }

        public List<Vehicle> GetOrderedVehicles()
        {
            return _vehicles.OrderBy(v => v.Make).ToList();
        }

        public bool AddBooking(Booking booking)
        {
            if (_bookings.Any(b => b.Vehicle.RegistrationNumber == booking.Vehicle.RegistrationNumber
                                  && b.Schedule.Overlaps(booking.Schedule)))
            {
                return false;
            }
            _bookings.Add(booking);
            return true;
        }

        public bool RemoveBooking(Vehicle vehicle, Schedule schedule)
        {
            var bookingToRemove = _bookings.FirstOrDefault(b => b.Vehicle.RegistrationNumber == vehicle.RegistrationNumber
                                                                 && b.Schedule.Equals(schedule));
            if (bookingToRemove != null)
            {
                _bookings.Remove(bookingToRemove);
                return true;
            }

            return false;
        }

        public List<Vehicle> GetAvailableVehicles(Schedule wantedSchedule, Type type)
        {
            return _vehicles.Where(v => v.GetType() == type && !_bookings.Any(b => b.Vehicle == v && b.Schedule.Overlaps(wantedSchedule))).ToList();
        }

       
        public bool CheckAvailability(Vehicle vehicle, Schedule schedule)
        {
            return !_bookings.Any(b => b.Vehicle == vehicle && b.Schedule.Overlaps(schedule));
        }

        public void UpdateBookingSchedule(Vehicle vehicle, Schedule oldSchedule, Schedule newSchedule)
        {
            var bookingToUpdate = _bookings.FirstOrDefault(b => b.Vehicle == vehicle && b.Schedule == oldSchedule);
            if (bookingToUpdate != null)
            {
                bookingToUpdate.Schedule = newSchedule;
            }
        }

        public List<Report> GenerateReportData()
        {
            var reportData = new List<Report>();
            foreach (var vehicle in _vehicles)
            {
                var vehicleBookings = _bookings.Where(b => b.Vehicle == vehicle).OrderBy(b => b.Schedule.PickUpDate);
                var vehicleReportData = new Report { Vehicle = vehicle, Bookings = vehicleBookings.ToList() };
                reportData.Add(vehicleReportData);
            }
            return reportData;
        }

        public Vehicle GetVehicleByNumber(string number)
        {
            return _vehicles.FirstOrDefault(v => v.RegistrationNumber == number);
        }

        public decimal CalculateTotalPrice(Vehicle vehicle, Schedule schedule)
        {
           
            TimeSpan duration = schedule.DropOffDate.Date - schedule.PickUpDate.Date;

        
            decimal totalPrice = (decimal)(duration.Days * (double)vehicle.DailyRentalPrice);
            return totalPrice;
        }
    }
}
