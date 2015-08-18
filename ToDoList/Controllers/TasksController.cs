using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Todo.BL;
using Todo.Model;

namespace ToDoList.Controllers
{
    public class TasksController : Controller
    {
        // GET: Tasks
        public ActionResult AllTasks()
        {
            IEnumerable<TodoTask> tasks = TasksBL.GetTasks();

            return View(tasks);
        }

        public ActionResult CreateTask()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateTask(TodoTask task)
        {
            if (ModelState.IsValid)
            {
                TasksBL.CreateTask(task);
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
    }
}