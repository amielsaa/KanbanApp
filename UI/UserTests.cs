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
            Console.WriteLine("------------------------------------------------test0--------------------------------------------------------");
            Response res0 = userService.Register("rk@post.il", "aA1235");//create new user
            if (res0.ErrorOccured)
            {
                Console.WriteLine(res0.ErrorMessage);
            }
            else
            {
                Console.WriteLine("Created user successfully");
            }


            Console.WriteLine("------------------------------------------------test1------------------------------------------------------");
            Response res1 = userService.Register("guy@gmail.com", "aA1235");//create new user
            if (res1.ErrorOccured)
            {
                Console.WriteLine(res1.ErrorMessage);
            }
            else
            {
                Console.WriteLine("Created user successfully");
            }

            Console.WriteLine("------------------------------------------------test2---------------------------------------------------------");
            Response res2 = userService.Register("guy@gmail", "aB1235@");//should throw exception (user already exist)
            if (res2.ErrorOccured)
            {
                Console.WriteLine(res2.ErrorMessage);
            }
            else
            {
                Console.WriteLine("Created user successfully");
            }


            Console.WriteLine("------------------------------------------------test3-----------------------------------------------------------");
            Response res3= userService.Register(null, "1235");// email cant be null
            if (res3.ErrorOccured)
            {
                Console.WriteLine(res3.ErrorMessage);
            }
            else
            {
                Console.WriteLine("Created user successfully");
            }

            Console.WriteLine("------------------------------------------------test4-----------------------------------------------------------");
            Response res4 = userService.Login("guy@gmail.com", "aA1235");//login successfuly
            if (res4.ErrorOccured)
            {
                Console.WriteLine(res4.ErrorMessage);
            }
            else
            {
                Console.WriteLine("Logged in successfully");
            }

            Console.WriteLine("------------------------------------------------test5-----------------------------------------------------------");
            Response res5 = userService.Login("guy@gmail.com", "aA1235"); //user already logged in 
            if (res5.ErrorOccured)
            {
                Console.WriteLine(res5.ErrorMessage);
            }

            Console.WriteLine("------------------------------------------------test6--------------------------------------------------------------");
            Response res6 = userService.Logout( "guy@gmail.com");//logout successfuly
            if (res6.ErrorOccured)
            {
                Console.WriteLine(res6.ErrorMessage);
            }

            Console.WriteLine("------------------------------------------------test7--------------------------------------------------------------");
            Response res7 = userService.Logout("guy@gmail.com");//user already logged out 
            if (res7.ErrorOccured)
            {
                Console.WriteLine(res7.ErrorMessage);
            }
            else
            {
                Console.WriteLine("Logged in successfully");
            }

            Console.WriteLine("------------------------------------------------test8---------------------------------------------------------------");
            Response res8 = userService.Register("guy@gmail.com", "aA1235"); //reject registration
            if (res8.ErrorOccured)
            {
                Console.WriteLine(res8.ErrorMessage);
            }
            else
            {
                Console.WriteLine("reggister regected sucssesfully");
            }

            Console.WriteLine("------------------------------------------------test9--------------------------------------------------------------");
            Response res9 = userService.Register("guy2@gmail..xo.xo", "aB1235@"); //email not leagal
            if (res9.ErrorOccured)
            {
                Console.WriteLine(res9.ErrorMessage);
            }
            else
            {
                Console.WriteLine("Created user successfully");
            }

            Console.WriteLine("------------------------------------------------test10---------------------------------------------------------------");
            Response res10 = userService.Register("guy2@gmail.!xo.xo", "aB1235@");//email not legal
            if (res10.ErrorOccured)
            {
                Console.WriteLine(res10.ErrorMessage);
            }
            else
            {
                Console.WriteLine("Created user successfully");
            }

            Console.WriteLine("------------------------------------------------test11----------------------------------------------------------------");
            Response res11 = userService.Logout("guy@.com");//email not legal
            if (res11.ErrorOccured)
            {
                Console.WriteLine(res11.ErrorMessage);
            }

            Console.WriteLine("------------------------------------------------test13-----------------------------------------------------------------");
            Response res13 = userService.Register("def@gmail.com", "1234");//password not legal
            if (res13.ErrorOccured)
            {
                Console.WriteLine(res13.ErrorMessage);
            }

            Console.WriteLine("----------------------------------------------test12-----------------------------------------------------------------");

            Response res12 = userService.LoadData();//regualr data loading 
            if (res12.ErrorOccured)
            {
                Console.WriteLine(res12.ErrorMessage);
            }
            else
            {
                Console.WriteLine("loading data sucssesfully");
            }

            Console.WriteLine("----------------------------------------------test14-----------------------------------------------------------------");
            Response res14 = userService.Login("rk@post.il", "Rk1234");//login successfuly
            if (res14.ErrorOccured)
            {
                Console.WriteLine(res14.ErrorMessage);
            }
            else
            {
                Console.WriteLine("Logged in successfully");
            }

            Console.WriteLine("----------------------------------------------test15-----------------------------------------------------------------");
            Response res15 = userService.Login("rk@post.il", "Rk124");//password incorrect
            if (res15.ErrorOccured)
            {
                Console.WriteLine(res15.ErrorMessage);
            }
            else
            {
                Console.WriteLine("Logged in successfully");
            }

            Console.WriteLine("----------------------------------------------test16-----------------------------------------------------------------");
            Response res16 = userService.Logout("rk@post.il");//user unlogged
            if (res16.ErrorOccured)
            {
                Console.WriteLine(res16.ErrorMessage);
            }
            else
            {
                Console.WriteLine("Logged out successfully");
            }

            Console.WriteLine("----------------------------------------------test17-----------------------------------------------------------------");
            Response res17 = userService.Login("rk@post.il", "Rk124");//password incorrect
            if (res17.ErrorOccured)
            {
                Console.WriteLine(res17.ErrorMessage);
            }

            Console.WriteLine("----------------------------------------------test18-----------------------------------------------------------------");

            Response res18 = userService.DeleteData();//regualr data loading 
            if (res18.ErrorOccured)
            {
                Console.WriteLine(res18.ErrorMessage);
            }
            else
            {
                Console.WriteLine("deleting database sucssesfully");
            }








        }

    }
}
