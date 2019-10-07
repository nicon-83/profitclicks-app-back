using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyWebApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MyWebApi.Controllers
{
    [Route("api/[controller]")]
    public class PhoneController : Controller
    {
        private readonly IConfiguration configuration;

        public PhoneController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        private string GetConnString()
        {
            return configuration.GetConnectionString("MyWebApiConnection");
        }

        // POST api/phone
        [HttpPost]
        public HttpResponseMessage AddPhone([FromBody]JObject data)
        {
            Phone phone;
            User user;
            Phone oldPhone;

            if (data == null)
                return new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = "POST body is null" };

            try
            {
                phone = data.ToObject<Phone>();

                using (var db = new MyWebApiContext(GetConnString()))
                {
                    user = db.Users.Where(x => x.Id == phone.UserId).FirstOrDefault();
                    if(User == null) throw new Exception($"Не найден контакт с id {phone.UserId}");
                    oldPhone = db.Phones.Where(x => x.UserId == phone.UserId && x.Number == phone.Number).FirstOrDefault();
                    if (oldPhone != null) throw new Exception($"Контакт с id {phone.UserId} уже имеет номер {phone.Number}");
                    db.Phones.Add(phone);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = e.Message.Replace(Environment.NewLine, "") };
            }

            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, ReasonPhrase = $"Number {phone.Number} added successfully to Contact with id {phone.UserId}" };
        }

        // DELETE api/phone/5
        [HttpDelete("{id}")]
        public HttpResponseMessage DeletePhone(int id)
        {
            Phone phone;

            try
            {
                using (var db = new MyWebApiContext(GetConnString()))
                {
                    phone = db.Phones.Where(x => x.Id == id).FirstOrDefault();
                    if (phone == null) throw new Exception($"Не найден номер телефона с id {id}");
                    db.Phones.Remove(phone);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = e.Message.Replace(Environment.NewLine, "") };
            }

            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, ReasonPhrase = $"Phone with {id} deleted successfully" };
        }
    }
}
