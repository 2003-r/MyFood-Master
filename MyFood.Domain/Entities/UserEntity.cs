using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFood.Domain.Entities
{
    public class UserEntity
    {
        [MaxLength(20)]
        public string? Name { get; set; }
        [MaxLength(50)]
        public string? Email { get; set; }
        [MaxLength(500)]
        public string? Password { get; set; }
        
    }
}
