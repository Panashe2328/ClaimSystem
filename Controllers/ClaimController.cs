using ClaimSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Security.Claims;
using System.Linq;
using System;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace ClaimSystem.Controllers
{
    public class ClaimController : Controller
    {
        private static ClaimRepository _repository = new ClaimRepository();

        public IActionResult SubmitClaim()
        {
            return View();
        }

        public IActionResult HrView()
        {
            var claims = _repository.GetClaims();
            return View(claims); 
        }

        public IActionResult AdminView()
        {
            var claims = _repository.GetClaims(); // Get claims from the repository
            return View(claims);
        }

        [HttpPost]
        public IActionResult SubmitClaim(ClaimSystem.Models.Claim claim, IFormFile document)
        {
            if (ModelState.IsValid)
            {
                if (document != null && document.Length > 0)
                {
                    // File validation logic
                    var allowedExtensions = new[] { ".pdf", ".docx", ".xlsx" };
                    var fileExtension = Path.GetExtension(document.FileName).ToLower();

                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        ModelState.AddModelError("DocumentPath", "Only PDF, DOCX, or XLSX files are allowed.");
                        return View(claim);
                    }

                    // Ensure the folder exists
                    var folderPath = Path.Combine("wwwroot", "Documents");
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    // Save the file to wwwroot/documents
                    var fileName = Path.GetFileName(document.FileName);
                    var filePath = Path.Combine(folderPath, fileName);

                    try
                    {
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            document.CopyTo(stream);
                        }
                        claim.DocumentPath = filePath; // Store the file path in the claim
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("DocumentPath", "File upload failed: " + ex.Message);
                        return View(claim);
                    }
                }
                else
                {
                    ModelState.AddModelError("DocumentPath", "Please upload a document.");
                    return View(claim);
                }

                // Set claim data
                claim.LecturerName = User.Identity.Name;
                claim.LecturerEmail = User.FindFirstValue(ClaimTypes.Email);
                claim.Status = "Pending"; // Initial claim status

                // Add claim to the repository
                _repository.AddClaim(claim);

                // Redirect to the claim status view after successful submission
                return RedirectToAction("ClaimStatus");
            }
            return View(claim); // Return to the form if there are validation errors
        }

        public IActionResult ClaimStatus()
        {
            var claims = _repository.GetClaims();
            return View(claims);
        }

        public IActionResult ApproveClaim(int id)
        {
            _repository.UpdateClaimStatus(id, "Approved");
            return RedirectToAction("ClaimStatus");
        }

        public IActionResult RejectClaim(int id)
        {
            _repository.UpdateClaimStatus(id, "Rejected");
            return RedirectToAction("ClaimStatus");
        }


        public IActionResult GenerateInvoice(int claimId)
        {
            var claim = _repository.GetClaims().FirstOrDefault(c => c.Id == claimId);
            if (claim == null)
            {
                return NotFound();
            }

            // Create a PDF document
            var doc = new Document();
            var stream = new MemoryStream();
            var writer = PdfWriter.GetInstance(doc, stream);
            doc.Open();

            // Add content to the PDF
            doc.Add(new Paragraph($"Invoice for Claim ID: {claim.Id}"));
            doc.Add(new Paragraph($"Lecturer: {claim.LecturerName}"));
            doc.Add(new Paragraph($"Email: {claim.LecturerEmail}"));
            doc.Add(new Paragraph($"Hours Worked: {claim.HoursWorked}"));
            doc.Add(new Paragraph($"Hourly Rate: {claim.HourlyRate:C}"));
            doc.Add(new Paragraph($"Total Amount: {claim.HoursWorked * claim.HourlyRate:C}"));
            doc.Add(new Paragraph($"Claim Status: {claim.Status}"));
            doc.Add(new Paragraph($"Reason for Rejection (if any): {claim.ReasonForRejection}"));

            doc.Close();

            // Return the PDF as a file to the user
            return File(stream.ToArray(), "application/pdf", $"Invoice_Claim_{claim.Id}.pdf");
        }

    }
}
