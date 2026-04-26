using GarageAPI.Data;
using GarageAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GarageAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehiclesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VehiclesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/vehicles
        [HttpGet]
        public async Task<ActionResult<List<Vehicle>>> GetAllVehicles()
        {
            return await _context.Vehicles
                .OrderBy(v => v.ExpectedReturnDate)
                .ToListAsync();
        }

        // GET: api/vehicles/upcoming
        [HttpGet("upcoming")]
        public async Task<ActionResult<List<Vehicle>>> GetUpcomingVehicles()
        {
            var today = DateTime.Today;
            var cutoffDate = today.AddDays(3);

            var upcomingVehicles = await _context.Vehicles
                .Where(v => v.ExpectedReturnDate >= today && v.ExpectedReturnDate <= cutoffDate)
                .OrderBy(v => v.ExpectedReturnDate)
                .ToListAsync();

            return upcomingVehicles;
        }

        // POST: api/vehicles
        [HttpPost]
        public async Task<ActionResult<Vehicle>> AddVehicle(Vehicle vehicle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAllVehicles), new { id = vehicle.Id }, vehicle);
        }

        // PUT: api/vehicles/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, Vehicle vehicle)
        {
            if (id != vehicle.Id)
            {
                return BadRequest("Vehicle ID does not match request URL.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingVehicle = await _context.Vehicles.FindAsync(id);
            if (existingVehicle == null)
            {
                return NotFound();
            }

            existingVehicle.CustomerName = vehicle.CustomerName;
            existingVehicle.MobileNumber = vehicle.MobileNumber;
            existingVehicle.VehicleModel = vehicle.VehicleModel;
            existingVehicle.VehicleNumber = vehicle.VehicleNumber;
            existingVehicle.ProblemDescription = vehicle.ProblemDescription;
            existingVehicle.DateReceived = vehicle.DateReceived;
            existingVehicle.ExpectedReturnDate = vehicle.ExpectedReturnDate;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/vehicles/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
