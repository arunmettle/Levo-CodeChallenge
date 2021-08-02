using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TodoListApi.Models
{
    public class TodoList
    {
        public int TodoListId { get; set; }
        public string Todo { get; set; }
        [Column(TypeName = "nvarchar(450)")]
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
