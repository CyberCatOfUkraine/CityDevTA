using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWorker.Model
{
    internal class App
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public App(string name)
        {
            Name = name;
        }
    }
}