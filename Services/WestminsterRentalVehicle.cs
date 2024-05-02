using System;
using System.Collections.Generic;
using System.IO;
using VehicleSystem.Data;
using VehicleSystem.Interfaces;
using VehicleSystem.Models;

namespace VehicleSystem.Services
{
    public class WestminsterRentalVehicle : IRentalManager, IRentalCustomer
    {
        private List<Vehicle> vehicles = new List<Vehicle>();
        private InMemoryData _inMemoryData;

        public WestminsterRentalVehicle(InMemoryData data)
        {
            _inMemoryData = data;
            vehicles = _inMemoryData.GetAllVehicles();
        }



        public bool AddVehicle(Vehicle v)
        {
            if (_inMemoryData.AddWestminsterRentalVehicle(v))
            {
                Console.WriteLine($"Vehicle with registration number {v.RegistrationNumber} added successfully.");
                Console.WriteLine($"Number of available parking lots: {50 - _inMemoryData.GetAllVehicles().Count}");
                return true;
            }
            else
            {
                Console.WriteLine($"Vehicle with registration number {v.RegistrationNumber} already exists.");
                return false;
            }
        }

        public bool DeleteVehicle(string number)
        {
            if (_inMemoryData.GetVehicleByNumber(number) != null)
            {
                _inMemoryData.RemoveVehicle(_inMemoryData.GetVehicleByNumber(number));
                Console.WriteLine($"Vehicle with registration number {number} deleted successfully.");
                Console.WriteLine($"Number of available parking lots: {50 - _inMemoryData.GetAllVehicles().Count}");
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
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                foreach (var vehicle in vehicles)
                {
                    writer.WriteLine($"Registration Number: {vehicle.RegistrationNumber}, Make: {vehicle.Make}, Model: {vehicle.Model}");
                }
            }
            Console.WriteLine($"Report generated successfully and saved as {fileName}");
        }

        public void ListOrderedVehicles()
        {
            var orderedVehicles = _inMemoryData.GetOrderedVehicles();
            Console.WriteLine("Ordered Vehicles:");
            foreach (var vehicle in orderedVehicles)
            {
                Console.WriteLine($"Make: {vehicle.Make}, Model: {vehicle.Model}, Registration Number: {vehicle.RegistrationNumber}, Daily Rental Price: {vehicle.DailyRentalPrice}");
            }
        }
        public void ListVehicles()
        {
            foreach (var vehicle in _inMemoryData.GetAllVehicles())
            {
                Console.WriteLine($"Registration Number: {vehicle.RegistrationNumber}, Make: {vehicle.Make}, Model: {vehicle.Model}");
            }
        }

        public void ListAvailableVehicles(Schedule wantedSchedule, Type type)
        {
            var availableVehicles = _inMemoryData.GetAvailableVehicles(wantedSchedule, type);
            if (availableVehicles.Count == 0)
            {
                Console.WriteLine("No vehicles available for the specified schedule and type.");
            }
            else
            {
                foreach (var vehicle in availableVehicles)
                {
                    vehicle.DisplayInfo();
                }
            }
        }



        public decimal CalculateTotalPrice(Vehicle vehicle, Schedule schedule)
        {
            TimeSpan duration = schedule.DropOffDate.Date - schedule.PickUpDate.Date;
            decimal totalPrice = (decimal)(duration.Days * (double)vehicle.DailyRentalPrice);
            return totalPrice;
        }

        public bool AddReservation(string number, Schedule wantedSchedule, Type type, Driver driver)
        {
            var vehicle = vehicles.Find(v => v.RegistrationNumber == number && v.GetType() == type);
            if (vehicle != null)
            {
                if (vehicle.IsAvailable(wantedSchedule))
                {
                    var totalPrice = CalculateTotalPrice(vehicle, wantedSchedule);
                    var reservation = new Booking(vehicle, driver, wantedSchedule);
                    reservation.TotalPrice = (double)totalPrice; 
                    vehicle.Reservations.Add(reservation);
                    Console.WriteLine("Reservation added successfully.");
                    return true;
                }
                else
                {
                    Console.WriteLine("The vehicle is not available for the specified schedule.");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Invalid vehicle number or type.");
                return false;
            }
        }

        public bool ChangeReservation(string number, Schedule oldSchedule, Schedule newSchedule)
        {
            var vehicle = vehicles.Find(v => v.RegistrationNumber == number);
            if (vehicle != null)
            {
                if (vehicle.IsAvailable(newSchedule))
                {
                    var reservation = vehicle.Reservations.Find(r => r.Schedule == oldSchedule);
                    if (reservation != null)
                    {
                        reservation.Schedule = newSchedule;


                        reservation.TotalPrice = (double)CalculateTotalPrice(vehicle, newSchedule);
                        Console.WriteLine("Reservation updated successfully.");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("No reservation found for the specified schedule.");
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine("The new schedule overlaps with existing bookings.");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Invalid vehicle number.");
                return false;
            }
        }

        public bool DeleteReservation(string number, Schedule schedule)
        {
            var vehicle = vehicles.Find(v => v.RegistrationNumber == number);
            if (vehicle != null)
            {
                var reservationToRemove = vehicle.Reservations.Find(r => r.Schedule == schedule);
                if (reservationToRemove != null)
                {
                    vehicle.Reservations.Remove(reservationToRemove);

                    Console.WriteLine("Reservation deleted successfully.");
                    return true;
                }
                else
                {
                    Console.WriteLine("No reservation found for the specified schedule.");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Invalid vehicle number.");
                return false;
            }
        }
    }
}
