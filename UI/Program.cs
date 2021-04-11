using System;
using IntroSE.Kanban.Backend.ServiceLayer;
using IntroSE.Kanban.Frontend;

namespace UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            UserTests test = new UserTests(new Service());
            test.runTests();
        }

    }
}
