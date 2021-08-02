using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using TodoListApp.Controllers;
using TodoListApp.Models;

namespace TodoListApp.Helphers
{
    public class TodoListApi
    {
        public HttpClient Initial()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:45846/api");
            return client;
        }

        public async Task<HttpResponseMessage> CreateUser(string userId, string name, string email)
        {
            HttpClient client = Initial();
            User user = new User()
            {
                UserId = userId,
                UserName = name, 
                Email = email
            };
            var data = new StringContent(content: JsonConvert.SerializeObject(user),
                encoding: Encoding.UTF8,
                mediaType: "application/json");
            var result= await client.PostAsync("/api/User/AddUser", data);
            if (result.IsSuccessStatusCode)
            {
                return result;
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
        }

        public async Task<User> GetUserByEmail(string email)
        {
            HttpClient client = Initial();
            var result = await client.GetAsync(string.Format("/api/User/email/{0}", email));
            User user = new User();
            if (result.IsSuccessStatusCode)
            {
                var data = result.Content.ReadAsStringAsync().Result;
                user= JsonConvert.DeserializeObject<User>(data);
                return user;
            }
            else
            {
                return user;
            }
        }

        public async Task<User> GetUserByUserId(string id)
        {
            HttpClient client = Initial();
            var result = await client.GetAsync(string.Format("/api/User/id/{0}", id));
            User user = new User();
            if (result.IsSuccessStatusCode)
            {
                var data = result.Content.ReadAsStringAsync().Result;
                user = JsonConvert.DeserializeObject<User>(data);
                return user;
            }
            else
            {
                return user;
            }
        }

        public async Task<List<TodoList>> GetTodoListByUser(User user)
        {
            HttpClient client = Initial();
            List<TodoList> todoList = new List<TodoList>();
            var result = await client.GetAsync(string.Format("{0}/{1}", "/api/TodoList/user/email", user.Email));
            if (result.IsSuccessStatusCode)
            {
                var stringfiedData= result.Content.ReadAsStringAsync().Result;
                todoList = JsonConvert.DeserializeObject<List<TodoList>>(stringfiedData);
            }

            return todoList;
        }

        public async Task<TodoList> GetTodoById(string id)
        {
            HttpClient client = Initial();
            TodoList todo = null;
            var result = await client.GetAsync(string.Format("{0}/{1}", "/api/TodoList/id", id));
            if (result.IsSuccessStatusCode)
            {
                var stringfiedData = result.Content.ReadAsStringAsync().Result;
                todo = JsonConvert.DeserializeObject<TodoList>(stringfiedData);
            }

            return todo;
        }

        public async Task<List<TodoList>> GetTodoList()
        {
            HttpClient client = Initial();
            List<TodoList> todo = null;
            var result = await client.GetAsync( "/api/TodoList/TodoList");
            if (result.IsSuccessStatusCode)
            {
                var stringfiedData = result.Content.ReadAsStringAsync().Result;
                todo = JsonConvert.DeserializeObject<List<TodoList>>(stringfiedData);
            }
            return todo;
        }

        public async Task<TodoList> CreateTodo(TodoViewModel todoListViewModel)
        {
            HttpClient client = Initial();
            TodoList todoList = new TodoList()
            {
                Todo = todoListViewModel.Todo,
                UserId = todoListViewModel.UserId
            };
            var data = new StringContent(JsonConvert.SerializeObject(todoList),
                encoding: Encoding.UTF8,
                mediaType: "application/json");
            var result = await client.PostAsync("/api/TodoList/AddTodo", data);
            if (result.IsSuccessStatusCode)
            {
                var responseData = result.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<TodoList>(responseData);
            }
            else
            {
                return new TodoList();
            }
        }

        public async Task<TodoList> UpdateTodo(TodoViewModel todoListViewModel)
        {
            HttpClient client = Initial();
            TodoList todoList = new TodoList()
            {
                Todo = todoListViewModel.Todo,
                TodoListId = todoListViewModel.TodoId
            };
            var data = new StringContent(JsonConvert.SerializeObject(todoList),
                encoding: Encoding.UTF8,
                mediaType: "application/json");
            var result = await client.PostAsync("/api/TodoList/UpdateTodo", data);
            if (result.IsSuccessStatusCode)
            {
                var responseData = result.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<TodoList>(responseData);
            }
            else
            {
                return new TodoList();
            }
        }


        public async Task<TodoList> DeleteTodo(TodoViewModel todoListViewModel)
        {
            HttpClient client = Initial();
            TodoList todoList = new TodoList()
            {
                Todo = todoListViewModel.Todo,
                TodoListId = todoListViewModel.TodoId
            };
            var data = new StringContent(JsonConvert.SerializeObject(todoList),
                encoding: Encoding.UTF8,
                mediaType: "application/json");
            var result = await client.PostAsync("/api/TodoList/DeleteTodo", data);
            if (result.IsSuccessStatusCode)
            {
                var responseData = result.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<TodoList>(responseData);
            }
            else
            {
                return new TodoList();
            }
        }


    }
}
