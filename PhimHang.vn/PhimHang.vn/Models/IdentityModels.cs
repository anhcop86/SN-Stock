using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System;

namespace PhimHang.vn.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        public ApplicationUser()
        {
            //this.Verify = Verify.;
        }

        public Nullable<DateTime> BirthDate { get; set; }

        [StringLength(256)]
        public String AvataImage { get; set; }

        [StringLength(256)]
        public String AvataCover { get; set; }

        [StringLength(256)]
        public string FullName { get; set; }

        [StringLength(256)]
        public string Email { get; set; }

        public DateTime CreatedDate { get; set; }
                
        public Verify Verify { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }
    }

    public enum Verify
    {
        NO,YES
    }
}