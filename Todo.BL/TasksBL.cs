using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.DAL;
using Todo.Model;

namespace Todo.BL
{
    public class TasksBL
    {
        public static IEnumerable<TodoTask> GetTasks()
        {
            return TasksDAL.GetTasks();
        }
    }
}
