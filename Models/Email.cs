using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServeBook_Backend.Models
{
    public class Email
    {
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}