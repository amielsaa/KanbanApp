using System;
using IntroSE.Kanban.Backend.ServiceLayer;
using IntroSE.Kanban.Backend.DataAccessLayer;
using IntroSE.Kanban.Backend.DataAccessLayer.DalObjects;

using IntroSE.Kanban.Frontend;
using System.Collections.Generic;

namespace UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DTask tableTask = new DTask();
            List<TaskDTO> list = tableTask.SelectAllTaskByEmail("ido");
            tableTask.Update("ido", "description", "ddddddd");
            Console.WriteLine(list[0].Description + list[1].Description);
        }

    }
}
