using ClaimSystem.Models;
// using ClaimSystem.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Security.Claims;

namespace ClaimSystem.Controllers
{
    public class ClaimController : Controller
    {
        private static ClaimRepository _repository = new ClaimRepository();

        public IActionResult SubmitClaim()
        {
            return View();
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
                    var filePath = Path.Combine("wwwroot/documents", document.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        document.CopyTo(stream);
                    }
                    claim.DocumentPath = filePath; // Store file path
                }

                claim.LecturerName = User.Identity.Name; // Assuming user is logged in
                claim.LecturerEmail = User.FindFirstValue(System.Security.Claims.ClaimTypes.Email); // Get lecturer's email

                _repository.AddClaim(claim);
                return RedirectToAction("ClaimStatus");
            }
            return View(claim);
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
    }
}
