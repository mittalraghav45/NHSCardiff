using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
namespace WebApplication2.Controllers
{
    public class IncidentController : Controller
    {

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

 

[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Create(Incident incident)
{
    if (!ModelState.IsValid)
    {
        return View(incident);
    }

    return Content(
        $"Valid incident: {incident.Title}, " +
        $"Location: {incident.Location}, " +
        $"Severity: {incident.Severity}"
    );
}



        public IActionResult Index()
        {
            var incidents = new List<WebApplication2.Models.Incident>
    {
        new WebApplication2.Models.Incident
        {
            Id = 101,
            Title = "Water contamination reported",
            Location = "Cardiff",
            Severity = "High",
            ReportedAt = DateTime.Now
        },
        new WebApplication2.Models.Incident
        {
            Id = 102,
            Title = "Food poisoning outbreak",
            Location = "Swansea",
            Severity = "Medium",
            ReportedAt = DateTime.Now
        },
        new WebApplication2.Models.Incident
        {
            Id = 103,
            Title = "Air quality concern",
            Location = "Newport",
            Severity = "Low",
            ReportedAt = DateTime.Now
        }
    };

            return View(incidents);


        }

        public IActionResult Details(int id)
        {
            var incidents = new List<WebApplication2.Models.Incident>
    {
        new() { Id = 101, Title = "Water contamination reported",
                Location = "Cardiff", Severity = "High" },

        new() { Id = 102, Title = "Food poisoning outbreak",
                Location = "Swansea", Severity = "Medium" },

        new() { Id = 103, Title = "Air quality concern",
                Location = "Newport", Severity = "Low" }
    };

            var incident = incidents.FirstOrDefault(i => i.Id == id);

            if (incident == null)
            {
                return NotFound("Incident not found");
            }

            return View(incident);
        }


    }
}
