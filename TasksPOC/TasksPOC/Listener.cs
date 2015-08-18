using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksPOC
{
    /// <summary>
    /// Message listener, in real life, acts as the communication library
    /// </summary>
    public static class Listener
    {
        /// <summary>
        /// Mapping tasks to messages
        /// </summary>
        private static Dictionary<int, Task> _TasksDict = new Dictionary<int, Task>();

        public static async Task<TMessage> StartTask<TMessage>(int id, TMessage message)
        {
            Console.WriteLine("Starting task with Id : {0}", id);
            Task<TMessage> result = new Task<TMessage>(() => SendMesssage(message));

            _TasksDict[id] = result;


            return await result;

        }

        /// <summary>
        /// In real life - handle the message being received from the other side
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="message"></param>
        /// <returns></returns>
        private static TMessage SendMesssage<TMessage>(TMessage message)
        {
            return message;
        }


        /// <summary>
        /// When using in real-life, this function will handle the messages being received
        /// </summary>
        /// <param name="id"></param>
        public static void EndTask(int id)
        {
            var task = _TasksDict[id];
            if (task != null)
            {
                task.Start();
            }
        }
    }
}
