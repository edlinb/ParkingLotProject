using ParkingLotProject.Models;
using System.Runtime.CompilerServices;

namespace ParkingLotProject.Repositories
{
    public class PricingPlansRepository
    {
        private readonly ParkingDbContext _context;
        public  PricingPlansRepository(ParkingDbContext context)
        {
            _context = context;
        }
        public void CreatePricingPlan(PricingPlan pricingPlan)
        {
            _context.PricingPlan.Add(pricingPlan);
            _context.SaveChanges();
        }

        public List<PricingPlan> GetPricingPlans()
        {
            return _context.PricingPlan.ToList();
        }
        //metoda per te ber patch hourlyPricing dhe DailyPricing

        public void UpdatePricingPlan(int id,decimal hourlyPricing,decimal dailyPricing)
        {
            var pricingPlan = _context.PricingPlan.FirstOrDefault(p => p.Id == id);
            if (pricingPlan != null)
            {
                pricingPlan.HourlyPricing = hourlyPricing;
                pricingPlan.DailyPricing = dailyPricing;
            }
        }
    }
}
