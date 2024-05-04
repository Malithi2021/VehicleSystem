using System.Globalization;
using VehicleSystem.Data;
using VehicleSystem.Models;
using VehicleSystem.Services;

namespace VehicleSystem
{
    class Program
    {
        static void ShowCustomerMenu(WestminsterRentalVehicle rental)
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Customer Menu:");
                Console.WriteLine("1. List Available Vehicles");
                Console.WriteLine("2. Add Reservation");
                Console.WriteLine("3. Change Reservation");
                Console.WriteLine("4. Delete Reservation");
                Console.WriteLine("5. Access Admin Menu");
                Console.WriteLine("6. Exit");

                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine() ?? "";
                switch (choice)
                {
                    case "1":
                        DateTime pickUpDate = DateTime.MinValue;
                        DateTime dropOffDate = DateTime.MinValue;
                        bool validDates = false;
                        while (!validDates)
                        {
                            Console.WriteLine("Enter the pick-up date (DD/MM/YYYY):");
                            string pickUpDateString = Console.ReadLine();
                            Console.WriteLine("Enter the drop-off date (DD/MM/YYYY):");
                            string dropOffDateString = Console.ReadLine();
                            try
                            {
                                pickUpDate = DateTime.ParseExact(pickUpDateString, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                dropOffDate = DateTime.ParseExact(dropOffDateString, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                if (dropOffDate < pickUpDate)
                                {
                                    Console.WriteLine("Invalid dates. Drop-off date cannot be earlier than the pick-up date.");
                                    continue;
                                }
                                validDates = true;
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Invalid date format. Please enter the date in the format DD/MM/YYYY.");
                            }
                        }
                        Schedule wantedSchedule = new Schedule { PickUpDate = pickUpDate, DropOffDate = dropOffDate };
                        rental.ListAvailableVehicles(wantedSchedule, typeof(Vehicle));
                        break;
                    case "2":
                        while (true)
                        {
                            try
                            {
                                Console.WriteLine("Enter the registration number of the vehicle:");
                                string regNumber = Console.ReadLine() ?? "";
                                Console.WriteLine("Enter the pick-up date (DD/MM/YYYY):");
                                DateTime newPickUpDate = DateTime.ParseExact(Console.ReadLine() ?? "", "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                Console.WriteLine("Enter the drop-off date (DD/MM/YYYY):");
                                DateTime newDropOffDate = DateTime.ParseExact(Console.ReadLine() ?? "", "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                
                                if (newDropOffDate < newPickUpDate)
                                {
                                    Console.WriteLine("Invalid dates. Drop-off date cannot be earlier than the pick-up date.");
                                    continue;
                                }
                                Schedule newSchedule = new Schedule { PickUpDate = newPickUpDate, DropOffDate = newDropOffDate };
                                Console.WriteLine("Enter driver's first name:");
                                string firstName = Console.ReadLine() ?? "";
                                Console.WriteLine("Enter driver's surname:");
                                string surName = Console.ReadLine() ?? "";
                                Console.WriteLine("Enter driver's date of birth (DD/MM/YYYY):");
                                DateTime dob = DateTime.ParseExact(Console.ReadLine() ?? "", "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                Console.WriteLine("Enter driver's license number:");
                                string licenseNumber = Console.ReadLine() ?? "";
                                Driver driver = new Driver { FirstName = firstName, SurName = surName, DateOfBirth = dob, LicenseNumber = licenseNumber };

                                Console.WriteLine("Enter the type of vehicle (Car, ElectricCar, MotorBike, or Van): ");
                                string vehicleTypeInput = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Console.ReadLine()?.ToLowerInvariant() ?? "");

                                Dictionary<string, string> typeMappings = new Dictionary<string, string>
                                {
                                        { "Car", "Car" },
                                        { "Van", "Van" },
                                        { "Motorbike", "MotorBike" },
                                        { "Electriccar", "ElectricCar" }
                                };

                                if (typeMappings.TryGetValue(vehicleTypeInput, out string vehicleTypeName))
                                {
                                    Type type = Type.GetType("VehicleSystem.Models." + vehicleTypeName, throwOnError: true);
                                    rental.AddReservation(regNumber, newSchedule, type, driver);
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid vehicle type. Please try again.");
                                }
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Invalid input format. Please enter valid data.");
                            }
                        }
                        break;

                    case "3":
                        Console.WriteLine("Enter the registration number of the vehicle:");
                        string vehicleNumber = Console.ReadLine() ?? "";
                        Console.WriteLine("Enter the existing pick-up date (DD/MM/YYYY):");
                        DateTime oldPickUpDate = DateTime.ParseExact(Console.ReadLine() ?? "", "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        Console.WriteLine("Enter the existing drop-off date (DD/MM/YYYY):");
                        DateTime oldDropOffDate = DateTime.ParseExact(Console.ReadLine() ?? "", "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        if (oldDropOffDate < oldPickUpDate)
                        {
                            Console.WriteLine("Invalid dates. Drop-off date cannot be earlier than the pick-up date.");
                            break;
                        }
                        Schedule existingSchedule = new Schedule { PickUpDate = oldPickUpDate, DropOffDate = oldDropOffDate };
                        Console.WriteLine("Enter the new pick-up date (DD/MM/YYYY):");
                        DateTime updatedPickUpDate = DateTime.ParseExact(Console.ReadLine() ?? "", "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        Console.WriteLine("Enter the new drop-off date (DD/MM/YYYY):");
                        DateTime updatedDropOffDate = DateTime.ParseExact(Console.ReadLine() ?? "", "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        if (updatedDropOffDate < updatedPickUpDate)
                        {
                            Console.WriteLine("Invalid dates. Drop-off date cannot be earlier than the pick-up date.");
                            break;
                        }
                        Schedule updatedSchedule = new Schedule { PickUpDate = updatedPickUpDate, DropOffDate = updatedDropOffDate };
                        rental.ChangeReservation(vehicleNumber, existingSchedule, updatedSchedule);
                        break;
                    case "4":
                        Console.WriteLine("Enter the registration number of the vehicle:");
                        string deleteVehicleNumber = Console.ReadLine() ?? "";
                        Console.WriteLine("Enter the pick-up date (DD/MM/YYYY):");
                        DateTime deletePickUpDate = DateTime.ParseExact(Console.ReadLine() ?? "", "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        Console.WriteLine("Enter the drop-off date (DD/MM/YYYY):");
                        DateTime deleteDropOffDate = DateTime.ParseExact(Console.ReadLine() ?? "", "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        if (deleteDropOffDate < deletePickUpDate)
                        {
                            Console.WriteLine("Invalid dates. Drop-off date cannot be earlier than the pick-up date.");
                            break;
                        }
                        Schedule deleteSchedule = new Schedule { PickUpDate = deletePickUpDate, DropOffDate = deleteDropOffDate };
                        rental.DeleteReservation(deleteVehicleNumber, deleteSchedule);
                        break;
                    case "5":
                        ShowAdminMenu(rental);
                        break;
                    case "6":
                        exit = true;
                        break;
                    case "7":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void ShowAdminMenu(WestminsterRentalVehicle rental)
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Admin Menu:");
                Console.WriteLine("1. Add Vehicle");
                Console.WriteLine("2. Delete Vehicle");
                Console.WriteLine("3. List Vehicles");
                Console.WriteLine("4. List Ordered Vehicles");
                Console.WriteLine("5. Generate Report");
                Console.WriteLine("6. Back to Customer Menu");
                Console.WriteLine("7. Exit");

                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Enter the vehicle details:");
                        Console.Write("Registration Number: ");
                        string regNumber = Console.ReadLine() ?? "";
                        Console.Write("Make: ");
                        string make = Console.ReadLine() ?? "";
                        Console.Write("Model: ");
                        string model = Console.ReadLine() ?? "";
                        Console.Write("Year: ");
                        int year = int.Parse(Console.ReadLine() ?? "") ;
                        Console.Write("Color: ");
                        string color = Console.ReadLine() ?? "";
                        Console.Write("Price per Day: ");
                        double pricePerDay = double.Parse(Console.ReadLine() ?? "");

                        // Ask for the type of vehicle
                        Console.WriteLine("Enter the type of vehicle (Car, ElectricCar, MotorBike, or Van): ");
                        string vehicleType = Console.ReadLine() ?? "";

                        Vehicle newVehicle;
                        switch (vehicleType.ToLower())
                        {
                            case "car":
                                Console.Write("Passenger Capacity: ");
                                int passengerCapacity = int.Parse(Console.ReadLine() ?? "");
                                newVehicle = new Car(regNumber, make, model, pricePerDay, passengerCapacity);
                                break;
                            case "electriccar":
                                Console.Write("Battery Capacity: ");
                                int batteryCapacity = int.Parse(Console.ReadLine() ?? "");
                                newVehicle = new ElectricCar(regNumber, make, model, pricePerDay, batteryCapacity);
                                break;
                            case "motorbike":
                                Console.Write("Engine Displacement (cc): ");
                                int engineDisplacement = int.Parse(Console.ReadLine() ?? "");
                                newVehicle = new MotorBike(regNumber, make, model, pricePerDay, engineDisplacement);
                                break;
                            case "van":
                                Console.Write("Cargo Capacity: ");
                                int cargoCapacity = int.Parse(Console.ReadLine() ?? "");
                                newVehicle = new Van(regNumber, make, model, pricePerDay, cargoCapacity);
                                break;
                            default:
                                Console.WriteLine("Invalid vehicle type.");
                                return;
                        }

                        if (rental.AddVehicle(newVehicle))
                        {
                            Console.WriteLine("Vehicle added successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Failed to add vehicle. Duplicate registration number or maximum capacity reached.");
                        }
                        break;

                    case "2":
                        Console.WriteLine("Enter the registration number of the vehicle to delete:");
                        string deleteRegNumber = Console.ReadLine() ?? "";
                        rental.DeleteVehicle(deleteRegNumber);
                        break;
                    case "3":
                        rental.ListVehicles();
                        break;
                    case "4":
                        rental.ListOrderedVehicles();
                        break;
                    case "5":
                        Console.WriteLine("Enter the file name to save the report:");
                        string fileName = Console.ReadLine() ?? "";
                        rental.GenerateReport(fileName);
                        break;
                    case "6":
                        exit = true;
                        break;
                    case "7":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void Main(string[] args)
        {
            var rental = new WestminsterRentalVehicle(new InMemoryData());

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Welcome to Vehicle Rental System");
                Console.WriteLine("1. Customer Menu");
                Console.WriteLine("2. Admin Menu");
                Console.WriteLine("3. Exit");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowCustomerMenu(rental);
                        break;
                    case "2":
                        ShowAdminMenu(rental);
                        break;
                    case "3":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

    }
}
