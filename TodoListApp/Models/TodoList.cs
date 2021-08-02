using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TodoListApp.Models
{
    public class TodoList
    {
        public int TodoListId { get; set; }
        public string Todo { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
