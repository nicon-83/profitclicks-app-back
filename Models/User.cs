
using System.Collections.Generic;

namespace MyWebApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }

        public List<Phone> Phones { get; set; }
    }
}