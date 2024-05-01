using VehicleSystem.Data;
using VehicleSystem.Interfaces;
using VehicleSystem.Models;

namespace VehicleSystem.Services
{

    public class RentalCustomer : IRentalCustomer
    {
        private readonly InMemoryData _data;
        public RentalCustomer(InMemoryData data)
        {
            _data = data;
        }

        public void ListAvailableVehicles(Schedule wantedSchedule, Type type)
        {
            var availableVehicles = _data.GetAvailableVehicles(wantedSchedule, type);
        }

        public decimal CalculateTotalPrice(Vehicle vehicle, Schedule schedule)
        {
            // Calculate total price based on vehicle daily rental price and duration of reservation
            int days = (int)(schedule.DropOffDate - schedule.PickUpDate).TotalDays;
            decimal totalPrice = (decimal)(vehicle.DailyRentalPrice * days);
            return totalPrice;
        }

        public bool AddReservation(string number, Schedule wantedSchedule, Driver driver)
        {
            var vehicle = _data.GetVehicleByNumber(number);
            if (vehicle != null)
            {
                if (_data.CheckAvailability(vehicle, wantedSchedule))
                {
                    var totalPrice = _data.CalculateTotalPrice(vehicle, wantedSchedule);
                    var reservation = new Booking(vehicle, driver, wantedSchedule);
                    _data.AddBooking(reservation);
                    return true;
                }
                else
                {
                    // Handle reservation not available
                    return false;
                }
            }
            else
            {
                // Handle invalid vehicle number
                return false;
            }
        }

        public bool ChangeReservation(string number, Schedule oldSchedule, Schedule newSchedule)
        {
            var vehicle = _data.GetVehicleByNumber(number);
            if (vehicle != null)
            {
                if (_data.CheckAvailability(vehicle, newSchedule))
                {
                    _data.UpdateBookingSchedule(vehicle, oldSchedule, newSchedule);
                    return true;
                }
                else
                {
                    // Handle overlapping schedules
                    return false;
                }
            }
            else
            {
                // Handle invalid vehicle number
                return false;
            }
        }

        public bool DeleteReservation(string number, Schedule schedule)
        {
            var vehicle = _data.GetVehicleByNumber(number);
            if (vehicle != null)
            {
                _data.RemoveBooking(vehicle, schedule);
                return true;
            }
            else
            {
                // Handle invalid vehicle number
                return false;
            }

        }

      
    }
}
