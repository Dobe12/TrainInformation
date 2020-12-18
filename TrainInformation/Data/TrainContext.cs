using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainInformation.Names;
using Microsoft.EntityFrameworkCore;

namespace TrainInformation.Data
{
    public sealed class TrainContext : DbContext
    {
        public DbSet<Ride> Rides { get; set; }
        public DbSet<Train> Trains { get; set; }



        public TrainContext(DbContextOptions<TrainContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Train>().HasData(new List<Train>
            {
                new Train
                {
                    Id = 1,
                    Name = "Ласточка",
                    WagonsCount = 18,
                    Capacity = 550
                },
                new Train
                {
                    Id = 2,
                    Name = "Сапсан",
                    WagonsCount = 23,
                    Capacity = 770
                },
            });

            modelBuilder.Entity<Ride>().HasData(new List<Ride>
            {
                new Ride
                {
                    Id = 1,
                    DepartureCountry = "Russia",
                    DepartureCity = "Moscow",
                    DepartureStation = "Ярославский",
                    ArrivalStation = "Иркутск-Пассажирский",
                    ArrivalCountry = "Russia",
                    ArrivalCity = "Irkutsk",
                    DepartureTime = new DateTime(2020, 12, 11, 1, 12, 12),
                    ArrivalTime = new DateTime(2020, 12, 11, 2, 00, 12),
                    IsRoundTrip = true,
                    DepartureTrainId = 1,
                    ArrivalTrainId = 2
                }
            });
        }
    }
}
