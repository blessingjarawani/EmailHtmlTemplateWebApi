using EmailTemplate.DAL.Dictionary;
using EmailTemplate.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailTemplate.Infrastructure.Shared.Context
{
    public class EmailContext : IContext
    {
        public int TemplateId { get; set; }
        public string EmailAddress { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
        public MessageStatus SendingStatus { get; set; }
        public TemplateDTO Template { get; set; }
    }
}
