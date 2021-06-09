using System;
using introSE.KanbanBoard.Backend.BuisnessLayer;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.DataAccessLayer;
using IntroSE.Kanban.Backend.DataAccessLayer.DalObjects;
using IntroSE.Kanban.Backend.BuisnessLayer;
using System.Text.RegularExpressions;

namespace introSE.KanbanBoard.Backend.BuisnessLayer
{
    public class UserController
    {
        //fields
        private static UserController instance;
        public List<string> usersEmail;
        public List<User> users;
        private DUserController dUserController;
        private UserDTO newUser;

        //constructor
        private UserController()
        {
            usersEmail = new List<string>();
            users = new List<User>();
            dUserController = new DUserController();
            //boardsController = BoardController.getInstance();

        }
        //methods

        /// <summary>
        /// using UserController as a singletone 
        /// </summary>
        /// <returns>it returns the instance of it therefor there's only one instance of it in the whole program </returns>
        public static UserController getInstance()
        {
            if (instance == null)
            {
                instance = new UserController();
            }
            return instance;
        }
        


        /// <summary>
        /// create a new user in the user's list of the site 
        /// </summary>
        /// <param name="email">Email of user. must not be used before or it'll return an error</param>
        /// <param name="password"> password for the new user</param>
        /// <returns>It rerurns nothing but creates a new user account in the users list </returns>
        public void register(string email, string password)
        {
            email = validateEmail(email);
            email = checkExistance(email);
            User user = new User(email, password);
            users.Add(user);
            usersEmail.Add(email);
            newUser = new UserDTO(email, password,0);
            user.dtoUser = newUser;
            dUserController = new DUserController();
            bool check = dUserController.Insert(newUser);
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
            if (email == null | password == null)
            {
                throw new ArgumentException("input can't be null");
            }    
            email = validateEmail(email);
            User user = getUser(email);
            if (user == null)
            {
                throw new ArgumentException("There's no user with that email");
            }
            else
            {
                if (user.login|| !user.equalPasswords(password))
                    throw new ArgumentException("User is already logged in or password is incorrect");      
                else
                    user.login = true;
            }
            return user;
        }
        public void logout(string userEmail)
        {
            userEmail = validateEmail(userEmail);
            User user = getUser(userEmail);
            user.logout();

        }

        /// <summary>
        /// get a user 
        /// </summary>
        /// <param name="email">Email of user. to check if i exist</param>
        /// <returns>A user if it exist in the users list, returns null otherwise</returns>
        public User getUser(string email)
        {
            
            var user = users.Find(x => x.email.Equals(email));
            if (user == null)
                throw new ArgumentException("User not found.");
            return user;
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
        /// <summary>
        /// pull all users from the database
        /// </summary>
        public void pullAllUsers()
        {
            List<UserDTO> userDtoList = dUserController.SelectAllUsers();
            foreach (UserDTO userDTO in userDtoList)
            {
                User user = new User(userDTO.Email, userDTO.Password, userDTO.getOldPasswords(), userDTO.getMyAssignments(), userDTO.BoardsId);
                string email = userDTO.Email;
                users.Add(user);
                usersEmail.Add(email);
            }

        }

        public void isUserAssignee(string email, int taskId,int boardId,string creatorEmail)
        {
            var user = getUser(email);
            user.checkIfLogedIn();
            if (!(user.myAssignments.Exists(x => x.taskId == taskId & x.boardId == boardId & x.email == creatorEmail)) )
            {
                throw new ArgumentException("The user is'nt the assignee of the current task");
            }
        }

        /// <summary>
        /// checking validity of an email
        /// </summary>
        /// <param name="email">Email of user. Must be logged in</param>
        /// <returns>a string variable with the email, if it isn't valid it returns an error insted. </returns>
        public string validateEmail(string email)
        {
            string expression = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
            + "@"
            + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";
            if (email != null && email[email.Length - 1] < 65)
                throw new ArgumentException("email isn't valid");
            if (Regex.IsMatch(email, expression))
                return email;
            else
                throw new ArgumentException("email isn't valid");
        }

        public void deleteAll()
        {
            dUserController.DeleteAllUsersInfo();
        }



    }
}
