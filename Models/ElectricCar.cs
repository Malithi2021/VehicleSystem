namespace VehicleSystem.Models
{
    public class ElectricCar(string registrationNumber, string make, string model, double dailyRentalPrice, int batteryCapacity) : Vehicle(registrationNumber, make, model, dailyRentalPrice)
    {
        public int BatteryCapacity { get; set; } = batteryCapacity;

        public override void DisplayInfo()
        {
            Console.WriteLine($"Vehicle Type: Electric Car");
            Console.WriteLine($"Registration Number: {RegistrationNumber}");
            Console.WriteLine($"Battery capacity:{BatteryCapacity}");
            Console.WriteLine();
        }
    }
}
