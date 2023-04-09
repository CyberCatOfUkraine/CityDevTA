using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Middleware.Models
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public UserDTO(string name)
        {
            Name = name;
        }
    }
}