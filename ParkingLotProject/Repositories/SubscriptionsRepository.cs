using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using ParkingLotProject.Models;

namespace ParkingLotProject.Repositories
{
    public class SubscriptionsRepository
    {
        private readonly ParkingDbContext _context;
        public SubscriptionsRepository(ParkingDbContext context)
        {
            _context = context;
        }
        private PricingPlan GetPricingPlan(DayOfWeek dayOfWeek) 
        {
            return _context.PricingPlan.FirstOrDefault(plan => 
                 plan.Type == PricingPlanType.Weekday && dayOfWeek >= DayOfWeek.Monday && dayOfWeek <= DayOfWeek.Friday)
                ?? _context.PricingPlan.FirstOrDefault(plan =>
                plan.Type == PricingPlanType.Weekend && (dayOfWeek == DayOfWeek.Saturday || dayOfWeek == DayOfWeek.Sunday));
        }
        public IEnumerable<Subscription> GetAll()
        {
            return _context.Subscription.ToList();
        }

        public Subscription GetById(int id)
        {
            return _context.Subscription.Find(id);
        }

        public void AddSubscription(Subscription newSubscription)
        {
            if (newSubscription == null || 
                newSubscription.StartTime == DateTime.MinValue ||
                newSubscription.EndTime == DateTime.MinValue)
            {
                throw new ArgumentException("All fields required.");
            }

            if (newSubscription.Code != 0 && _context.Subscription.Any(sub => sub.Code == newSubscription.Code)) 
            {
                throw new ArgumentException("A subscription with the same code already exists.");
            }


            if (newSubscription.StartTime >= newSubscription.EndTime) 
            {
                throw new ArgumentException("End time must be after start time.");
            }

            decimal price = 0;
            TimeSpan subscriptionDuration = newSubscription.EndTime - newSubscription.StartTime;
            int TotalDays = (int)Math.Ceiling(subscriptionDuration.TotalDays);
            PricingPlan pricingPlan = GetPricingPlan(newSubscription.StartTime.DayOfWeek);

            if (pricingPlan != null) 
            {
                price = TotalDays * pricingPlan.DailyPricing;

                if (newSubscription.Discountvalue > 0) 
                {
                    price -= newSubscription.Discountvalue;
                }
            }
            newSubscription.Price = price;
            _context.Subscription.Add(newSubscription);
            _context.SaveChanges();
        }

        public Subscription GetSubscriptionByCode(int code) 
        {
            return _context.Subscription.FirstOrDefault(sub => sub.Code == code);
        }

        public void UpdateSubscription(Subscription subscription)
        {
            if (subscription == null) 
            {
                throw new ArgumentNullException("Subscription object cannot be null.");
            }

            var existingSubscription =
                _context.Subscription.FirstOrDefault(sub => sub.Code == subscription.Code);
            if (existingSubscription == null )
            {
                throw new ArgumentException("Subscription not found.");
            }

            existingSubscription.StartTime = subscription.StartTime;
            existingSubscription.EndTime = subscription.EndTime;
            existingSubscription.Price = subscription.Price;
            existingSubscription.Discountvalue = subscription.Discountvalue;


            _context.SaveChanges();
        }

        public void SoftDelete(int code)
        {
            var subscription = _context.Subscription.FirstOrDefault(sub => sub.Code == code);
            if (subscription != null)
            {
                throw new ArgumentException("Subscription not found.");
            }

            subscription.isDeleted = true;
            _context.SaveChanges();
        }
    }

}

