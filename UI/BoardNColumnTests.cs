using IntroSE.Kanban.Backend.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Frontend
{
    class BoardNColumnTests
    {

        Service userService;
        public BoardNColumnTests(Service userService)
        {
            this.userService = userService;
        }

        public void runTests()
        {
            userService.Register("guy@gmail.com", "Aa123456");
            var res = userService.Login("guy@gmail.com", "Aa123456");
            Response addBoard = userService.AddBoard("guy@gmail.com", "testBoard");
            if (addBoard.ErrorOccured)
            {
                Console.WriteLine(addBoard.ErrorMessage);
            }
            else
            {
                Console.WriteLine("Added board");
            }

            Response removeBoard = userService.RemoveBoard("guy@gmail.com", "testBoard");
            if (removeBoard.ErrorOccured)
            {
                Console.WriteLine(addBoard.ErrorMessage);
            }
            else
            {
                Console.WriteLine("removed board");
            }
            
            Response GetColumnName = userService.GetColumnName("guy@gmail.com", "testBoard", 0);
            if (GetColumnName.ErrorOccured)
            {
                Console.WriteLine(GetColumnName.ErrorMessage);
            }
            else
            {
                Console.WriteLine("GetColumnName DONE");
            }
            
            Response GetColumn = userService.GetColumn("guy@gmail.com", "testBoard", 0);
            if (GetColumn.ErrorOccured)
            {
                Console.WriteLine(GetColumn.ErrorMessage);
            }
            else
            {
                Console.WriteLine("Got column - 0");
            }

            Response addBoard2 = userService.AddBoard("guy@gmail.com", "testBoard2");
            if (addBoard2.ErrorOccured)
            {
                Console.WriteLine(addBoard2.ErrorMessage);
            }
            else
            {
                Console.WriteLine("Added board2");
            }

            Response limitColumn = userService.LimitColumn("guy@gmail.com", "testBoard2", 0, -2);
            if (limitColumn.ErrorOccured)
            {
                Console.WriteLine(limitColumn.ErrorMessage);
            }
            else
            {
                Console.WriteLine("limiting to 10");
            }

            Response getLimit = userService.GetColumnLimit("guy@gmail.com", "testBoard2", 0);
            if (getLimit.ErrorOccured)
            {
                Console.WriteLine(getLimit.ErrorMessage);
            }
            else
            {
                Console.WriteLine("limited");
            }
        }
    }
}
