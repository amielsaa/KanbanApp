using System;
using IntroSE.Kanban.Backend.ServiceLayer;
using IntroSE.Kanban.Backend.DataAccessLayer;
using IntroSE.Kanban.Frontend;
using IntroSE.Kanban.Backend.DataAccessLayer.DalObjects;
using System.Collections.Generic;

namespace UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string userTable = "Users";
            DUserController userController = new DUserController();
            //string command = $"select * from {userTable} where email = 'amiel'";
            List<UserDTO> list = userController.SelectAllUsersEmails();
            Console.WriteLine(list.Count);
            /*
            boardsController.Update("email@jss", BoardsDTO.BoardIdColumnName, 5);
            
            
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


            Console.Read();
            /*
            UserTests test = new UserTests(new Service());
            test.runTests();
            */
        }

    }
}
