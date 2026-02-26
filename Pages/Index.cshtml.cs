using Microsoft.AspNetCore.Mvc.RazorPages;
using NkatekoBarberWeb.Models;
using System.Collections.Generic;

namespace NkatekoBarberWeb.Pages
{
    public class IndexModel : PageModel
    {
        public List<Service> Services { get; set; } = new();

        public void OnGet()
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