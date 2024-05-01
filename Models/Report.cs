namespace VehicleSystem.Models
{
    public class Report
    {
        public Vehicle Vehicle { get; set; }
        public List<Booking> Bookings { get; set; }

       
        public Report()
        {
            Bookings = new List<Booking>();
        }
    }
}
