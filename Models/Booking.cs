namespace VehicleSystem.Models
{
    public class Booking
    {
        public Vehicle Vehicle { get; private set; }
        public Driver Driver { get; private set; }
        public Schedule Schedule { get; private set; }
        public double TotalPrice {  get; private set; }


        public Booking (Vehicle vehicle,Driver driver,Schedule schedule)
        {
            Vehicle = vehicle;
            Driver = driver;
            Schedule = schedule;
            CalculateTotalPrice();
        }

        private void CalculateTotalPrice()
        {
            TimeSpan duration = Schedule.DropOffDate.Date - Schedule.PickUpDate.Date;
            TotalPrice = (double)(duration.Days * Vehicle.dailyRentalPrice);

        }
    }
}
