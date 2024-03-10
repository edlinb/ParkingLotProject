using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingLotProject.Models
{
    public class PricingPlan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {  get; set; }
        public decimal HourlyPricing {  get; set; }
        public decimal DailyPricing { get; set; }
        public int MinimumHours { get; set; }
        public PricingPlanType Type { get; set; }
        //koment nga edlin
    }
    public enum PricingPlanType
    {
        Weekday,
        Weekend
    }
}
