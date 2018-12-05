using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Runtime.Remoting.Contexts;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ChatApp.Models
{
    public class Contact
    {
        [Key, Column(Order = 0)]
        public string UserId { get; set; }
        [Key, Column(Order = 1)]
        public string ContactId { get; set; }

        public void AddContact(ContactsContext contactsContext)
        {
            contactsContext.Contacts.Add(this);
            contactsContext.SaveChanges();
        }

        public void DeleteContact(ContactsContext contactsContext)
        {
            contactsContext.Contacts.Remove(this);
            contactsContext.SaveChanges();
        }
    }
    public class ContactsContext: DbContext
    {
        public System.Data.Entity.DbSet<ChatApp.Models.Contact> Contacts { get; set; }
    }

    public class UserContacts
    {
        public string UserId { get; set; }
        public List<ApplicationUser> Contacts { get; set; }

        public UserContacts(string currentUserId, ContactsContext contactsDb,UserManager<ApplicationUser> identityDb)
        {
            this.UserId = currentUserId;
            var contactsId=contactsDb.Contacts.Where(x => x.UserId == UserId);
            Contacts = new List<ApplicationUser>();
            foreach(var contact in contactsId)
            {
                Contacts.Add(identityDb.Users.Where(x => x.Id == contact.ContactId).FirstOrDefault());
            }
        }
    }

    public class ContactsAndMessages
    {
        public UserContacts Contacts { get; set; }
        public List<Message> Messages { get; set; }

        public ContactsAndMessages(string currentUserId, ContactsContext contactsDb, UserManager<ApplicationUser> identityDb)
        {
            this.Contacts = new UserContacts(currentUserId,contactsDb,identityDb);
            this.Messages = new List<Message>();
        }
        public ContactsAndMessages()
        {

        }
    }

}