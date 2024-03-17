using Microsoft.AspNetCore.Mvc;
using ParkingLotProject.Models;
using ParkingLotProject.Repositories;

namespace ParkingLotProject.Controllers
{
    public class ParkingSpotController
    {
        [ApiController]
        [Route("api/ParkingSpot")]

        public class ParkingSpotsController : ControllerBase
        {
            private readonly ParkingSpotsRepository _parkingSpotRepository;

            public ParkingSpotsController(ParkingSpotsRepository parkingSpotRepository)
            {
                _parkingSpotRepository = parkingSpotRepository;
            }

            [HttpPost]
            public IActionResult CreateParkingSpot([FromBody] ParkingSpot parkingSpot)
            {
                try
                {
                    _parkingSpotRepository.CreateParkingSpot(parkingSpot);
                    return Ok("ParkingSpot created succesfully.");
                }
                catch (Exception ex)
                {
                    return BadRequest($"Failed to create parkingSpot: {ex.Message}");
                }
            }
            [HttpGet("Total")]
            public IActionResult GetTotalSpots(ParkingSpot parkingSpot)
            {
                try
                {
                   int totalSpots =  _parkingSpotRepository.GetTotalSpots();
                    return Ok("ParkingSpot was listed succesfully.");
                }
                catch (Exception ex)
                {
                    return BadRequest($"Failed to get parkingSpot list: {ex.Message}");
                }
            }
            [HttpGet ("Reserved")]
            public IActionResult GetReservedSpots()
            {
                try
                {
                   int activeSubscriberCount =_parkingSpotRepository.GetReservedSpots();
                    return Ok("ReservedParkingSpots are shown successfully .");
                }
                catch (Exception ex)
                {
                    return BadRequest($"Failed to get ReservedParkingSpots : {ex.Message}");
                }
            }
            [HttpGet("Free")]
            public IActionResult GetFreeSpots()
            {
                try
                {
                    int freeSpots = _parkingSpotRepository.GetFreeSpots();
                    return Ok("FreeSpots are shown successfully .");
                }
                catch (Exception ex)
                {
                    return BadRequest($"Failed to get FreeSpots : {ex.Message}");
                }
            }

            [HttpPut("{Id}")]
            public IActionResult UpdateParkingSpots(int Id, ParkingSpot updatedparkingSpot)
            {
                try
                {
                    var parkingSpot = new ParkingSpot
                    {
                        Id = Id,
                        TotalSpots = updatedparkingSpot.TotalSpots
                    };
                    _parkingSpotRepository.UpdateParkingSpot(parkingSpot);
                    return Ok("ParkingSpot was updated successfully.");
                }
                catch (Exception ex)
                {
                    return BadRequest($"Failed to update parkingSpot : {ex.Message}");
                }
            }
            [HttpGet("Occupied/Reserved")]
            public IActionResult GetOccupiedReservedSpots ()
            {
                int occupiedReservedSpots = _parkingSpotRepository.GetOccupiedReservedSpots();
                return Ok(occupiedReservedSpots);
            }
            [HttpGet("Occupied/Regular")]
            public IActionResult GetOccupiedRegularSpots()
            {
                int occupiedRegularSpots = _parkingSpotRepository.GetOccupiedRegularSpots();
                return Ok(occupiedRegularSpots);
            }
        }
    }
}
