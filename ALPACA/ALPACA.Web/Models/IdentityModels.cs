﻿//using System.Data.Entity;
//using System.Security.Claims;
//using System.Threading.Tasks;
//using Microsoft.AspNet.Identity;
//using NHibernate.AspNet.Identity;

//namespace ALPACA.Entities
//{
//    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
//    public class ApplicationUser : IdentityUser
//    {
//        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
//        {
//            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
//            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
//            // Add custom user claims here
//            return userIdentity;
//        }

//        public virtual MyUserInfo MyUserInfo { get; set; }
//    }

//    public class MyUserInfo
//    {
//        public int Id { get; set; }
//        public string firstname { get; set; }
//        public string lastname { get; set; }
//    }

//    //public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
//    //{
//    //    public ApplicationDbContext()
//    //        : base("DefaultConnection", throwIfV1Schema: false)
//    //    {
//    //    }

//    //    public static ApplicationDbContext Create()
//    //    {
//    //        return new ApplicationDbContext();
//    //    }

//    //    public System.Data.Entity.DbSet<MyUserInfo> MyUserInfo { get; set; }
//    //}
//}