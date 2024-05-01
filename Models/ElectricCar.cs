using System.Reflection;

namespace VehicleSystem.Models
{
    public class ElectricCar : Vehicle
    {
        public int BatteryCapacity { get; set; }  // Additional property for electric cars

        public ElectricCar(string registrationNumber, string make, string model, double dailyRentalPrice, int batteryCapacity)
            : base(registrationNumber, make, model, dailyRentalPrice)
        {
            BatteryCapacity = batteryCapacity;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Vehicle Type: Electric Car");
            Console.WriteLine($"Registration Number: {RegistrationNumber}");
            Console.WriteLine($"Battery capacity:{BatteryCapacity}");
            Console.WriteLine();
        }
    }
}
