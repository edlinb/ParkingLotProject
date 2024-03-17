using Microsoft.AspNetCore.Mvc;
using ParkingLotProject.Models;
using ParkingLotProject.Repositories;

namespace ParkingLotProject.Controllers

{
    [ApiController]
    [Route("api/Logs")]

    public class LogsController : ControllerBase
    {
        private readonly LogRepository _logsRepository;

        public LogsController(LogRepository logsReporsitory)
        {
            _logsRepository = logsReporsitory;
        }

        [HttpPost]

        public IActionResult CreateLog([FromBody] Log logs)
        {
            try
            {
                _logsRepository.CreateLog(logs);
                return Ok("Log created succesfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to create log: {ex.Message}");
            }
        }

        [HttpGet("Date/{date}")]
        public IActionResult GetLogsByDate(DateTime date)
        {
            try
            {
                IEnumerable<Log> logs = _logsRepository.GetLogsByDate(date);
                return Ok(logs);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to retrieve logs:{ex.Message}");
            }
        }
        [HttpDelete("{id}")]

        public IActionResult Deletelogs(int id)
        {
            try
            {
                _logsRepository.Deletelogs(id);
                return Ok("Log deleted succesfully.");

            }catch (Exception ex)
            {
                return BadRequest($"Failed to delete log: {ex.Message}");
            }
        }
        [HttpPatch("{id}/checkout time")]
        public IActionResult UpdateCheckoutTime(int id, [FromBody] DateTime newCheckOutTime)
        {
            _logsRepository.UpdateCheckOutTime(id, newCheckOutTime);
            return Ok();
        }
    }
}
