using System;
using introSE.KanbanBoard.Backend.BuisnessLayer;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.DataAccessLayer;
using IntroSE.Kanban.Backend.DataAccessLayer.DalObjects;

namespace introSE.KanbanBoard.Backend.BuisnessLayer
{
    public class UserController
    {
        //fields
        public List<string> usersEmail;
        public List<User> users;
       // private DUserController;
        //private UserDTO;
        //constructor
        public UserController()
        {
            usersEmail = new List<string>();
            users = new List<User>(); 
        }
        //methods

        /// <summary>
        /// create a new user in the user's list of the site 
        /// </summary>
        /// <param name="email">Email of user. must not be used before or it'll return an error</param>
        /// <param name="password"> password for the new user</param>
        /// <returns>It rerurns nothing but creates a new user account in the users list </returns>
        public void register(string email, string password)
        {
            email = checkExistance(email);
            User user = new User(email, password);
            users.Add(user);
            UserDTO newuser = new UserDTO(email, password);
            DUserController Dusercontrol = new DUserController();
            bool check = Dusercontrol.Insert(newuser);
            if (!check)
            {
                throw new ArgumentException("insertion to dataBase failed");
            }
        } 

        /// <summary>
        /// loggin the user to his account 
        /// </summary>
        /// <param name="email">Email of user. there must be a user with this email</param>
        /// <param name="password"> password that matchs the email for the user</param>
        /// <returns>A user and change its loggin status to logged in, if one of the fields is incorrect it throws an error</returns>
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

        /// <summary>
        /// get a user 
        /// </summary>
        /// <param name="email">Email of user. to check if i exist</param>
        /// <returns>A user if it exist in the users list, returns null otherwise</returns>
        public User getUser(string email)
        {
            return users.Find(x => x.email.Equals(email));
        }

        /// <summary>
        /// check existance of a user in the users list 
        /// </summary>
        /// <param name="email">Email of user.</param>
        /// <returns>A string of the email it was given if it didn't find it in the users list. it'll return error if it does find</returns>
        private string checkExistance(string email)
        {
            if (users.Exists(x => x.email.Equals(email)))
                throw new ArgumentException("this email is already registerd");
            return email;
        }

    }
}
