using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Middleware.Models
{
    public class RecordDTO
    {
        public RecordDTO(AppDTO app, UserDTO user, CommentDTO comment)
        {
            App = app;
            User = user;
            Comment = comment;
        }

        public int Id { get; set; }
        public AppDTO App { get; set; }
        public UserDTO User { get; set; }
        public CommentDTO Comment { get; set; }
    }
}