using VehicleSystem.Models;

namespace VehicleSystem.Interfaces
{
    public interface IRentalManager
    {
        public bool AddVehicle(Vehicle v);
        public bool DeleteVehicle(string number);
        public void ListVehicles();
        public void ListOrderedVehicles();
       // public  void GenerateReport(string fileName);
    }
}
