using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Model;

namespace Todo.DAL
{
    public class TasksDAL
    {
        public static IEnumerable<TodoTask> GetTasks()
        {
            using (TodoContext ctx = new TodoContext())
            {
                return ctx.Tasks.ToList();
            }
        }

        public static void CreateTask(TodoTask task)
        {
            using (TodoContext ctx = new TodoContext())
            {
                ctx.Tasks.Add(task);
                ctx.SaveChanges();
            }
        }
    }
}
