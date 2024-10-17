using System.Collections.Generic;
namespace ClaimSystem.Models
{
    public class ClaimRepository
    {
        private static List<Claim> claims = new List<Claim>();
        private static int nextId = 1;

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
