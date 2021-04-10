using IntroSE.Kanban.Backend.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Frontend
{
    class UserTests

    {
        private Service userService;
        public UserTests(Service userService)
        {
            this.userService = userService;
        }
        public void runTests()
        {
            Response res3 = userService.Register("guy@gmail.com", "aA1235");
            if (res3.ErrorOccured)
            {
                Console.WriteLine(res3.ErrorMessage);
            }
            else
            {
                Console.WriteLine("Created user successfully");
            }
            Response res6 = userService.Register("guy2@gmail.c", "aB1235@");
            if (res6.ErrorOccured)
            {
                Console.WriteLine(res6.ErrorMessage);
            }
            else
            {
                Console.WriteLine("Created user successfully");
            }

            Response res4 = userService.Register(null, "1235");
            if (res4.ErrorOccured)
            {
                Console.WriteLine(res4.ErrorMessage);
            }
            else
            {
                Console.WriteLine("Created user successfully");
            }

            Response res5 = userService.Login("guy@gmail.com", "aA1235");
            if (res5.ErrorOccured)
            {
                Console.WriteLine(res5.ErrorMessage);
            }
            else
            {
                Console.WriteLine("Logged in successfully");
            }
        }

    }
}
