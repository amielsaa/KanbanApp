using System;
using introSE.KanbanBoard.Backend.BuisnessLayer;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace introSE.KanbanBoard.Backend.BuisnessLayer
{
    public class UserController
    {
        //fields
        public List<User> users;
        //constructor
        public UserController()
        {
            users = new List<User>(); 
        }
        //methods
        public void register(string email, string password)
        {
            email = checkExistance(email);
            User user = new User(email, password);
            users.Add(user);
        }
        public User login(string email, string password)
        {
            User user = getUser(email);
            if (user == null)
            {
                throw new ArgumentException("There's no user with that email");
            }
            else
            {
                if (password.Equals(user.Password))
                    user.login = true;
                else
                    throw new ArgumentException("Password is incorrect");
            }
            return user;
        }
        public User getUser(string email)
        {
            return users.Find(x => x.email == email);
        }
        private string checkExistance(string email)
        {
            if (users.Exists(x => x.email == email))
                throw new ArgumentException("this email is already registerd");
            return email;
        }

    }
}
