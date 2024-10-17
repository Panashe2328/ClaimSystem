using System.ComponentModel.DataAnnotations;

namespace ClaimSystem.Models
{
    public class Claim
    {

        public int Id { get; set; }

        [Required]
        public string LecturerName { get; set; }

        [Required]
        public decimal HoursWorked { get; set; }

        [Required]
        public decimal HourlyRate { get; set; }

        public string AdditionalNotes { get; set; }

        public string DocumentPath { get; set; }

        public string Status { get; set; } = "Pending"; // Default status

        public DateTime SubmissionDate { get; set; } = DateTime.Now;
    }
}
