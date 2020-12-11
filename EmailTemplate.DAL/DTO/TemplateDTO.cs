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
        public int Id { get; private set; }
        public string Subject { get; private set; }
        public string Body { get; private set; }

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
