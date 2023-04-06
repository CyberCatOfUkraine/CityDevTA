using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWorker.Model
{
    public class Record
    {
        public Record(App app, User user, Comment comment)
        {
            App = app;
            User = user;
            Comment = comment;
        }

        public int Id { get; set; }
        public App App { get; set; }
        public User User { get; set; }
        public Comment Comment { get; set; }
    }
}