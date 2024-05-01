using System.Reflection;

namespace VehicleSystem.Models
{
    public class Van(string registrationNumber, string make, string model, double dailyRentalPrice, int cargoCapacity) : Vehicle(registrationNumber,make,model,dailyRentalPrice)
    {
        public int CargoCapacity { get; set; } = cargoCapacity;

        public override void DisplayInfo()
        {
            Console.WriteLine($"Vehicle Type: Van");
          
        }
    }
}
