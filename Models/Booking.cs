using System;

namespace NkatekoBarberWeb.Models
{
    public class Booking
    {
        public int Id { get; set; }   // IMPORTANT for database

        public string CustomerName { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public DateTime AppointmentDate { get; set; }
        public string ServiceName { get; set; } = "";
    }
}