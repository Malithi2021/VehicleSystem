namespace VehicleSystem.Models
{
    public abstract class Vehicle : IComparable<Vehicle>
    {
        public string? RegistrationNumber { get; set; }
        public string? Make { get; set; }
        public string? Model { get; set; }
        public double? DailyRentalPrice { get; set; }
        public List<Booking> Reservations { get; set; } = new List<Booking>();

        public Vehicle(string registrationNumber, string make, string model, double dailyRentalPrice)
        {
            RegistrationNumber = registrationNumber;
            Make = make;
            Model = model;
            DailyRentalPrice = dailyRentalPrice;
        }

        public int CompareTo(Vehicle other)
        {
            if (other == null) return 1;
            return string.Compare(this.Make, other.Make, StringComparison.OrdinalIgnoreCase);
        }

        public abstract void DisplayInfo();

        public bool IsAvailable(Schedule wantedSchedule)
        {
            foreach (var reservation in Reservations)
            {
                if (wantedSchedule.Overlaps(reservation.Schedule))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
