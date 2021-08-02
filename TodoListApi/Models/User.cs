using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TodoListApi.Models
{
    public class User
    {
        [Column(TypeName = "nvarchar(450)")]
        public string UserId { get; set; }
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
