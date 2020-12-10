using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailTemplate.Infrastructure.DTO
{
    public class EmailDTO
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Body { get; set; }
        public string Topic { get; set; }
        public bool IsHtml => true;
    }
}
