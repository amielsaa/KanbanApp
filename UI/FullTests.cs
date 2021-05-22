using IntroSE.Kanban.Backend.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Frontend
{
    class FullTests
    {
        private readonly Service userService;


        public FullTests(Service userService)
        {
            this.userService = userService;
        }
        public void RunTests()
        {
            Response deleteData = userService.DeleteData();
            if (deleteData.ErrorOccured)
            {
                Console.WriteLine(deleteData.ErrorMessage);
            }
            else
            {
                Console.WriteLine("###Deleted data succes###");
            }

            ///***USER TESTS***///
            Console.WriteLine("\n///***USER TESTS***///");
            Response res3 = userService.Register("guy@gmail.com", "aA1235");
            if (res3.ErrorOccured)
            {
                Console.WriteLine(res3.ErrorMessage);
            }
            else
            {
                Console.WriteLine("###Created user successfully###");
            }
            /*Response res4 = userService.ChangePassword("guy@gmail.com", "aA12356");
            if (res4.ErrorOccured)
            {
                Console.WriteLine(res4.ErrorMessage);
            }
            else
            {
                Console.WriteLine("###Created user successfully###");
            }
            Response res5 = userService.ChangePassword("guy@gmail.com", "aA123567");
            if (res5.ErrorOccured)
            {
                Console.WriteLine(res5.ErrorMessage);
            }
            else
            {
                Console.WriteLine("###Created user successfully###");
            }*/
            Response res7 = userService.Register("oran@gmail.com", "aaAa1234");
            if (res7.ErrorOccured)
            {
                Console.WriteLine(res7.ErrorMessage);
            }
            else
            {
                Console.WriteLine("###Created user successfully###");
            }
            /*Response res10 = userService.ChangePassword("oran@gmail.com", "aaAa12345");
            if (res10.ErrorOccured)
            {
                Console.WriteLine(res10.ErrorMessage);
            }
            else
            {
                Console.WriteLine("###changed password successfully###");
            }*/
            Response res77 = userService.Login("guy@gmail.com", "aA123567");
            if (res77.ErrorOccured)
            {
                Console.WriteLine(res77.ErrorMessage);
            }
            else
            {
                Console.WriteLine("###login successfully###");
            }
            Response res88 = userService.Login("oran@gmail.com", "aaAa12345");
            if (res88.ErrorOccured)
            {
                Console.WriteLine(res88.ErrorMessage);
            }
            else
            {
                Console.WriteLine("###login successfully###");
            }
            Response res89 = userService.Logout("oran@gmail.com");
            if (res89.ErrorOccured)
            {
                Console.WriteLine(res89.ErrorMessage);
            }
            else
            {
                Console.WriteLine("###logout successfully###");
            }
            Response res888 = userService.Login("oran@gmail.com", "aaAa12345");
            if (res888.ErrorOccured)
            {
                Console.WriteLine(res888.ErrorMessage);
            }
            else
            {
                Console.WriteLine("###login successfully - After logout###");
            }
            Response register = userService.Register("logoutuser@gmail.com", "aaAa958");
            if (register.ErrorOccured)
            {
                Console.WriteLine(register.ErrorMessage);
            }
            else
            {
                Console.WriteLine("###Register successfully###");
            }
            Response res90 = userService.Login("logoutuser@gmail.com", "aaAa958");
            if (res90.ErrorOccured)
            {
                Console.WriteLine(res90.ErrorMessage);
            }
            else
            {
                Console.WriteLine("###login successfully###");
            }
            Response res91 = userService.Logout("logoutuser@gmail.com");
            if (res91.ErrorOccured)
            {
                Console.WriteLine(res91.ErrorMessage);
            }
            else
            {
                Console.WriteLine("###logout successfully###");
            }


            ///***BOARD TESTS***///


            Console.WriteLine("\n///***BOARD TESTS***///");
            Response res6 = userService.AddBoard("guy@gmail.com", "newBoard0");
            if (res6.ErrorOccured)
            {
                Console.WriteLine(res6.ErrorMessage);
            }
            else
            {
                Console.WriteLine("###Created board successfully###");
            }

            Response res55 = userService.AddBoard("oran@gmail.com", "newBoard1");
            if (res55.ErrorOccured)
            {
                Console.WriteLine(res55.ErrorMessage);
            }
            else
            {
                Console.WriteLine("###Created board successfully###");
            }

            Response res56 = userService.AddBoard("oran@gmail.com", "newBoard2");
            if (res56.ErrorOccured)
            {
                Console.WriteLine(res56.ErrorMessage);
            }
            else
            {
                Console.WriteLine("###Created board successfully###");
            }
      
            Response res500 = userService.RemoveBoard("oran@gmail.com", "oran@gmail.com", "newBoard2");
            if (res500.ErrorOccured)
            {
                Console.WriteLine(res500.ErrorMessage);
            }
            else
            {
                Console.WriteLine("###board deleted successfully###");
            }


            Response res57 = userService.AddBoard("oran@gmail.com", "newBoard3");
            if (res57.ErrorOccured)
            {
                Console.WriteLine(res57.ErrorMessage);
            }
            else
            {
                Console.WriteLine("###Created board successfully###");
            }

            Response res58 = userService.AddBoard("guy@gmail.com", "newBoard4");
            if (res58.ErrorOccured)
            {
                Console.WriteLine(res58.ErrorMessage);
            }
            else
            {
                Console.WriteLine("###Created board successfully###");
            }


            Response res59 = userService.AddBoard("guy@gmail.com", "newBoard4");
            if (res59.ErrorOccured)
            {
                Console.WriteLine(res59.ErrorMessage);
            }
            else
            {
                Console.WriteLine("###Created board successfully###");
            }
            Response limitcolum0 = userService.LimitColumn("oran@gmail.com", "oran@gmail.com", "newBoard1", 0, 10);
            if (limitcolum0.ErrorOccured)
            {
                Console.WriteLine(limitcolum0.ErrorMessage);
            }
            else
            {
                Console.WriteLine("###Limit column 0 succes###");
            }
            Response limitcolum1 = userService.LimitColumn("oran@gmail.com", "oran@gmail.com", "newBoard1", 1, 5);
            if (limitcolum1.ErrorOccured)
            {
                Console.WriteLine(limitcolum1.ErrorMessage);
            }
            else
            {
                Console.WriteLine("###Limit column 1 succes###");
            }
            Response res8 = userService.JoinBoard("oran@gmail.com", "guy@gmail.com", "newBoard0");
            if (res8.ErrorOccured)
            {
                Console.WriteLine(res8.ErrorMessage);
            }
            else
            {
                Console.WriteLine("###Joined board successfully###");
            }

            Response res9 = userService.JoinBoard("guy@gmail.com", "oran@gmail.com", "newBoard1");
            if (res9.ErrorOccured)
            {
                Console.WriteLine(res9.ErrorMessage);
            }
            else
            {
                Console.WriteLine("###Joined board successfully###");
            }

            Response limitcolum2 = userService.LimitColumn("guy@gmail.com", "oran@gmail.com", "newBoard1", 2, 5);
            if (limitcolum2.ErrorOccured)
            {
                Console.WriteLine(limitcolum2.ErrorMessage);
            }
            else
            {
                Console.WriteLine("###Limit column 2 succes after join board###");
            }
            var getColumnLimit1 = userService.GetColumnLimit("oran@gmail.com", "guy@gmail.com", "newBoard0", 2);
            if (getColumnLimit1.ErrorOccured)
            {
                Console.WriteLine(getColumnLimit1.ErrorMessage);
            }
            else
            {
                Console.WriteLine($"###Get column limit - should return 5, returned: {getColumnLimit1.Value} ###");
            }
            var getColumnName = userService.GetColumnName("oran@gmail.com", "guy@gmail.com", "newBoard0", 2);
            if (getColumnName.ErrorOccured)
            {
                Console.WriteLine(getColumnName.ErrorMessage);
            }
            else
            {
                Console.WriteLine($"###Get column name - should return done, returned: {getColumnName.Value} ###");
            }
            Response res22 = userService.AddTask("guy@gmail.com", "guy@gmail.com", "newBoard0", "new task1", "ddd", DateTime.Today.AddDays(1));
            if (res22.ErrorOccured)
            {
                Console.WriteLine(res22.ErrorMessage);
            }
            else
            {
                Console.WriteLine("###Created task successfully###");
            }
            Response res23 = userService.AddTask("oran@gmail.com", "guy@gmail.com", "newBoard0", "new task2", "ddd2", DateTime.Today.AddDays(2));
            if (res23.ErrorOccured)
            {
                Console.WriteLine(res23.ErrorMessage);
            }
            else
            {
                Console.WriteLine("###Created task successfully###");
            }
            Response res37 = userService.AddTask("oran@gmail.com", "oran@gmail.com", "newBoard1", "new task3", "4ddd2", DateTime.Today.AddDays(2));
            if (res37.ErrorOccured)
            {
                Console.WriteLine(res37.ErrorMessage);
            }
            else
            {
                Console.WriteLine("###Created task successfully###");
            }
            Response updateDue1 = userService.UpdateTaskDueDate("oran@gmail.com", "guy@gmail.com", "newBoard0", 0, 0, DateTime.Today.AddDays(4));
            if (updateDue1.ErrorOccured)
            {
                Console.WriteLine(updateDue1.ErrorMessage);
            }
            else
            {
                Console.WriteLine("###Updated task due date successfully###");
            }

            Response advTask = userService.AdvanceTask("oran@gmail.com", "guy@gmail.com", "newBoard0", 0, 1);
            if (advTask.ErrorOccured)
            {
                Console.WriteLine(advTask.ErrorMessage);
            }
            else
            {
                Console.WriteLine("###Advanced task successfuly###");
            }

            var getcol = userService.GetColumn("oran@gmail.com", "guy@gmail.com", "newBoard0", 0);
            if (getcol.ErrorOccured)
            {
                Console.WriteLine(getcol.ErrorMessage);
            }
            else
            {
                Console.WriteLine("###Got the column successfully###");
            }

            Response rmvbrd = userService.RemoveBoard("oran@gmail.com", "guy@gmail.com", "newBoard0");
            if (rmvbrd.ErrorOccured)
            {
                Console.WriteLine(rmvbrd.ErrorMessage);
            }
            else
            {
                Console.WriteLine("###Removed newBoard2 successfully###");
            }

            var inprog = userService.InProgressTasks("oran@gmail.com");
            if (inprog.ErrorOccured)
            {
                Console.WriteLine(inprog.ErrorMessage);
            }
            else
            {
                Response<IList<Backend.ServiceLayer.Task>> inprogress = userService.InProgressTasks("oran@gmail.com");
                IList<Backend.ServiceLayer.Task> i = inprogress.Value;
                foreach (Backend.ServiceLayer.Task task in i)
                {
                    Console.WriteLine(task.Id + " is part of the in progress of the user oran@gmail.com");
                }
            }

            Response asgntsk = userService.AssignTask("oran@gmail.com", "oran@gmail.com", "newBoard1", 0, 0, "guy@gmail.com");
            if (asgntsk.ErrorOccured)
            {
                Console.WriteLine(asgntsk.ErrorMessage);
            }
            else
            {
                Console.WriteLine("###Assigned task successfully###");
            }

            var brdnms = userService.GetBoardNames("oran@gmail.com");
            if (brdnms.ErrorOccured)
            {
                Console.WriteLine(brdnms.ErrorMessage);
            }
            else
            {
                Response<IList<string>> brdnames = userService.GetBoardNames("oran@gmail.com");
                IList<string> i = brdnames.Value;
                foreach (string name in i)
                {
                    Console.WriteLine(name + " is part of the boards of the user oran@gmail.com");
                }
            }

            var brdnms2 = userService.GetBoardNames("guy@gmail.com");
            if (brdnms2.ErrorOccured)
            {
                Console.WriteLine(brdnms2.ErrorMessage);
            }
            else
            {
                Response<IList<string>> brdnames = userService.GetBoardNames("guy@gmail.com");
                IList<string> i = brdnames.Value;
                foreach (string name in i)
                {
                    Console.WriteLine(name + " is part of the boards of the user guy@gmail.com");
                }
            }
            userService.DeleteData();

        }
    }
}
