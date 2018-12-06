using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ChatApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ChatApp.Controllers
{
    public class MessagesController : Controller
    {
        private MessagesContext db = new MessagesContext();
        private ContactsContext contactsContext = new ContactsContext();
        private ContactsAndMessages contactsAndMessages;

        protected ApplicationDbContext ApplicationDbContext { get; set; }
        
        protected UserManager<ApplicationUser> UserManager { get; set; }

        // GET: Messages
        public ActionResult Index()
        {
            this.ApplicationDbContext = new ApplicationDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.ApplicationDbContext));
            string currentUserId = User.Identity.GetUserId();
            if (TempData["cam"] != null)
            {
                contactsAndMessages = new ContactsAndMessages(currentUserId, contactsContext, this.UserManager,((ContactsAndMessages)TempData["cam"]).SelectedContact.UserName);
                //contactsAndMessages.SelectedContact= ApplicationDbContext.Users.Where(x => x.UserName == contactsAndMessages.SelectedContact.UserName).FirstOrDefault();
            }
            else
            {
                contactsAndMessages = new ContactsAndMessages(currentUserId, contactsContext, this.UserManager);
            }
            
            
            return View(contactsAndMessages);
        }
        [HttpPost]
        public ActionResult Index(string username)
        {
            this.ApplicationDbContext = new ApplicationDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.ApplicationDbContext));
            string currentUserId = User.Identity.GetUserId();

            ApplicationUser contactUser = UserManager.Users.Where(x => x.Email == username).FirstOrDefault();
            contactsAndMessages = new ContactsAndMessages(currentUserId, contactsContext, this.UserManager);
            if (contactUser == null)
            {
                contactsAndMessages.ErrorMessage = "A user with such username does not exist!";
            }
            else
            {
                Contact contact = contactsContext.Contacts.Where(x => x.UserId == contactUser.Id).FirstOrDefault();
                if (contact != null)
                {
                    contactsAndMessages.ErrorMessage = "The user you are trying to add is already in your contacts list!";
                }
                else
                {
                    AddContact(contactUser.Id, currentUserId);
                }
                
            }
            
            
            //Response.Write("console.log('into posted index')");
            return View(contactsAndMessages);
        }

        [HttpPost]
        public ActionResult Index2(ContactsAndMessages contactsAndMessages)
        {
            
            TempData["cam"] = contactsAndMessages;
            return RedirectToAction("Index","Messages");
        }

        private void AddContact(string id, string currentUserId)
        {
            Contact newContact = new Contact("","");
            newContact.UserId = currentUserId;
            newContact.ContactId = id;
            newContact.AddContact(contactsContext);
        }

        // GET: Messages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // GET: Messages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Messages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Text,Date,FromUser,ToUser")] Message message)
        {
            if (ModelState.IsValid)
            {
                db.Messages.Add(message);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(message);
        }

        // GET: Messages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // POST: Messages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Text,Date,FromUser,ToUser")] Message message)
        {
            if (ModelState.IsValid)
            {
                db.Entry(message).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(message);
        }

        // GET: Messages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Message message = db.Messages.Find(id);
            db.Messages.Remove(message);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
