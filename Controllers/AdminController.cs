using Microsoft.AspNetCore.Mvc;
using ClaimSystem.Models; 
using System.Collections.Generic;

namespace ClaimSystem.Controllers
{
    public class AdminController : Controller
    {
        
        private static List<Claim> claims = new List<Claim>();

        public IActionResult ClaimOverview()
        {
            return View(claims); // Pass the list of claims to the view
        }
    }
}
