namespace VehicleSystem.Models
{
    public class Driver
    {
        public string FirstName {  get; set; }  
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string LicenseNumber { get; set; }

        public Driver(string firstName, string lastName, DateTime dateOfBirth, string licenseNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            LicenseNumber = licenseNumber;
        }
    }

    
}
