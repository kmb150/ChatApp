using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
namespace ChatApplication.Models
{
    public class User : IdentityUser
    {
       
    }
    public class MyDbContext : IdentityDbContext<User>
    {
        // Other part of codes still same 
        // You don't need to add AppUser and AppRole 
        // since automatically added by inheriting form IdentityDbContext<AppUser>
    }
}