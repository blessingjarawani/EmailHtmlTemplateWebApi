using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailTemplate.DAL.Entities
{
    public class Template : BaseEntity
    {
        [StringLength(50)]
        public string Subject { get; set; }
        public string Body { get; set; }
        public ICollection<EmailHistory> EmailHistory { get; set; }
    }
}
