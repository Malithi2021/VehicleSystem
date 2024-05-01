namespace VehicleSystem.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using VehicleSystem.Interfaces;
    using VehicleSystem.Models;


    public class WestminsterRentalVehicle : IRentalManager
    {
        private List<Vehicle> vehicles = new List<Vehicle>();


        public bool AddVehicle(Vehicle v)
        {
            if (vehicles.Exists(vehicle => vehicle.RegistrationNumber == v.RegistrationNumber))
            {
                Console.WriteLine($"Vehicle with registration number {v.RegistrationNumber} already exists.");
                return false;
            }

            vehicles.Add(v);
            Console.WriteLine($"Vehicle with registration number {v.RegistrationNumber} added successfully.");
            Console.WriteLine($"Number of available parking lots: {50 - vehicles.Count}");
            return true;
        }

        public bool DeleteVehicle(string number)
        {
            Vehicle vehicleToRemove = vehicles.Find(v => v.RegistrationNumber == number);
            if (vehicleToRemove != null)
            {
                vehicles.Remove(vehicleToRemove);
                Console.WriteLine($"Vehicle with registration number {number} deleted successfully.");
                Console.WriteLine($"Number of available parking lots: {50 - vehicles.Count}");
                return true;
            }
            else
            {
                Console.WriteLine($"Vehicle with registration number {number} not found.");
                return false;
            }
        }

        public void GenerateReport(string fileName)
        {
            throw new NotImplementedException();
        }

        public void ListOrderedVehicles()
        {
            throw new NotImplementedException();
        }

        public void ListVehicles()
        {
            throw new NotImplementedException();
        }


      

     
    }
}
