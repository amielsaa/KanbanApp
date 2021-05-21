using IntroSE.Kanban.Backend.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Frontend
{
    class TaskTests
    {
        Service userService;
        public TaskTests(Service userService)
        {
            this.userService = userService;
        }

        /*
        public void runTests()
        {
            userService.Register("guy@gmail.com", "Aa123456");
            var res = userService.Login("guy@gmail.com", "Aa123456");
            userService.AddBoard("guy@gmail.com", "TestBoard");
            userService.AddBoard("guy@gmail.com", "AnotherBoard");
            var task1 = userService.AddTask("guy@gmail.com", "TestBoard", "MA", "SOMEDESC", DateTime.Now);
            var task2 = userService.AddTask("guy@gmail.com", "TestBoard", "HI", "SOMEDESC", DateTime.Now);
            var task3 = userService.AddTask("guy@gmail.com", "AnotherBoard", "SHIT", "SOMEDESC", DateTime.Now);
            if (task1.ErrorOccured | task2.ErrorOccured | task3.ErrorOccured)
                Console.WriteLine("Failed to create tasks");
            else
                Console.WriteLine("All tasks created successfully");

            var adv1 = userService.AdvanceTask("guy@gmail.com", "TestBoard", 0, task1.Value.Id);
            var adv2 = userService.AdvanceTask("guy@gmail.com", "TestBoard", 0, task2.Value.Id);

            if (adv1.ErrorOccured | adv2.ErrorOccured)
                Console.WriteLine("Failed to advanced one or more tasks");
            else
                Console.WriteLine("Tasks advanced successfully");

            var t1 = userService.InProgressTasks("guy@gmail.com");
            if (t1.ErrorOccured)
            {
                Console.WriteLine("FAILED");
            }
            else
                Console.WriteLine("Returned: " + t1.Value.Count + "(suppose to be 2)");

            var adv3 = userService.AdvanceTask("guy@gmail.com", "TestBoard", 1, task1.Value.Id);
            if (adv3.ErrorOccured)
                Console.WriteLine("Failed to advance task1 to InProgress");
            else
                Console.WriteLine("task1 advanced to Done successfully");

            

            var adv4 = userService.AdvanceTask("guy@gmail.com", "TestBoard", 2, task1.Value.Id);
            if (adv4.ErrorOccured)
                Console.WriteLine("GOOD GOOD");
            else
                Console.WriteLine("task1 advanced to illegal column (yani failed)");

            
            

        }
        */
    }
}
            /*
            /*
            userService.AdvanceTask("guy@gmail.com", "TestBoard", 0, task.Value.Id);
            userService.AdvanceTask("guy@gmail.com", "TestBoard", 1, task.Value.Id);

            Response t1 = userService.UpdateTaskDescription("guy@gmail.com", "TestBoard", 2, task.Value.Id, "SOMETHING");
            if (t1.ErrorOccured)
                Console.WriteLine("NICE: " + t1.ErrorMessage);
            else
                Console.WriteLine("FAILED: DESC length");
            */





            /*
            //exceeding DESC length
            Response t1 = userService.UpdateTaskDescription("guy@gmail.com", "TestBoard", 0, task.Value.Id, bigString(400));
            if (t1.ErrorOccured)
                Console.WriteLine("NICE");
            else
                Console.WriteLine("FAILED: DESC length");
            //null DESC
            Response t2 = userService.UpdateTaskDescription("guy@gmail.com", "TestBoard", 0, task.Value.Id, null);
            if (t2.ErrorOccured)
                Console.WriteLine("NICE");
            else
                Console.WriteLine("FAILED: DESC null");
            //exceeding TITLE length
            Response t3 = userService.UpdateTaskTitle("guy@gmail.com", "TestBoard", 0, task.Value.Id,bigString(60));
            if (t3.ErrorOccured)
                Console.WriteLine("NICE");
            else
                Console.WriteLine("FAILED: exceeding");
            //0 TITLE length
            Response t4 = userService.UpdateTaskTitle("guy@gmail.com", "TestBoard", 0, task.Value.Id, "");
            if (t4.ErrorOccured)
                Console.WriteLine("NICE");
            else
                Console.WriteLine("FAILED: 0");
            //null TITLE 
            Response t5 = userService.UpdateTaskTitle("guy@gmail.com", "TestBoard", 0, task.Value.Id, null);
            if (t5.ErrorOccured)
                Console.WriteLine("NICE");
            else
                Console.WriteLine("FAILED: null");
            //null DUEDATE 
            Response t6 = userService.UpdateTaskDueDate("guy@gmail.com", "TestBoard", 0, task.Value.Id,DateTime.Today);
            if (t6.ErrorOccured)
                Console.WriteLine("NICE");
            else
                Console.WriteLine("FAILED: min value");

            *//*
        }

        public string bigString(int length)
        {
            string s = "";
            for (int i = 0; i < length; i++)
                s = s + "a";
            return s;
        }
    }
}
*/