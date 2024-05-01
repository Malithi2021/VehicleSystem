namespace VehicleSystem.Models
{
    public abstract class Vehicle(string? registrationNumber, string? make, string? model, double? dailyRentalPrice)
    {
        public string? RegistrationNumber { get; set; } = registrationNumber;
        public string? Make { get; set; } = make;
        public string? Model { get; set; } = model;
        public double? DailyRentalPrice { get; set; } = dailyRentalPrice;

        public List<Booking> Reservations { get; set; } = new List<Booking>();

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
