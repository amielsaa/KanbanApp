using introSE.KanbanBoard.Backend.BuisnessLayer;
using IntroSE.Kanban.Backend.BuisnessLayer;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
//using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class UserService
    {
        public UserController userController;
        public BoardController boardController;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        public UserService()
        {
            userController = new UserController();
            boardController = userController.boardController;
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
            log.Info("Starting Log!");
            
        }

        

        /// <summary>
        /// Log in an existing user
        /// </summary>
        /// <param name="userEmail">The email address of the user to login</param>
        /// <param name="password">The password of the user to login</param>
        /// <returns>A response object with a value set to the user, instead the response should contain a error message in case of an error</returns>
        public Response<User> Login(string userEmail, string password)
        {
            try
            {
                var user = userController.login(userEmail, password);
                log.Info("User logged in successfully");
                return Response<User>.FromValue(new User(userEmail));
            }
            catch (Exception e)
            {
                log.Error("Something went wrong during login attempt");
                return Response<User>.FromError(e.Message);
            }
        }
        /// <summary>        
        /// Log out an logged-in user. 
        /// </summary>
        /// <param name="userEmail">The email of the user to log out</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response Logout(string userEmail)
        {
            try
            {
                userController.logout(userEmail);
                var user = userController.getUser(userEmail);
                log.Info("User logged out successfully");
                return new Response();
            }
            catch (Exception e)
            {
                log.Error("An error occurd during logout");
                return new Response(e.Message);
            }
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="userEmail">The email address of the user to register</param>
        /// <param name="password">The password of the user to register</param>
        /// <returns>A response object. The response should contain a error message in case of an error<returns>
        public Response Register(string userEmail, string password)
        {
            try
            {
                userController.register(userEmail, password);
                log.Info("User registered successfully");
                return new Response();
            }
            catch (Exception e)
            {
                log.Error("Register failed");
                return new Response(e.Message);
            }
        }

        /// <summary>
        /// Adds a board created by another user to the logged-in user. 
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the new board</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response JoinBoard(string userEmail, string creatorEmail, string boardName)
        {
            try
            {
                var user = userController.getUser(userEmail);
                var creatorUser = userController.getUser(creatorEmail);
                user.joinBoard(creatorUser, boardName);
                log.Info($"User joined {boardName} successfully");
                return new Response();

            }
            catch(Exception e)
            {
                log.Error("Something went wrong during board join.");
                return new Response(e.Message);
            }
        }

        /// <summary>
        /// Creates a new board for the logged-in user.
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="boardName">The name of the new board</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response AddBoard(string userEmail, string boardName)
        {
            try
            {
                var user = userController.getUser(userEmail);
                user.newBoard(boardName);
                log.Info("The user add the board successfully");

                return new Response();
            }
            catch (Exception e)
            {
                log.Error("Something went wrong during adding the board");
                return new Response(e.Message);
            }
        }

        /// <summary>
        /// Removes a board.
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response RemoveBoard(string userEmail, string creatorEmail, string boardName)
        {
            try
            {
                var user = userController.getUser(userEmail);
                Board board = boardController.getBoard(creatorEmail, boardName);
                user.removeBoard(board);
                log.Info("User removed the board successfully");
                return new Response();
            }catch(Exception e)
            {
                log.Error("Something went wrong during remove board");
                return new Response(e.Message);
            }
        }

        public Response<IList<Task>> InProgressTasks(string userEmail)
        {
            try
            {
                var user = userController.getUser(userEmail);
                if (user == null) { throw new ArgumentException("User doesnt exists!"); }
                if (user.login)
                {
                    var toConvertProgTasksList = user.getAllInProgressTasks();
                    IList<Task> inProgList = new List<Task>();
                    foreach (var task in toConvertProgTasksList)
                    {
                        inProgList.Add(new Task(task.taskId, task.getCreationTime(), task.getTitle(), task.getDescription(), task.getDueTime(), task.assigneeEmail));
                    }
                    log.Info("In Progress Tasks fetched successfully.");
                    return Response<IList<Task>>.FromValue(inProgList);
                }
                else
                {
                    log.Error("You must be logged in");
                    throw new ArgumentException("You must be logged in");
                }
            }
            catch (Exception e)
            {
                log.Error("InProgress tasks couldnt be fetched");
                return Response<IList<Task>>.FromError(e.Message);
            }
        }

        /// <summary>
        /// Returns the list of board of a user. The user must be logged-in. The function returns all the board names the user created or joined.
        /// </summary>
        /// <param name="userEmail">The email of the user. Must be logged-in.</param>
        /// <returns>A response object with a value set to the board, instead the response should contain a error message in case of an error</returns>
        public Response<IList<String>> GetBoardNames(string userEmail)
        {
            try
            {
                IList<String> list = new List<String>();
                var boards = boardController.getAllUserBoards(userEmail);
                foreach(var board in boards)
                {
                    list.Add(board.name);
                }
                log.Info("Get board names");
                return Response<IList<String>>.FromValue(list);
            }catch(Exception e)
            {
                log.Error("Something went wrong during get board names");
                return Response<IList<String>>.FromError(e.Message);
            }
        }


    }

}
