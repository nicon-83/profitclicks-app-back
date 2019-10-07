using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyWebApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MyWebApi.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IConfiguration configuration;

        public UserController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        private string GetConnString()
        {
            return configuration.GetConnectionString("MyWebApiConnection");
        }

        // GET api/user
        [HttpGet]
        public IActionResult GetUsers()
        {
            List<User> users;

            try
            {
                using (var db = new MyWebApiContext(GetConnString()))
                {
                    users = db.Users.Include(x => x.Phones).ToList();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Json(users, new JsonSerializerSettings
            {
                Formatting = Formatting.None,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.None,
                TypeNameHandling = TypeNameHandling.None,
            });
        }

        // GET api/user/5
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            User user;

            try
            {
                using (var db = new MyWebApiContext(GetConnString()))
                {
                    user = db.Users.Where(x => x.Id == id).Include(x => x.Phones).FirstOrDefault();
                    if (user == null) throw new Exception($"Не найден контакт с id {id}");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Json(user, new JsonSerializerSettings
            {
                Formatting = Formatting.None,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.None,
                TypeNameHandling = TypeNameHandling.None,
            });
        }

        // POST api/user
        [HttpPost]
        public HttpResponseMessage AddUser([FromBody]JObject data)
        {
            User user;
            User oldUser;

            if (data == null)
                return new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = "POST body is null" };

            try
            {
                user = data.ToObject<User>();

                using (var db = new MyWebApiContext(GetConnString()))
                {
                    oldUser = db.Users.Where(x => x.FirstName == user.FirstName && x.LastName == user.LastName && x.MiddleName == user.MiddleName).FirstOrDefault();
                    if (oldUser != null) throw new Exception($"Контакт {user.LastName} {user.FirstName} {user.MiddleName} уже существует");
                    db.Users.Add(user);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = e.Message.Replace(Environment.NewLine, "") };
            }

            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, ReasonPhrase = $"User {JsonConvert.SerializeObject(user)} added successfully" };
        }

        // PUT api/user/5
        [HttpPut("{id}")]
        public HttpResponseMessage UpdateUser(int id, [FromBody]JObject data)
        {
            User oldUser;
            User newUser;

            if (data == null)
                return new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = "POST body is null" };

            try
            {
                newUser = data.ToObject<User>();

                using (var db = new MyWebApiContext(GetConnString()))
                {
                    oldUser = db.Users.Where(x => x.Id == id).FirstOrDefault();
                    if (oldUser == null) throw new Exception($"Не найден контакт с id {id}");
                    oldUser.FirstName = newUser.FirstName;
                    oldUser.LastName = newUser.LastName;
                    oldUser.MiddleName = newUser.MiddleName;
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = e.Message.Replace(Environment.NewLine, "") };
            }

            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, ReasonPhrase = $"User {JsonConvert.SerializeObject(oldUser)} updated successfully" };
        }

        // DELETE api/user/5
        [HttpDelete("{id}")]
        public HttpResponseMessage DeleteUser(int id)
        {
            User user;

            try
            {
                using (var db = new MyWebApiContext(GetConnString()))
                {
                    user = db.Users.Where(x => x.Id == id).FirstOrDefault();
                    if (user == null) throw new Exception($"Не найден контакт с id {id}");
                    db.Users.Remove(user);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = e.Message.Replace(Environment.NewLine, "") };
            }

            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, ReasonPhrase = $"User {JsonConvert.SerializeObject(user)} deleted successfully" };
        }
    }
}
