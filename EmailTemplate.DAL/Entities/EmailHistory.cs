using EmailTemplate.DAL.Dictionary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailTemplate.DAL.Entities
{
    public class EmailHistory : BaseEntity
    {
        public MessageStatus Status { get; set; }
        [StringLength(150)]
        public string Email { get; set; }
        [StringLength(50)] 
        public string Name { get; set; }
        [ForeignKey("TemplateId")]
        public virtual Template Template { get; set; }

    }
}
