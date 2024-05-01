using System.Reflection;

namespace VehicleSystem.Models
{
    public class Car : Vehicle
    {
        public int PassengerCapacity { get; set; }  // Additional property for cars

        public Car(string registrationNumber, string make, string model, double dailyRentalPrice, int passengerCapacity)
        : base(registrationNumber, make, model, dailyRentalPrice)
        {
            PassengerCapacity = passengerCapacity;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Vehicle Type: Car");
            Console.WriteLine($"Registration Number: {RegistrationNumber}");
            Console.WriteLine($"Passenger Capacity: {PassengerCapacity}");
            Console.WriteLine();
        }
    }
}
