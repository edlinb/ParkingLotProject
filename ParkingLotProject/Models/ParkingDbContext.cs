using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ParkingLotProject.Models
{
    

        public partial class ParkingDbContext : DbContext
        {
            public ParkingDbContext()
            { }
            public ParkingDbContext(DbContextOptions<ParkingDbContext> options) : base(options)
            { }
            public virtual DbSet<Log> Logs { get; set; }
            public virtual DbSet<ParkingSpot> ParkingSpot { get; set; }
            public virtual DbSet<PricingPlan> PricingPlan { get; set; }
            public virtual DbSet<Subscriber> Subscriber { get; set; }
            public virtual DbSet<Subscription> Subscription { get; set; }


               }
        }
    
