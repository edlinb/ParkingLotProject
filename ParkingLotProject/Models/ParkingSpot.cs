using System.ComponentModel.DataAnnotations;

namespace ParkingLotProject.Models
{
    public class ParkingSpot
    {
        [Key]
        public int Id { get; set; }
        public int TotalSpots {  get; set; }
        public int ReservedSpots {  get; set; }
    }
}
