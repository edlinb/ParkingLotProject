﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingLotProject.Models
{
    public class Subscription
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        
        
   
        public int Id {  get; set; }
        
        public int Code { get; set; }
        
        public int SubscriberId {  get; set; }
        public Subscriber Subscriber { get; set; }
        public decimal Price { get; set; }
        public decimal Discountvalue {  get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool isDeleted { get; set; }=false;

    }
}
