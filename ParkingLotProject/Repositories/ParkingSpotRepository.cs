using ParkingLotProject.Models;

namespace ParkingLotProject.Repositories
{
    public class ParkingSpotsRepository
    {
        private readonly ParkingDbContext _context;
        public ParkingSpotsRepository(ParkingDbContext context)
        {
            _context = context;
        }

        // Method to add a new parking spot
        public void CreateParkingSpot(ParkingSpot parkingSpot)
        {
            _context.ParkingSpot.Add(parkingSpot);
            _context.SaveChanges();
        }

        // Method to retrieve all parking spots
        public List<ParkingSpot> GetParkingSpots()
        {
            return _context.ParkingSpot.ToList();
        }

        // Method to update the total and reserved spots for a parking spot
        public void UpdateParkingSpot(ParkingSpot updatedParkingSpot)
        {
            var existingParkingSpot = _context.ParkingSpot.FirstOrDefault(p => p.Id == updatedParkingSpot.Id);
            if (existingParkingSpot != null)
            {
                existingParkingSpot.TotalSpots = updatedParkingSpot.TotalSpots;
            }
        }
        public int GetReservedSpots()
        {
            var activeSubscriberCount = _context.Subscriber.Count(subscriber => !subscriber.isDeleted);
            return activeSubscriberCount;
        }
        public int GetTotalSpots()
        {
            var totalSpots = _context.ParkingSpot.FirstOrDefault().TotalSpots;
            return totalSpots;
        }
        public int GetFreeSpots()
        {
            int totalSpots = GetTotalSpots();
            int reservedSpots = GetReservedSpots();
            int freeSpots = GetFreeSpots();
            return freeSpots;
     
        }

        public int GetOccupiedRegularSpots()
        {
            var checkedInRegularSpots = _context.Logs.Count(log => log.CheckOut == null && log.SubscriptionId == null);
            return checkedInRegularSpots;
        }

        public int GetOccupiedReservedSpots()
        {
            var checkedInReservedSpots = _context.Logs.Count(log => log.CheckOut == null && log.SubscriptionId == null);
    
            return checkedInReservedSpots;
        }
    }
}
