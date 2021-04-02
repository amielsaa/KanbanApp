using System;
using introSE.KanbanBoard.Backend.BuisnessLayer;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace introSE.KanbanBoard.Backend.BuisnessLayer
{
    class UserController
    {
        //fields
        public List<User> users;
        //constructor
        public UserController()
        {
            users = new List<User>(); 
        }
        //methods
        public User register(string email, string password)
        {
            email = checkExistance(email);
            User user = new User(email, password);
            users.Add(user);
            return user;
        }
        public void login(string email, string password)
        {
            User user = getUser(email);
            if (user == null)
            {
                throw new ArgumentException("There's no user with that email");
            }
            else
            {
                if (password.Equals(user.getpassword()))
                    user.login = true;
                else
                    throw new ArgumentException("Password is incorrect");
            }
        }
        private User getUser(string email)
        {
            return users.Find(x => x.email == email);
        }
        private string checkExistance(string email)
        {
            while (users.Exists(x => x.email == email))
            {
                Console.WriteLine("this email is already registerd, try another email");
                email = Console.ReadLine();
            }
            return email;
        }

    }
}
