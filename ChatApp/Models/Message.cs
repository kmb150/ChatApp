using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
namespace ChatApp.Models
{
    public class Message
    {
        [Key]
        public int ID { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public string FromUser { get; set; }
        public string ToUser { get; set; }
        public Boolean IsRead { get; set; }

        public void AddMessage(MessagesContext db)
        {
            db.Messages.Add(this);
            db.SaveChanges();
        }

        public Message(string text,DateTime date,string fromUser, string toUser)
        {
            this.Text = text;
            this.Date = date;
            this.FromUser = fromUser;
            this.ToUser = toUser;
            this.IsRead = false;
        }
        public Message()
        {
            Text = "";
            Date = DateTime.Now;
            FromUser = "";
            ToUser = "";
            IsRead = false;
        }

        public string GetFormattedDate()
        {
            if (Date.Day == DateTime.Now.Day)
            {
                return Date.ToString("H:mm");
            }
            if(Date.Day > DateTime.Now.AddDays(-6).Day)
            {
                return Date.ToString("ddd 'at' H:mm");
            }
            else
            {
                return Date.ToString("ddd, dd MMM H:mm");
            }
        }

    }

}