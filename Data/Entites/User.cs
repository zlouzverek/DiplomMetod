using Microsoft.AspNetCore.Identity;

namespace DiplomMetod.Data.Entites
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
