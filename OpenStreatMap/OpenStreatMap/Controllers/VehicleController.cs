// In Controllers namespace
using Microsoft.AspNetCore.Mvc;
using OpenStreatMap.Dal;
using OpenStreatMap.Manager;
using OpenStreatMap.DTO;
using OpenStreatMap.Manager;

[ApiController]
[Route("[controller]")]
public class VehicleController : ControllerBase
{
    private readonly VehicleRepository _vehicleRepository;
    //private readonly VehicleManager _vehicleManager;

    public VehicleController(MongoDBContext mongoDbContext)
    {
        _vehicleRepository = new VehicleRepository(mongoDbContext.VehicleParking);
        //_vehicleManager = new VehicleManager();
    }

    [HttpPost("addVehicle")]
    public IActionResult AddVehicle()
    {
        // Validate inputs as needed
        var VehicleParking = VehicleManager.CreateVehicleFromUserInput();

        _vehicleRepository.InsertVehicle(VehicleParking);

        return Ok("Vehicle data inserted into MongoDB successfully.");

    }
    [HttpGet("deleteAllVehicleParking")]
    public IActionResult DeleteAllVehicleParking()
    {
        try
        {
            // Delete all nodes from MongoDB
            _vehicleRepository.DeleteAllVehicleParking();

            return Ok("All VehicleParking deleted from MongoDB successfully.");
        }
        catch (Exception ex)
        {
            // Handle the exception according to your application's logging or error handling mechanism
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

}
