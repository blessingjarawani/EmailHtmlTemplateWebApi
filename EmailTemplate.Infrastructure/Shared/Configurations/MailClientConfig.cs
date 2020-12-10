using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailTemplate.Infrastructure.Shared.Configurations
{
    public class MailClientConfig
    {
        public string MailFrom { get; set; }
        public bool Ssl { get; set; }
        public bool UseDefaultCredentials { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public MailCredentials Credentials { get; set; }
    }

    public class MailCredentials
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
