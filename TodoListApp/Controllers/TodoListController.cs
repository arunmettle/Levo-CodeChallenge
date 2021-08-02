using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TodoListApp.Data;
using TodoListApp.Helphers;
using TodoListApp.Models;

namespace TodoListApp.Controllers
{
    public class TodoListController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly TodoListApi _todoListApi= new TodoListApi();

        public TodoListController(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        // GET: TodoListController
        public async Task<ActionResult> Index()
        {
            string emailIdentityName = _httpContextAccessor.HttpContext.User.Identity.Name;
            User user= await _todoListApi.GetUserByEmail(emailIdentityName);
            List<TodoList> todoList = await _todoListApi.GetTodoListByUser(user);
            if (todoList==null || todoList.Count <= 0)
            {
                todoList = new List<TodoList>();
                todoList.Add(new TodoList()
                {
                    UserId = user.UserId,
                    User = user
                });
            }
            return View(todoList);
        }

        // GET: TodoListController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TodoListController/Create
        public ActionResult Create(string id)
        {
            TodoViewModel todoViewModel = new TodoViewModel()
            {
                UserId = id
            };
            return View(todoViewModel);
        }

        // POST: TodoListController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTodo(TodoViewModel todoList)
        {
            var data = todoList;
            try
            {
                await _todoListApi.CreateTodo(todoList);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // GET: TodoListController/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            string emailIdentityName = _httpContextAccessor.HttpContext.User.Identity.Name;
            User user = await _todoListApi.GetUserByEmail(emailIdentityName);
            TodoList todo = _todoListApi.GetTodoById(id).Result;
            TodoViewModel todoViewModel = new TodoViewModel()
            {
                UserId = id, 
                Todo = todo.Todo, 
                TodoId = todo.TodoListId
            };
            return View(todoViewModel);
        }

        // POST: TodoListController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTodo(TodoViewModel todoList)
        {

            var data = todoList;
            try
            {
                 await _todoListApi.UpdateTodo(todoList);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // GET: TodoListController/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            string emailIdentityName = _httpContextAccessor.HttpContext.User.Identity.Name;
            User user = await _todoListApi.GetUserByEmail(emailIdentityName);
            TodoList todo = _todoListApi.GetTodoById(id).Result;
            TodoViewModel todoViewModel = new TodoViewModel()
            {
                UserId = id,
                Todo = todo.Todo,
                TodoId = todo.TodoListId
            };
            return View(todoViewModel);
        }

        // POST: TodoListController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteTodo(TodoViewModel todoList)
        {
            var data = todoList;
            try
            {
                await _todoListApi.DeleteTodo(todoList);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
    }

    public class TodoViewModel
    {
        [DisplayName("Todo")]
        [Required, MaxLength(80)] 
        public string Todo { get; set; }

        public int TodoId { get; set; }

        public string UserId { get; set; }
    }
}
