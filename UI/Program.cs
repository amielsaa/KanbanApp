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
            Service service = new Service();
            Console.WriteLine((service.LoadData()).ErrorMessage);
            string userTable = "Users";
            DUserController userController = new DUserController();
            DBoardsController boardsController = new DBoardsController();
            string command = $"select * from {userTable} where email = 'amiel'";

            List<UserDTO> list = userController.SelectAllUsers();
            Console.WriteLine(list.Count);
            
            boardsController.Update("email@jss", BoardsDTO.BoardIdColumnName, 5);


          

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
            Console.Read();
            
            UserTests test = new UserTests(new Service());
            test.runTests();
            /*
            Task newTask = new Task(new DateTime(2021,5,15,15,15,15),"milestone2","finish it", 4,0,new User("shapiraido123@gmail.com","ido1Ido1"),"amiel",5);
            DTask dtask = new DTask();
            newTask.setTitle("newmileso");
            dtask.Update("ido", 2, 5, "dueDate", "okg");
            */
        }


    }
}
