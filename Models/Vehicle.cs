namespace VehicleSystem.Models
{
    public abstract class Vehicle
    {
        // Properties common to all vehicles
        public string ? RegistrationNumber { get; set; }
        public string ? make { get; set; }
        public string ? model { get; set; }
        public double ? dailyRentalPrice { get; set; }

       public Vehicle(string? registrationNumber, string? make, string? model, double? dailyRentalPrice)
        {
            RegistrationNumber = registrationNumber;
            this.make = make;
            this.model = model;
            this.dailyRentalPrice = dailyRentalPrice;
        }

        public abstract void DisplayInfo();
    }
}
