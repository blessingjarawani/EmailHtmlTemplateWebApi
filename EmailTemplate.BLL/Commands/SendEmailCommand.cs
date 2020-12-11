using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace EmailTemplate.BLL.Commands
{
    public class SendEmailCommand
    {
        [Required]
        public int TemplateId { get; set; }
        [Required]
        [EmailAddressAttribute]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
