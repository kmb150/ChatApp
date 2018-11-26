using Microsoft.AspNet.Identity.EntityFramework;

namespace ChatApplication.App_Start
{
    public class AppRole : IdentityRole
    {
        public AppRole() : base() { }
        public AppRole(string name) : base(name) { }
        // extra properties here 
    }

    public class MyDbContext : IdentityDbContext<AppUser>
    {
        // Other part of codes still same 
        // You don't need to add AppUser and AppRole 
        // since automatically added by inheriting form IdentityDbContext<AppUser>
    }
}