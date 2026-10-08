
using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Incident
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Incident title is required")]
        [StringLength(100)]
        public string Title { get; set; } = "";

        [Required(ErrorMessage = "Location is required")]
        [StringLength(100)]
        public string Location { get; set; } = "";

        [Required(ErrorMessage = "Please select a severity")]
        [RegularExpression("Low|Medium|High",
            ErrorMessage = "Select Low, Medium or High")]
        public string Severity { get; set; } = "";

        public DateTime ReportedAt { get; set; } = DateTime.Now;
    }
}
