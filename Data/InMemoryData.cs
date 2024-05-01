using VehicleSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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

       
        public bool AddVehicle(Vehicle vehicle)
        {
            if (_vehicles.Any(v => v.RegistrationNumber == vehicle.RegistrationNumber))
            {
                return false; // Vehicle with the same registration number already exists
            }

            _vehicles.Add(vehicle);
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
                return false; // Overlapping booking for the same vehicle
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

            return false; // Booking not found
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

      /*  public List<ReportData> GenerateReportData()
        {
            var reportData = new List<ReportData>();

            foreach (var vehicle in _vehicles)
            {
                var vehicleBookings = _bookings.Where(b => b.Vehicle == vehicle).OrderBy(b => b.Schedule.PickUpDate);
                var vehicleReportData = new ReportData { Vehicle = vehicle, Bookings = vehicleBookings.ToList() };
                reportData.Add(vehicleReportData);
            }

            return reportData;
        }*/

        public Vehicle GetVehicleByNumber(string number)
        {
            return _vehicles.FirstOrDefault(v => v.RegistrationNumber == number);
        }

        internal double CalculateTotalPrice(Vehicle vehicle, Schedule wantedSchedule)
        {
            throw new NotImplementedException();
        }
    }
}
