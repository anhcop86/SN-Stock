using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhimHang.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        public ApplicationUser()
        {
            //this.Verify = Verify.;
        }
     
        public virtual UserExtentLogin UserExtentLogin { get; set; }
         
    }
    [Table("UserLogins")]
    public class UserExtentLogin
    {
        public int Id { get; set; }
        public string KeyLogin { get; set; }

        [StringLength(256)]
        public string UserNameCopy { get; set; }
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

        public AccountType AccountType { get; set; }

        public LockAccount LockAccount { get; set; }

        [StringLength(128)]
        public string Status { get; set; }



    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<IdentityUser>()
        //        .ToTable("UserLogins");
        //    modelBuilder.Entity<ApplicationUser>()
        //        .ToTable("UserLogins");

        //    //modelBuilder. = false;
        //}

        public DbSet<UserExtentLogin> UserLogin { get; set; }
    }


    public enum Verify
    {
        NO,YES
    }

    public enum AccountType
    {
        Customer, Broker, Analyser
    }
    public enum LockAccount
    {
        Unlock, Lock
    }
}