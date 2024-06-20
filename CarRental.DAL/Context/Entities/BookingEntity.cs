namespace CarRental.DAL.Context.Entities
{
    public class BookingEntity
    {
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }

        public Guid VehicleId { get; set; }

        public DateTime BookingDate { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public double TotalPrice { get; set; }


        public CustomerEntity Customer { get; set; }
        public VehicleEntity Vehicle { get; set; }
    }
}
