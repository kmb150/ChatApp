using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ChatApp.Models
{
    public class Contact
    {
        [Key, Column(Order = 0)]
        public int UserId { get; set; }
        [Key, Column(Order = 1)]
        public int ContactId { get; set; }
    }
    public class ContactsContext: DbContext
    {
        public System.Data.Entity.DbSet<ChatApp.Models.Contact> Contacts { get; set; }
    }
}