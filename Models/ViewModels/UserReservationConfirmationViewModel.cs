
using BUA_project.Models;

namespace BUA_project.Models.ViewModels
{
    public class UserReservationConfirmationViewModel
    {
        // Reservation data
        public int VehicleId { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public string Origin { get; set; }

        public string Destination { get; set; }

        public int Passengers { get; set; }

        public double Load { get; set; }

        public string Purpose { get; set; }


        // Vehicle
        public Vehicle Vehicle { get; set; }


        // Route Estimate
        public double Distance { get; set; }

        public TimeSpan Duration { get; set; }

        public string RouteProvider { get; set; }


        // Fuel Prediction
        public double EstimatedFuel { get; set; }

        public double FuelMin { get; set; }

        public double FuelMax { get; set; }

        public double FuelCost { get; set; }

        public double FuelCostMin { get; set; }

        public double FuelCostMax { get; set; }

        public string FuelMethod { get; set; }

        public string FuelAssumptions { get; set; }

        // Fuel price used
        public double FuelPricePerLiter { get; set; }
    }
}

