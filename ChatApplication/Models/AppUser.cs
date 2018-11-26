using Microsoft.AspNet.Identity.EntityFramework;

namespace ChatApplication.App_Start
{
    public class AppUser: IdentityUser
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
    }

    
}