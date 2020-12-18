using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainInformation.Names
{
    public class Ride
    {
        public int Id { get; set; }
        public string DepartureCountry { get; set; }
        public string ArrivalCountry { get; set; }
        public string DepartureCity { get; set; }
        public string ArrivalCity { get; set; }
        public string DepartureStation { get; set; }
        public string ArrivalStation { get; set; }
        public bool IsRoundTrip { get; set; }

        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public TimeSpan RideTime => ArrivalTime - DepartureTime;


        public int? DepartureTrainId { get; set; }
        public Train DepartureTrain { get; set; }
        public int? ArrivalTrainId { get; set; }
        public Train ArrivalTrain { get; set; }
    }
}
