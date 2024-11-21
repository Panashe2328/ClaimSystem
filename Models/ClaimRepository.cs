using System;
using System.Collections.Generic;

namespace ClaimSystem.Models
{
    public class ClaimRepository
    {
        private static List<Claim> claims = new List<Claim>
        {
            new Claim
            {
                Id = 1,
                LecturerName = "John Doe",
                LecturerEmail = "john.doe@example.com",
                HoursWorked = 10,
                HourlyRate = 50,
                AdditionalNotes = "Completed project work.",
                DocumentPath = "/documents/john_doe_project.pdf", // Placeholder file path
                Status = "Pending",
                SubmissionDate = DateTime.Now.AddDays(-5)
            },
            new Claim
            {
                Id = 2,
                LecturerName = "Jane Smith",
                LecturerEmail = "jane.smith@example.com",
                HoursWorked = 8,
                HourlyRate = 45,
                AdditionalNotes = "Prepared lecture materials.",
                DocumentPath = "/documents/jane_smith_lecture.docx", // Placeholder file path
                Status = "Approved",
                SubmissionDate = DateTime.Now.AddDays(-3)
            },
            new Claim
            {
                Id = 3,
                LecturerName = "Alice Johnson",
                LecturerEmail = "alice.johnson@example.com",
                HoursWorked = 12,
                HourlyRate = 55,
                AdditionalNotes = "Conducted workshop.",
                DocumentPath = "/documents/alice_johnson_workshop.xlsx", // Placeholder file path
                Status = "Rejected",
                SubmissionDate = DateTime.Now.AddDays(-1)
            }
        };

        private static int nextId = claims.Count + 1;

        public void AddClaim(Claim claim)
        {
            claim.Id = nextId++;
            claims.Add(claim);
        }

        public List<Claim> GetClaims() => claims;

        public void UpdateClaimStatus(int id, string status)
        {
            var claim = claims.Find(c => c.Id == id);
            if (claim != null)
            {
                claim.Status = status;
            }
        }
    }
}
