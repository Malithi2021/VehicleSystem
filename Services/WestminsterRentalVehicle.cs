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


        public void ListVehicles()
        {
            foreach (Vehicle vehicle in vehicles)
            {
                Console.WriteLine($"Registration Number: {vehicle.RegistrationNumber}");
                Console.WriteLine($"Type: {vehicle.GetType().Name}");
                Console.WriteLine($"Reservations:");

                foreach (Booking booking in vehicle.Bookings)
                {
                    Console.WriteLine($"- Pick-up Date: {booking.Schedule.PickUpDate.ToShortDateString()}, Drop-off Date: {booking.Schedule.DropOffDate.ToShortDateString()}");
                }

                Console.WriteLine();
            }
        }

       /* public void ListOrderedVehicles()
        {
            vehicles.Sort((v1, v2) => String.Compare(v1.Make, v2.Make, StringComparison.Ordinal));
            ListVehicles();
        }


        public void GenerateReport(string fileName)
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                foreach (Vehicle vehicle in vehicles)
                {
                    writer.WriteLine($"Registration Number: {vehicle.RegistrationNumber}");
                    writer.WriteLine($"Type: {vehicle.GetType().Name}");
                    writer.WriteLine($"Reservations:");

                    foreach (Booking booking in vehicle.Bookings)
                    {
                        writer.WriteLine($"- Pick-up Date: {booking.Schedule.PickUpDate.ToShortDateString()}, Drop-off Date: {booking.Schedule.DropOffDate.ToShortDateString()}");
                    }

                    writer.WriteLine();
                }
            }
            Console.WriteLine($"Report generated successfully and saved to {fileName}");
        }

        public void ListAvailableVehicles(Schedule wantedSchedule, Type type)
        {
            foreach (Vehicle vehicle in vehicles)
            {
                if (vehicle.GetType() == type && IsVehicleAvailable(vehicle, wantedSchedule))
                {
                    Console.WriteLine($"Registration Number: {vehicle.RegistrationNumber}");
                    Console.WriteLine($"Type: {vehicle.GetType().Name}");
                    Console.WriteLine($"Daily Rental Price: {vehicle.DailyRentalPrice}");
                }
            }
        }


        private bool IsVehicleAvailable(Vehicle vehicle, Schedule wantedSchedule)
        {
            foreach (Booking booking in vehicle.Bookings)
            {
                if (booking.Schedule.Overlaps(wantedSchedule))
                    return false;
            }
            return true;
        }

        public bool AddReservation(string number, Schedule wantedSchedule)
        {
            Vehicle vehicle = vehicles.Find(v => v.RegistrationNumber == number);
            if (vehicle != null && IsVehicleAvailable(vehicle, wantedSchedule))
            {
                double totalPrice = vehicle.DailyRentalPrice * (wantedSchedule.DropOffDate - wantedSchedule.PickUpDate).TotalDays;
                Booking booking = new Booking(vehicle, new Driver(), wantedSchedule);
                booking.TotalPrice = totalPrice;
                vehicle.Bookings.Add(booking);
                return true;
            }
            return false;
        }

        public bool ChangeReservation(string number, Schedule oldSchedule, Schedule newSchedule)
        {
            Vehicle vehicle = vehicles.Find(v => v.RegistrationNumber == number);
            if (vehicle != null)
            {
                Booking bookingToChange = vehicle.Bookings.Find(booking => booking.Schedule == oldSchedule);
                if (bookingToChange != null && IsVehicleAvailable(vehicle, newSchedule))
                {
                    bookingToChange.Schedule = newSchedule;
                    return true;
                }
            }
            return false;
        }

        public bool DeleteReservation(string number, Schedule schedule)
        {
            Vehicle vehicle = vehicles.Find(v => v.RegistrationNumber == number);
            if (vehicle != null)
            {
                Booking bookingToRemove = vehicle.Bookings.Find(booking => booking.Schedule == schedule);
                if (bookingToRemove != null)
                {
                    vehicle.Bookings.Remove(bookingToRemove);
                    return true;
                }
            }
            return false;
        }


    }
*/
    }
}
