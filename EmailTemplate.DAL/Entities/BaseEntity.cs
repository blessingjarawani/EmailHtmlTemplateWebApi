using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailTemplate.DAL.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        [StringLength(50)]
        public string CreatedUser { get; set; }
        [StringLength(50)]
        public string UpdatedUser { get; set; }
    }
}
