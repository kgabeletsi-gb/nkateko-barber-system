using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NkatekoBarberWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NkatekoBarberWeb.Pages
{
    public class BookingModel : PageModel
    {
        private readonly AppDbContext _context;

        public BookingModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Booking NewBooking { get; set; } = new();

        public List<Service> Services { get; set; } = new();

        public string? Message { get; set; }

        public void OnGet()
        {
            LoadServices();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            LoadServices();

            if (!ModelState.IsValid)
                return Page();

            _context.Bookings.Add(NewBooking);
            await _context.SaveChangesAsync();

            Message = "Booking saved to database successfully!";
            ModelState.Clear();

            return Page();
        }

        private void LoadServices()
        {
            Services = new List<Service>
            {
                new Service { Name = "Adults Hair Cut", Price = 200 },
                new Service { Name = "Kids Hair Cut", Price = 150 },
                new Service { Name = "Chiskop Machine", Price = 80 },
                new Service { Name = "Beard Machine", Price = 40 },
                new Service { Name = "Beard Razor", Price = 50 },
                new Service { Name = "Chiskop Razor", Price = 100 },
                new Service { Name = "Bleach", Price = 120 },
                new Service { Name = "Colours", Price = 120 },
                new Service { Name = "Black Dye", Price = 100 },
                new Service { Name = "Wash", Price = 50 },
                new Service { Name = "Edge Up", Price = 100 },
                new Service { Name = "Beard Dye", Price = 100 }
            };
        }
    }
}