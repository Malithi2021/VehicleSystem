namespace VehicleSystem.Models
{
    public class Driver(string firstName, string lastName, DateTime dateOfBirth, string licenseNumber)
    {
        public string FirstName { get; set; } = firstName;
        public string LastName { get; set; } = lastName;
        public DateTime DateOfBirth { get; set; } = dateOfBirth;
        public string LicenseNumber { get; set; } = licenseNumber;
    }


}
