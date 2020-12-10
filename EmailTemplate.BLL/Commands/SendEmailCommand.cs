using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace EmailTemplate.BLL.Commands
{
    public class SendEmailCommand
    {
        public int TemplateId { get; set; }

        [EmailAddressAttribute]
        public string EmailAddress { get; set; }
        public string Name { get; set; }
    }
}
