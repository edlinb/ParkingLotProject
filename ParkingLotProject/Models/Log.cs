using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingLotProject.Models
{
    public class Log
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Code { get; set; }
        public int? SubscriptionId { get; set; }
        public Subscription? Subscription { get; set; }
        public DateTime CheckIn { get; set; }= DateTime.Now;
        public DateTime? CheckOut { get; set; }

        [Column(TypeName = "decimal(18, 2)")]

        public decimal Price {  get; set; }
        public bool isDeleted {  get; set; }= false;

    }
}
