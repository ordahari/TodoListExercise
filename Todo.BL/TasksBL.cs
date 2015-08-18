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
            var tasks = TasksDAL.GetTasks();

            foreach (var item in tasks)
            {
                if (item.Status == default(TodoTaskStatus))
                {
                    item.Status = TodoTaskStatus.New;
                }
            }

            return tasks;
        }

        public static void CreateTask(TodoTask task)
        {
            task.CreatedDate = DateTime.Now;
            task.Status = TodoTaskStatus.New;
            TasksDAL.CreateTask(task);
        }
    }
}
