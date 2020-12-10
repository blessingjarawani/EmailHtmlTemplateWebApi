using EmailTemplate.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailTemplate.DAL.DTO
{
    public class TemplateDTO
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public static TemplateDTO Create(Template template)
        {
            if (template != null)
                return new TemplateDTO
                {
                    Body = template.Body,
                    Subject = template.Subject,
                    Id = template.Id
                };
            return null;
        }
    }
}
