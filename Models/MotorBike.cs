using System.Reflection;

namespace VehicleSystem.Models
{
    public class MotorBike : Vehicle
    {
        public int EngineDisplacement { get; set; }  // Additional property for motorbikes

        public MotorBike(string registrationNumber, string make, string model, double dailyRentalPrice, int engineDisplacement)
            : base(registrationNumber, make, model, dailyRentalPrice)
        {
            EngineDisplacement = engineDisplacement;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Vehicle Type: Motorbike");
            Console.WriteLine($"Registration Number: {RegistrationNumber}");
            Console.WriteLine($"Engine Displacement: {EngineDisplacement} cc");
            Console.WriteLine();
        }
    }
}
