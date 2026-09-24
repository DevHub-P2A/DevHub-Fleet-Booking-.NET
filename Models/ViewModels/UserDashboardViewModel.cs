
using BUA_project.Models;

namespace BUA_project.Models.ViewModels
{
    public class UserDashboardViewModel
    {
        // Metrics
        public int AvailableVehiclesCount { get; set; }

        public int MyActiveRequestsCount { get; set; }

        public int ApprovedUpcomingTripsCount { get; set; }

        public int CompletedTripsCount { get; set; }


        // Vehicles
        public List<Vehicle> Vehicles { get; set; } = new List<Vehicle>();


        // Filter values
        public string? Type { get; set; }

        public string? Brand { get; set; }

        public int? Seats { get; set; }

        public string? FuelType { get; set; }

        public string? Transmission { get; set; }

        public string? Accessibility { get; set; }
    }
}

