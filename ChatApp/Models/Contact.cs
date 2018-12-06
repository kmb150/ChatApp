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

        public Contact(string UserId,string ConcactId)
        {
            this.UserId = UserId;
            this.ContactId = ContactId;
        }
        public Contact()
        {
            this.UserId = "";
            this.ContactId = "";
        }
    }
    public class ContactsContext: DbContext
    {
        public System.Data.Entity.DbSet<ChatApp.Models.Contact> Contacts { get; set; }
    }

    public class UserContacts
    {
        public ApplicationUser User { get; set; }
        public List<ApplicationUser> Contacts { get; set; }

        public UserContacts(string currentUserId, ContactsContext contactsDb,UserManager<ApplicationUser> identityDb)
        {
            if (currentUserId != null)
            {
                this.User = identityDb.Users.Where(x => x.Id == currentUserId).FirstOrDefault();
                var contactsId = contactsDb.Contacts.Where(x => x.UserId == User.Id);
                Contacts = new List<ApplicationUser>();
                foreach (var contact in contactsId)
                {
                    Contacts.Add(identityDb.Users.Where(x => x.Id == contact.ContactId).FirstOrDefault());
                }
            }
        }
    }

    public class ContactsAndMessages
    {
        public UserContacts Contacts { get; set; }
        public List<Message> Messages { get; set; }
        public ApplicationUser SelectedContact { get; set; }

        public ContactsAndMessages(string currentUserId, ContactsContext contactsDb, UserManager<ApplicationUser> identityDb, string selectedContactUsername)
        {
            MessagesContext messagesDb = new MessagesContext();
            this.Contacts = new UserContacts(currentUserId,contactsDb,identityDb);
            SelectedContact= identityDb.Users.Where(x => x.UserName == selectedContactUsername).FirstOrDefault();
            this.Messages = messagesDb.Messages.Where(x => (x.FromUser == currentUserId && x.ToUser == SelectedContact.Id) || (x.FromUser == SelectedContact.Id && x.ToUser == currentUserId)).ToList();
        }
        public ContactsAndMessages(string currentUserId, ContactsContext contactsDb, UserManager<ApplicationUser> identityDb)
        {
            MessagesContext messagesDb = new MessagesContext();
            this.Contacts = new UserContacts(currentUserId, contactsDb, identityDb);
            this.Messages = new List<Message>();
        }
        public ContactsAndMessages()
        {

        }

        public string GetImageByContactUsername(string username)
        {
            foreach(var user in Contacts.Contacts)
            {
                if (user.UserName == username)
                    return user.ImageUrl;
            }
            return "";
        }
    }

}