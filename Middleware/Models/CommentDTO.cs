using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Middleware.Models
{
    public class CommentDTO
    {
        public CommentDTO(string text, DateTime timeStamp)
        {
            Text = text;
            TimeStamp = timeStamp;
        }

        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}