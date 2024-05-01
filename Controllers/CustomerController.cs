using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VehicleSystem.Models;
using VehicleSystem.Services;

namespace VehicleSystem.Controllers
{
    public class CustomerController : ControllerBase
    {
        private readonly RentalCustomer _rentalCustomer;

        public CustomerController(RentalCustomer rentalCustomer)
        {
            _rentalCustomer = rentalCustomer;
        }
       
        [HttpGet]
        public IActionResult ListAvailableVehicles(Schedule wantedSchedule, Type type)
        {
            _rentalCustomer.ListAvailableVehicles(wantedSchedule, type);
            return Ok();
        }
    }
}
