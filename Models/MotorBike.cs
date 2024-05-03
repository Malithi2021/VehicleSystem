namespace VehicleSystem.Models
{
    public class MotorBike(string registrationNumber, string make, string model, double dailyRentalPrice, int engineDisplacement) : Vehicle(registrationNumber, make, model, dailyRentalPrice)
    {
        public int EngineDisplacement { get; set; } = engineDisplacement;

        public override void DisplayInfo()
        {
            Console.WriteLine($"Vehicle Type: Motorbike");
            Console.WriteLine($"Registration Number: {RegistrationNumber}");
            Console.WriteLine($"Engine Displacement: {EngineDisplacement} cc");
            Console.WriteLine();
        }
    }
}
