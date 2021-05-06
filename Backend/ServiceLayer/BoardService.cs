

using introSE.KanbanBoard.Backend.BuisnessLayer;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class BoardService
    {

        UserController userController;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public BoardService(UserService userService)
        {
            this.userController = userService.userController;

            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
            log.Info("BoardService initialized.");
        }


        /// <summary>
        /// Assigns a task to a user
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>        
        /// <param name="emailAssignee">Email of the user to assign to task to</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response AssignTask(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, string emailAssignee)
        {
            try
            {
                var currUser = userController.getUser(userEmail);
                var creatorUser = userController.getUser(creatorEmail);
                if (currUser == null | creatorUser == null)
                    throw new Exception("User doesnt exist");
                Board board = creatorUser.getBoardByName(boardName);
                Column column = board.getColumn(columnOrdinal);
                var task = column.getTaskById(taskId);
                task.changeAssignee(userController.getUser(emailAssignee));
                return new Response();

            } catch(Exception e)
            {
                return new Response(e.Message);
            }
        }

        /// <summary>
        /// Add a new task.
        /// </summary>
        /// <param name="email">Email of the user. The user must be logged in.</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="title">Title of the new task</param>
        /// <param name="description">Description of the new task</param>
        /// <param name="dueDate">The due date if the new task</param>
        /// <returns>A response object with a value set to the Task, instead the response should contain a error message in case of an error</returns>
        public Response<Task> AddTask(string email, string boardName, string title, string description, DateTime dueDate)
        {
            try
            {
                var user = userController.getUser(email);
                if (user == null)
                    throw new ArgumentException("User doesnt exists!");

                if (user.login)
                {
                    Board board = user.getBoardByName(boardName);
                    var task = board.addTask(dueDate,title,description);
                    log.Info("Task created successfully");
                    return Response<Task>.FromValue(new Task(task.taskId, task.getCreationTime(), task.getTitle(), task.getDescription(), task.getDueTime()));
                }
                else
                    throw new ArgumentException("You must be logged in");
            }
            catch (Exception e)
            {
                log.Error("Task creation attempt failed");
                return Response<Task>.FromError(e.Message);
            }
        }
        
        /// <summary>
        /// Advance a task to the next column
        /// </summary>
        /// <param name="email">Email of user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response AdvanceTask(string email, string boardName, int columnOrdinal, int taskId)
        {
            try
            {
                var user = userController.getUser(email);
                if (user == null)
                    throw new ArgumentException("User doesnt exists!");

                if (user.login)
                {
                    
                    Board board = user.getBoardByName(boardName);
                    var task = board.getColumn(columnOrdinal).getTaskById(taskId);
                    board.moveTask(task, columnOrdinal);
                    log.Info("Task advanced successfully");
                    return new Response();
                }
                else
                    throw new ArgumentException("You must be logged in");
            }
            catch (Exception e)
            {
                log.Error("Task advance attempt failed");
                return new Response(e.Message);
            }
        }
        
        /// <summary>
        /// Adds a board to the specific user.
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="name">The name of the new board</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response AddBoard(string email, string name)
        {
            
            try 
            {
                var user = userController.getUser(email);
                if (user == null)
                    throw new ArgumentException("User doesnt exists!");

                if (user.login)
                {
                    user.newBoard(name);
                    log.Info("Board created successfully");
                    return new Response();
                }
                else
                    throw new ArgumentException("You must be logged in");
            }
            catch (Exception e)
            {
                log.Error("Board creation attempt failed");
                return new Response(e.Message);
            }
        }
        /// <summary>
        /// Removes a board to the specific user.
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="name">The name of the board</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response RemoveBoard(string email, string name)
        {
            try
            {
                var user = userController.getUser(email);
                if (user == null)
                    throw new ArgumentException("User doesnt exists!");

                if (user.login) {
                    user.removeBoard(user.getBoardByName(name));
                    log.Info("Board removed successfully");
                    return new Response();
                }
                else
                {
                    throw new ArgumentException("User must be logged in.");
                }
            }
            catch(Exception e)
            {
                log.Error("Task removal attempt failed");
                return new Response(e.Message);
            }
        }
        /// <summary>
        /// Returns all the In progress tasks of the user.
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <returns>A response object with a value set to the list of tasks, The response should contain a error message in case of an error</returns>
        public Response<IList<Task>> InProgressTasks(string email)
        {
            try
            {
                var user = userController.getUser(email);
                if (user == null)
                    throw new ArgumentException("User doesnt exists!");

                if (user.login) { 
                    IList<Task> tasks = new List<Task>();
                    foreach(var task in user.getAllInProgressTasks())
                        {
                            tasks.Add(new Task(task.taskId, task.getCreationTime(), task.getTitle(), task.getDescription(), task.getDueTime()));
                        }
                    log.Info("InProgress tasks retrieved successfully");
                    return Response<IList<Task>>.FromValue(tasks);
                }
                else
                {
                    throw new ArgumentException("User must be logged in.");
                }
            }
            catch (Exception e)
            {
                log.Error("InProgress tasks couldnt be reached");
                return Response<IList<Task>>.FromError(e.Message);
            }
        }
    }

}
