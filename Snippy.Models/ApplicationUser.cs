using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;

using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace Snippy.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
            : base()
        {
            this.Snippets = new HashSet<Snippet>();
            this.Comments = new HashSet<Comment>();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public virtual ICollection<Snippet> Snippets { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
