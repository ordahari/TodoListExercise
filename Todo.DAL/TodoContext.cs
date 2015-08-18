using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Todo.Model;

namespace Todo.DAL
{
    internal class TodoContext : DbContext
    {
        public TodoContext():base("TodoConectionString")
        {

        }
        public DbSet<TodoTask> Tasks { get; set; }
    }
}
