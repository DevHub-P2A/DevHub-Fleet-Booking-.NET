namespace BUA_project.Models.ViewModels
{
    public class UserReservationViewModel
    {
        public int VehicleId { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public string Origin { get; set; }

        public string Destination { get; set; }

        public int Passengers { get; set; }

        public double Load { get; set; }

        public string Purpose { get; set; }
    }
}