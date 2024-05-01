using VehicleSystem.Models;

namespace VehicleSystem.Interfaces
{
    public interface IRentalCustomer
    {
      public void ListAvailableVehicles(Schedule wantedSchedule, Type type);
        public bool AddReservation(string number, Schedule wantedSchedule, Driver driver);
        public bool ChangeReservation(string number, Schedule oldSchedule, Schedule newSchedule);
        public bool DeleteReservation(string number, Schedule schedule);

        public decimal CalculateTotalPrice(Vehicle vehicle, Schedule schedule);
    }
}
