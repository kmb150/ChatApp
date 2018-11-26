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
        public int FromUser { get; set; }
        public int ToUser { get; set; }
    }

}