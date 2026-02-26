using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NkatekoBarberWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NkatekoBarberWeb.Pages
{
    public class AdminModel : PageModel
    {
        private readonly AppDbContext _context;

        public AdminModel(AppDbContext context)
        {
            _context = context;
        }

        public List<Booking> Bookings { get; set; } = new();
        public List<Booking> TodayBookings { get; set; } = new();

        public decimal TotalIncome { get; set; }
        public decimal TodayIncome { get; set; }

        public DateTime? SelectedDate { get; set; }

        public void OnGet(DateTime? selectedDate)
        {
            // If no date selected, use today
            SelectedDate = selectedDate ?? DateTime.Today;

            var date = SelectedDate.Value.Date;

            // Get all bookings ordered newest first
            Bookings = _context.Bookings
                               .OrderByDescending(b => b.AppointmentDate)
                               .ToList();

            // Filter bookings by selected date
            TodayBookings = Bookings
                            .Where(b => b.AppointmentDate.Date == date)
                            .ToList();

            // Calculate total income (all time)
            TotalIncome = Bookings.Sum(b => GetServicePrice(b.ServiceName));

            // Calculate income for selected date
            TodayIncome = TodayBookings.Sum(b => GetServicePrice(b.ServiceName));
        }

        private decimal GetServicePrice(string serviceName)
        {
            return serviceName switch
            {
                "Adults Hair Cut" => 200,
                "Kids Hair Cut" => 150,
                "Chiskop Machine" => 80,
                "Beard Machine" => 40,
                "Beard Razor" => 50,
                "Chiskop Razor" => 100,
                "Bleach" => 120,
                "Colours" => 120,
                "Black Dye" => 100,
                "Wash" => 50,
                "Edge Up" => 100,
                "Beard Dye" => 100,
                _ => 0
            };
        }
    }
}