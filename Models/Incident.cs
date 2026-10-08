
using System;

namespace WebApplication2.Models
{
    public class Incident
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Location { get; set; } = "";
        public string Severity { get; set; } = "";
        public DateTime ReportedAt { get; set; } = DateTime.Now;
    }
}
