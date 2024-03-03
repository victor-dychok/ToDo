using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public User() { }
        public User(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
