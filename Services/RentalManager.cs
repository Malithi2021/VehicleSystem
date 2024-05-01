using VehicleSystem.Data;
using VehicleSystem.Interfaces;
using VehicleSystem.Models;

namespace VehicleSystem.Services
{
    public class RentalManager : IRentalManager
    {
        private readonly InMemoryData _data;

        public RentalManager(InMemoryData data)
        {
            _data = data;
        }

        public bool AddVehicle(Vehicle v)
        {
            if (_data.AddVehicle(v))
            {
                return true;
            }
            else
            {
                // Handle duplicate vehicle registration number
                return false;
            }
        }

        public bool DeleteVehicle(string number)
        {
            var vehicle = _data.GetVehicleByNumber(number);
            if (vehicle != null)
            {
                _data.RemoveVehicle(vehicle);
                return true;
            }
            else
            {
                // Handle invalid vehicle number
                return false;
            }
        }

        public void ListVehicles()
        {
            var vehicles = _data.GetAllVehicles();
        }

        public void ListOrderedVehicles()
        {
            var orderedVehicles = _data.GetOrderedVehicles();
        }

       /* public void GenerateReport(string fileName)
        {
            var reportData = _data.GenerateReportData();
        }*/

    }
}
