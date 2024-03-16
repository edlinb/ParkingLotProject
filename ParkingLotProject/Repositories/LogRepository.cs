using ParkingLotProject.Models;

namespace ParkingLotProject.Repositories
{
    public class LogRepository
    {
        private readonly ParkingDbContext _context;

        public LogRepository(ParkingDbContext context)
        {
            _context = context;
        }

        public void CreateLog(Log log)
        {
            TimeSpan duration = log.CheckOut.Value - log.CheckIn;

            if (duration.TotalMinutes < 15)
            {
                log.Price = 0;
            }
            else
            {
                PricingPlan pricingPlan = _context.PricingPlan.FirstOrDefault(plan => plan.Type == GetPricingPlanType(log.CheckIn));

                if (log.SubscriptionId > 0)
                {
                    log.Price = 0;
                }
                else
                {
                    decimal totalhours = (decimal)duration.TotalHours;

                    if (totalhours <= pricingPlan?.MinimumHours)
                    {
                        log.Price = totalhours * pricingPlan.HourlyPricing;
                    }
                    else
                    {
                        int totalDays = (int)Math.Floor(totalhours / 24);
                        decimal remaininghours = totalhours % 24;

                        if (remaininghours <= pricingPlan.MinimumHours)
                        {
                            log.Price = (totalDays * pricingPlan.DailyPricing) + (remaininghours * pricingPlan.HourlyPricing);
                        }
                        else
                        {
                            log.Price = (totalDays + 1) * pricingPlan.DailyPricing;
                        }
                    }
                }
            }
            _context.Logs.Add(log);
            _context.SaveChanges();
        }

        private PricingPlanType GetPricingPlanType(DateTime date)
        {
            return date.DayOfWeek switch
            {
                DayOfWeek.Saturday or DayOfWeek.Sunday => PricingPlanType.Weekend,
                _ => PricingPlanType.Weekday,
            };
        }
        public IEnumerable<Log> GetLogsByDate(DateTime date)
        {
            return _context.Logs.Where(log => log.CheckIn.Date == date.Date);
        }

        public void Deletelogs(int logsId)
        {
            var logs = _context.Logs.Find(logsId);
            if (logs != null)
            {
                logs.isDeleted = true;
                _context.Entry(logs).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();


            }
        }

        public void UpdateCheckOutTime(int logId, DateTime newCheckOutTime)
        {
            var logs = _context.Logs.Find(logId);
            if(logs != null)
            {
                logs.CheckOut = newCheckOutTime;
                _context.SaveChanges();
            }
        }

        
        

      


    }
}
