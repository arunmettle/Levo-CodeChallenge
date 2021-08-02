using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TodoListApi.Models;


namespace TodoListApi.Controllers
{
    [Route("api/[controller]")]
    public class TodoListController : ControllerBase
    {
        public readonly TodoListDbContext _todoListDbContext;
        public TodoListController(TodoListDbContext todoListDbContext)
        {
            _todoListDbContext = todoListDbContext;
        }

        [HttpGet("TodoList")]
        public async Task<ActionResult<IEnumerable<TodoList>>> GetTodoList()
        {

            return await _todoListDbContext.TodoLists.ToListAsync();
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<TodoList>> GetTodoListById(int id)
        {
            var todoList = await _todoListDbContext.TodoLists.SingleOrDefaultAsync(x => x.TodoListId == id);
            if(todoList!=null)
            {
                return Ok(JsonConvert.SerializeObject(todoList));
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("user/email/{email}")]
        public ActionResult GetTodoListByEmail(string email)
        {
            var todoList = _todoListDbContext.TodoLists.Where(x => x.User.Email == email).ToList();
            if (!(todoList is null) && todoList.Count > 0)
            {
                var userDb = _todoListDbContext.Users.SingleOrDefaultAsync(x => x.UserId == todoList[0].UserId);
                List<TodoList> todoListWithUser = new List<TodoList>();
                foreach (TodoList tdList in todoList)
                {

                    tdList.User = new User()
                    {
                        UserId = userDb.Result.UserId,
                        UserName = userDb.Result.UserName,
                        Email = userDb.Result.Email
                    };
                    todoListWithUser.Add(tdList);
                }

                
                return Ok(JsonConvert.SerializeObject(todoListWithUser));
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPost("AddTodo")]
        [Consumes("application/json")]
        public async Task<ActionResult<TodoList>> AddTodo([FromBody]TodoList todo)
        {
             var userDb = await _todoListDbContext.Users.SingleOrDefaultAsync(x => x.UserId == todo.UserId);
            todo.User = userDb;
            _todoListDbContext.TodoLists.Add(todo);
            await _todoListDbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTodoList), new { id = todo.TodoListId }, todo);

        }

        [HttpPost("UpdateTodo")]
        [Consumes("application/json")]
        public async Task<ActionResult<TodoList>> UpdateTodo([FromBody] TodoList todo)
        {
            var result = await _todoListDbContext.TodoLists.SingleOrDefaultAsync(x => x.TodoListId == todo.TodoListId);
            var userDb = await _todoListDbContext.Users.SingleOrDefaultAsync(x => x.UserId == result.UserId);
            todo.User = userDb;
            if (result != null)
            {
                result.Todo = todo.Todo;
                await _todoListDbContext.SaveChangesAsync();
            }
            return CreatedAtAction(nameof(GetTodoList), new { id = todo.TodoListId }, todo);

        }

        [HttpPost("DeleteTodo")]
        [Consumes("application/json")]
        public async Task<ActionResult<bool>> DeleteTodo([FromBody] TodoList todo)
        {
            TodoList result =  _todoListDbContext.TodoLists.Where(x => x.TodoListId == todo.TodoListId).Single<TodoList>();
            var userDb = await _todoListDbContext.Users.SingleOrDefaultAsync(x => x.UserId == result.UserId);
           
            //todo.User = userDb;
            if (result != null)
            {
                //result.Todo = todo.Todo;
                _todoListDbContext.TodoLists.Remove(result);
                await _todoListDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
