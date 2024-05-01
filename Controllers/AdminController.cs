using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VehicleSystem.Models;
using VehicleSystem.Services;

namespace VehicleSystem.Controllers
{
    public class AdminController : ControllerBase
    {
        private readonly RentalManager _rentalManager;

        public AdminController(RentalManager rentalManager)
        {
            _rentalManager = rentalManager;
        }

        [HttpPost]
        public IActionResult AddVehicle(Vehicle vehicle)
        {
            if (_rentalManager.AddVehicle(vehicle))
            {
                return Ok("Vehicle added successfully.");
            }
            else
            {
                return BadRequest("Failed to add vehicle. Duplicate registration number.");
            }
        }

        // Implement other actions for admin functionalities
    }


}

