using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TodoListApi.Models;

namespace TodoListApi.Controllers
{
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        public readonly TodoListDbContext _todoListDbContext;

        public UserController(TodoListDbContext todoListDbContext)
        {
            _todoListDbContext = todoListDbContext;
        }

        [HttpGet("GetUsers")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _todoListDbContext.Users.ToListAsync();
        }

        [HttpGet("id/{id}")]
        public ActionResult GetUser(string id)
        {
            var user = _todoListDbContext.Users.Where(x => x.UserId == id).FirstOrDefault();
            if (user != null && user is User)
            {
                return Ok(JsonConvert.SerializeObject(user));
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("email/{email}")]
        public ActionResult GetUserByEmail(string email)
        {
            var user = _todoListDbContext.Users.Where(x => x.Email == email).FirstOrDefault();
            if (user != null && user is User)
            {
                return Ok(JsonConvert.SerializeObject(user));
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPost("AddUser")]
        [Consumes("application/json")]
        public async Task<ActionResult<User>> AddUser([FromBody]User user)
        {
            _todoListDbContext.Users.Add(user);
            await _todoListDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new {id = user.UserId}, user);
        }

    }
}
