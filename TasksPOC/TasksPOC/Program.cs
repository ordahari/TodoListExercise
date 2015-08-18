using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TasksPOC
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();

            Task.Run(() =>
            {
                while (true)
                {
                    int id = random.Next();
                    var returnedMessage = GetMessage(id);

                
                    returnedMessage.ContinueWith((finishedTask) => Console.WriteLine("Finished Task {0}", finishedTask.Result));
                    Thread.Sleep(100);
                    Task.Run(() =>
                    {
                        Thread.Sleep(100);
                        Listener.EndTask(id);
                    });
                }
            });

            Console.WriteLine("Presss <Enter> to quit");
            Console.ReadLine();
        }

        private static async Task<int> GetMessage(int id)
        {
            var returnedMessage = await Listener.StartTask(id, id);
            return returnedMessage;


        }
    }
}
