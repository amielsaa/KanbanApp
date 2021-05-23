using System;
//using IntroSE.Kanban.Backend.ServiceLayer;
using IntroSE.Kanban.Backend.DataAccessLayer;
using introSE.KanbanBoard.Backend.BuisnessLayer;
using IntroSE.Kanban.Backend.DataAccessLayer.DalObjects;
using System.Collections.Generic;

using IntroSE.Kanban.Frontend;
using System.Collections.Generic;
using IntroSE.Kanban.Backend.ServiceLayer;

namespace UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /*
            Service service = new Service();
            FullTests tests = new FullTests(service);
            tests.RunTests();
            */
            
            Service service = new Service();
            service.LoadData();
            service.Login("amiel@gmail.com", "123456Kk");
            service.RemoveBoard("amiel@gmail.com", "amiel@gmail.com", "boardzbzxcbzbz");
            
            /*
            Service service = new Service();
            service.DeleteData();
            service.Register("amiel@gmail.com", "123456Kk");
            service.Register("amielhagever@gmail.com", "123456Kk");
            service.Login("amiel@gmail.com", "123456Kk");
            service.Login("amielhagever@gmail.com", "123456Kk");

            service.AddBoard("amiel@gmail.com", "boardz");
            service.AddBoard("amiel@gmail.com", "boardbzz");
            service.AddBoard("amiel@gmail.com", "boardzbzxcbzbz");
            var res4 = service.GetBoardNames("amiel@gmail.com");
            Console.WriteLine(res4.Value.Count);   
            service.JoinBoard("amielhagever@gmail.com", "amiel@gmail.com", "boardz");
            var res = service.AddTask("amiel@gmail.com", "amiel@gmail.com", "boardz", "MAKORE", "BSEDER", new DateTime(2021, 6, 5, 4, 5, 5));
            service.AdvanceTask("amiel@gmail.com", "amiel@gmail.com", "boardz", 0, res.Value.Id);
            var res1 = service.AddTask("amiel@gmail.com", "amiel@gmail.com", "boardz", "MA ATA ROTZE", "MIMENI", new DateTime(2021, 6, 5, 4, 5, 5));
            service.AdvanceTask("amiel@gmail.com", "amiel@gmail.com", "boardz", 0, res1.Value.Id);
            var res2 = service.AddTask("amiel@gmail.com", "amiel@gmail.com", "boardz", "MA MA", "MI ATA YAMANAYAK", new DateTime(2021, 6, 5, 4, 5, 5));
            service.AdvanceTask("amiel@gmail.com", "amiel@gmail.com", "boardz", 0, res2.Value.Id);
            var res3 = service.AddTask("amiel@gmail.com", "amiel@gmail.com", "boardz", "AL DABER ELAY BIHLAL", "ABA SHLI HAYAL AVAL SADIR", new DateTime(2021, 6, 5, 4, 5, 5));
            service.AdvanceTask("amiel@gmail.com", "amiel@gmail.com", "boardz", 0, res3.Value.Id);

            var res5 = service.InProgressTasks("amiel@gmail.com");
            var res6= service.AssignTask("amielhagever@gmail.com", "amiel@gmail.com", "boardz", 0, res.Value.Id, "amiel@gmail.com");
            Console.WriteLine(res5.Value.Count);




            /*string userTable = "Users";
            DUserController userController = new DUserController();
            DBoardsController boardsController = new DBoardsController();
            string command = $"select * from {userTable} where email = 'amiel'";

            List<UserDTO> list = userController.SelectAllUsers();
            Console.WriteLine(list.Count);
            
            boardsController.Update("email@jss", BoardsDTO.BoardIdColumnName, 5);

            */


            /*
            ans = forumController.Insert(forum);
            Console.WriteLine(ans);

            MessageDalController messageController = new MessageDalController();
            MessageDTO message = new MessageDTO(1, "Hello Hi2", "This is my body", forum.Id);
            messageController.Delete(message);
            messageController.Insert(message);

            List<MessageDTO> messages = messageController.SelectAllMessages();
            foreach (MessageDTO m in messages)
            {
                Console.WriteLine(m.Title);
            }

            */

            /*
            Task newTask = new Task(new DateTime(2021,5,15,15,15,15),"milestone2","finish it", 4,0,new User("shapiraido123@gmail.com","ido1Ido1"),"amiel",5);
            DTask dtask = new DTask();
            newTask.setTitle("newmileso");
            dtask.Update("ido", 2, 5, "dueDate", "okg");
            */
        }


    }
}
