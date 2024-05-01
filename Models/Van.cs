using System.Reflection;

namespace VehicleSystem.Models
{
    public class Van : Vehicle
    {
        public int CargoCapacity { get; set; }

        public Van(string registrationNumber,string make,string model,double dailyRentalPrice, int cargoCapacity)
            :base(registrationNumber,make,model,dailyRentalPrice)
        {
            CargoCapacity = cargoCapacity;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Vehicle Type: Van");
          
        }
    }
}
