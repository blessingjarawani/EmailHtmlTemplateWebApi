using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailTemplate.DAL.Dictionary
{
    public enum MessageStatus
    {

        NotSent = -2,
        TemplateNotFound = -1,
        Sent = 1,
    }
}
