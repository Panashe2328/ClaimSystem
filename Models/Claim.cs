using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClaimSystem.Models
{
    public class Claim
    {

        public int Id { get; set; }

        [Required]
        public string LecturerName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string LecturerEmail { get; set; }  

        [Required]
        public decimal HoursWorked { get; set; }

        [Required]
        public decimal HourlyRate { get; set; }

        public string AdditionalNotes { get; set; }

        public string? DocumentPath { get; set; }

        public string Status { get; set; } = "Pending"; // Default status

        public DateTime SubmissionDate { get; set; } = DateTime.Now;
    }
}
