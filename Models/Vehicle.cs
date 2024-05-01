namespace VehicleSystem.Models
{
    public abstract class Vehicle(string? registrationNumber, string? make, string? model, double? dailyRentalPrice)
    {
        // Properties common to all vehicles
        public string? RegistrationNumber { get; set; } = registrationNumber;
        public string? Make { get; set; } = make;
        public string? Model { get; set; } = model;
        public double? DailyRentalPrice { get; set; } = dailyRentalPrice;

        public abstract void DisplayInfo();
    }
}
