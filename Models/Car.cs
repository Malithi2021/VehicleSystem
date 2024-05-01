using System.Reflection;

namespace VehicleSystem.Models
{
    public class Car(string registrationNumber, string make, string model, double dailyRentalPrice, int passengerCapacity) : Vehicle(registrationNumber, make, model, dailyRentalPrice)
    {
        public int PassengerCapacity { get; set; } = passengerCapacity;

        public override void DisplayInfo()
        {
            Console.WriteLine($"Vehicle Type: Car");
            Console.WriteLine($"Registration Number: {RegistrationNumber}");
            Console.WriteLine($"Passenger Capacity: {PassengerCapacity}");
            Console.WriteLine();
        }
    }
}
