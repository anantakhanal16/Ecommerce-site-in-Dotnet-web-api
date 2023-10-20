using Microsoft.AspNetCore.Identity;
using System.Net.Sockets;

namespace Core.Entities.Identity
{
    public class AppUser :IdentityUser
    {
        public Address address;

        public string DisplayName { get; set; }

        public Address Address { get; set; }    
    }
}
