using Microsoft.AspNetCore.Mvc;
using OpenStreatMap.Dal;
using OpenStreatMap.DTO;
using OpenStreatMap.Manager; 

namespace OpenStreatMap.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class DatabaseConnection : ControllerBase
    {
        private readonly NodeRepository _nodeRepository;
        private readonly OpenStreetMapService _OpenStreetMapService;
        private readonly ParkingRepository _parkingRepository;

        public DatabaseConnection(OpenStreetMapService OpenStreetMapService, MongoDBContext mongoDbContext)
        {
            _OpenStreetMapService = OpenStreetMapService;
            _nodeRepository = new NodeRepository(mongoDbContext.NodesGraph);
            _parkingRepository = new ParkingRepository(mongoDbContext.Parkings);
        }

        [HttpGet("deleteAllNodes")]
        public IActionResult DeleteAllNodes()
        {
            try
            {
                // Delete all nodes from MongoDB
                _nodeRepository.DeleteAllNodes();

                return Ok("All nodes deleted from MongoDB successfully.");
            }
            catch (Exception ex)
            {
                // Handle the exception according to your application's logging or error handling mechanism
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpGet("processMapFile")]
        public IActionResult ProcessMapFile()
        {
            string filePath = "./XML.xml";
            var (nodes, parkingLots) = _OpenStreetMapService.ProcessMapFile(filePath);

            //Insert nodes into MongoDB
            _nodeRepository.InsertNodes(nodes);
            _parkingRepository.InsertParkings(parkingLots);


            return Ok("Nodes inserted into MongoDB successfully.");


        }
        [HttpPost("addParking")]
        public IActionResult AddParking([FromBody] Parking parking)
        {
            // You can call this API with JSON payload containing CodeParking, MaxPlace, SeatTaken, TypePrking, and GateParking details
            //_parkingManager.AddParking(parking.CodeParking, parking.MaxPlace, parking.SeatTaken, parking.GateParking, parking.TypePrking);

            return Ok("Parking added successfully.");
        }
        [HttpGet("getGraph")]
        public ActionResult<List<Node>> getGraph()
        {


            // Insert nodes into MongoDB


            return _nodeRepository.getGraph();
        }


    }
}









