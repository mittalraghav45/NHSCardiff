
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Models;
namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("api/incidents")]
    public class IncidentsApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public IncidentsApiController(
            ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetIncidents()
        {
            var incidents = await _context.Incidents
                .AsNoTracking()
                .ToListAsync();

            return Ok(incidents);
        }


        [HttpPost]
        public async Task<IActionResult> CreateIncident(
            [FromBody] Incident incident)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            incident.Id = 0;
            incident.ReportedAt = DateTime.Now;

            _context.Incidents.Add(incident);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetIncidents),
                new { id = incident.Id },
                incident
            );
        }


    }
}
