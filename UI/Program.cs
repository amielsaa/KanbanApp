using System;
using IntroSE.Kanban.Backend.ServiceLayer;

namespace UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            testReg();
        }

        public static void testReg()
        {
            UserService userService = new UserService();
            Response res2 = userService.Register("guy@gmail.com", "1235");
            
            if (res2.ErrorOccured)
            {
                Console.WriteLine(res2.ErrorMessage);
            }
            else
            {
                Console.WriteLine("Created user successfully");
            }

            Response res3 = userService.Register("guy@gmail.com", "1235");
            if (res3.ErrorOccured)
            {
                Console.WriteLine(res3.ErrorMessage);
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
        }
    }
}
