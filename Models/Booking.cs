namespace VehicleSystem.Models
{
    public class Booking
    {
        public Vehicle Vehicle { get; set; }
        public Driver Driver { get; set; }
        public Schedule Schedule { get; set; }
        public double TotalPrice { get; set; }

        public Booking(Vehicle vehicle, Driver driver, Schedule schedule)
        {
            Vehicle = vehicle;
            Driver = driver;
            Schedule = schedule;
            CalculateTotalPrice();
        }

        private void CalculateTotalPrice()
        {
            TimeSpan duration = Schedule.DropOffDate.Date - Schedule.PickUpDate.Date;
            TotalPrice = (double)(duration.Days * Vehicle.DailyRentalPrice);
        }
    }
}
