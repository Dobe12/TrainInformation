using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TrainInformation.Data;
using TrainInformation.Names;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TrainInformation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainInformationController : ControllerBase
    {
        private readonly TrainContext _context;

        public TrainInformationController(TrainContext context)
        {
            _context = context;
        }

        // GET api/[controller]/rides
        [HttpGet("rides")]
        [ProducesResponseType(typeof(IEnumerable<Ride>), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get()
        {
            var allRides = await _context.Rides.ToListAsync();

            if (allRides == null)
            {
                NotFound("Rides not found");
            }

            return Ok(allRides);
        }

        // GET api/[controller]/rides/{depStation}/{arrivStation}
        [HttpGet("rides/{depStation}/{arrivStation}")]
        [ProducesResponseType(typeof(IEnumerable<Ride>), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetRidesByStationNames(string depStation, string arrivStation)
        {
            var rides = await _context.Rides
                .Where(f => f.DepartureStation == depStation
                            && f.ArrivalStation == arrivStation)
                .ToListAsync();

            if (rides == null)
            {
                NotFound("Rides not found");
            }

            return Ok(rides);
        }

        // GET api/[controller]/rides/{depStation}/{depTime}/{arrivStation}/{arrivTime}/
        [HttpGet("rides/{depStation}/{depTime}/{arrivStation}/{arrivTime}/")]
        [ProducesResponseType(typeof(IEnumerable<Ride>), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetRidesByCodeAndTime(
            string depStation, DateTime depTime,
            string arrivStation, DateTime arrivTime)
        {
            var rides = await _context.Rides
                .Where(f => f.DepartureStation == depStation
                            && f.ArrivalStation == arrivStation
                            && f.DepartureTime.Day == depTime.Day
                            && f.ArrivalTime.Day == arrivTime.Day)
                .ToListAsync();

            if (rides == null)
            {
                NotFound("Rides not found");
            }

            return Ok(rides);
        }

        // GET api/[controller]/rides/{arrivTimeStart}/{arrivTimeEnd}/{arrivStation}
        [HttpGet("rides/{arrivTimeStart}/{arrivTimeEnd}/{arrivStation}")]
        [ProducesResponseType(typeof(IEnumerable<Ride>), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetArrivRidesByTimeAndStation(DateTime arrivTimeStart, DateTime arrivTimeEnd,
            string arrivStation)
        {
            var rides = await _context.Rides
                .Where(f => f.ArrivalStation == arrivStation
                            && IsRange(f.ArrivalTime, arrivTimeStart, arrivTimeEnd))
                .ToListAsync();

            if (rides == null)
            {
                NotFound("Rides not found");
            }

            return Ok(rides);
        }

        // GET api/[controller]/rides/{depTimeStart}/{depTimeEnd}/{depStation}
        [HttpGet("rides/{depTimeStart}/{depTimeEnd}/{depStation}")]
        [ProducesResponseType(typeof(IEnumerable<Ride>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetDepRidesByTimeAndStation(DateTime depTimeStart, DateTime depTimeEnd,
            string depStation)
        {
            var rides = await _context.Rides
                .Where(f => f.DepartureStation == depStation
                            && IsRange(f.DepartureTime, depTimeStart, depTimeEnd))
                .ToListAsync();

            if (rides == null)
            {
                NotFound("Rides not found");
            }

            return Ok(rides);
        }

        private static bool IsRange(DateTime dateToCheck, DateTime startDate, DateTime endDate)
        {
            return dateToCheck >= startDate && dateToCheck < endDate;
        }
    }

}