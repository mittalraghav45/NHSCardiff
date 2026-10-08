using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.Data;
namespace WebApplication2.Controllers
{
    public class IncidentController : Controller
    {
        private readonly ApplicationDbContext _context;
        public IncidentController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Incident incident)
        {
            if (!ModelState.IsValid)
            {
                return View(incident);
            }

            _context.Incidents.Add(incident);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index()
        {
            var incidents = await _context.Incidents.ToListAsync();
            return View(incidents);
        }
        public async Task<IActionResult> Details(int id)
        {
            var incident = await _context.Incidents
                .FirstOrDefaultAsync(i => i.Id == id);

            if (incident == null)
            {
                return NotFound("Incident not found");
            }

            return View(incident);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var incident = await _context.Incidents.FindAsync(id);

            if (incident == null)
            {
                return NotFound("Incident not found");
            }

            return View(incident);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Incident updatedIncident)
        {
            if (id != updatedIncident.Id)
            {
                return BadRequest("Incident ID mismatch");
            }

            if (!ModelState.IsValid)
            {
                return View(updatedIncident);
            }

            var incident = await _context.Incidents.FindAsync(id);

            if (incident == null)
            {
                return NotFound("Incident not found");
            }

            incident.Title = updatedIncident.Title;
            incident.Location = updatedIncident.Location;
            incident.Severity = updatedIncident.Severity;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var incident = await _context.Incidents.FindAsync(id);

            if (incident == null)
            {
                return NotFound("Incident not found");
            }

            return View(incident);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var incident = await _context.Incidents.FindAsync(id);

            if (incident == null)
            {
                return NotFound("Incident not found");
            }

            _context.Incidents.Remove(incident);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

    }
}