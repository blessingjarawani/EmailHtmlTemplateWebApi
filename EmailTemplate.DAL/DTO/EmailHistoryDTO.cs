using EmailTemplate.DAL.Dictionary;
using EmailTemplate.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailTemplate.DAL.DTO
{
    public class EmailHistoryDTO
    {
        public MessageStatus Status { get; private set; }
        public string Email { get; private set; }
        public string Name { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public string CreatedDateString => CreatedDate.ToString("dd-MM-yyyy");

        public static EmailHistoryDTO Create(EmailHistory emailHistory)
        {
            if (emailHistory != null)
                return new EmailHistoryDTO
                {
                    Email = emailHistory.Email,
                    CreatedDate = emailHistory.CreatedDate,
                    Name = emailHistory.Name,
                    Status = emailHistory.Status
                };
            return null;

        }
    }
}
