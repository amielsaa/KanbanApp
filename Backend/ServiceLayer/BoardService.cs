

using introSE.KanbanBoard.Backend.BuisnessLayer;
using IntroSE.Kanban.Backend.BuisnessLayer;
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

        public BoardController boardController;
        public UserController userController;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public BoardService(BoardController boardController , UserController userController)
        {
            this.boardController = boardController;
            this.userController = userController;

            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
            log.Info("BoardService initialized.");
        }


        /// <summary>
        /// Update the due date of a task
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="dueDate">The new due date of the column</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response UpdateTaskDueDate(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, DateTime dueDate)
        {
            try
            {
                userController.getUser(userEmail).checkIfLogedIn();
                Board board = boardController.getBoard(creatorEmail, boardName);
                var task = board.getColumn(columnOrdinal).getTaskById(taskId);
                if (task == null)
                    throw new ArgumentException("there is no task with this id");
                userController.isUserAssignee(userEmail, task.taskId, task.boardId, creatorEmail);
                task.setDueTime(dueDate);
                log.Info("Updated the task due date");

                return new Response();
            }catch(Exception e)
            {
                log.Error("Something went wrong during updating the task due date");
                return new Response(e.Message);
            }
        }
        /// <summary>
        /// Update task title
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="title">New title for the task</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response UpdateTaskTitle(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, string title)
        {
            try
            {
                userController.getUser(userEmail).checkIfLogedIn();
                Board board = boardController.getBoard(creatorEmail, boardName);
                var task = board.getColumn(columnOrdinal).getTaskById(taskId);
                userController.isUserAssignee(userEmail, task.taskId, task.boardId, creatorEmail);
                task.setTitle(title);
                log.Info("Updated the task title");
                return new Response();
            }
            catch (Exception e)
            {
                log.Error("Something went wrong during updating the task title");
                return new Response(e.Message);
            }
        }
        /// <summary>
        /// Update the description of a task
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="description">New description for the task</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response UpdateTaskDescription(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, string description)
        {
            try
            {
                userController.getUser(userEmail).checkIfLogedIn();
                Board board = boardController.getBoard(creatorEmail, boardName);
                var task = board.getColumn(columnOrdinal).getTaskById(taskId);
                userController.isUserAssignee(userEmail, task.taskId, task.boardId, creatorEmail);
                task.setDescription(description);
                log.Info("Updated the task description");
                return new Response();
            }
            catch (Exception e)
            {
                log.Error("Something went wrong during updating the task description");
                return new Response(e.Message);
            }
        }


        /// <summary>
        /// Get the limit of a specific column
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <returns>The limit of the column.</returns>
        public Response<int> GetColumnLimit(string userEmail, string creatorEmail, string boardName, int columnOrdinal)
        {
            try
            {
                userController.getUser(userEmail).checkIfLogedIn();
                Board board = boardController.getBoard(creatorEmail, boardName);
                log.Info("Get column limit");
                return Response<int>.FromValue(board.getColumn(columnOrdinal).Limit);
            }catch(Exception e)
            {
                log.Error("Something went wrong during get the column limit");
                return Response<int>.FromError(e.Message);
            }
        }

        /// <summary>
        /// Get the name of a specific column
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <returns>The name of the column.</returns>
        public Response<string> GetColumnName(string userEmail, string creatorEmail, string boardName, int columnOrdinal)
        {
            try
            {
                userController.getUser(userEmail).checkIfLogedIn();
                Board board = boardController.getBoard(creatorEmail, boardName);
                log.Info("Get column name");
                return Response<string>.FromValue(board.getColumn(columnOrdinal).Title);
            }catch(Exception e)
            {
                log.Error("Something went wrong during get the column name");
                return Response<string>.FromError(e.Message);
            }
        }

        /// <summary>
        /// Limit the number of tasks in a specific column
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="limit">The new limit value. A value of -1 indicates no limit.</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response LimitColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int limit)
        {
            try
            {
                var user = userController.getUser(userEmail);
                Board board = boardController.getBoard(creatorEmail, boardName);
                Column column = board.getColumn(columnOrdinal);
                user.ChangeColumnLimit(column, board, limit,creatorEmail);
                log.Info("limit successfully");
                return new Response();
            }catch(Exception e)
            {
                log.Error("limit went wrong");
                return new Response(e.Message);
            }
        }

        

        /// <summary>
        /// Add a new task.
        /// </summary>
		/// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="title">Title of the new task</param>
        /// <param name="description">Description of the new task</param>
        /// <param name="dueDate">The due date if the new task</param>
        /// <returns>A response object with a value set to the Task, instead the response should contain a error message in case of an error</returns>
        public Response<Task> AddTask(string userEmail, string creatorEmail, string boardName, string title, string description, DateTime dueDate)
        {
            try
            {
                userController.getUser(userEmail).checkIfLogedIn();
                Board board = boardController.getBoard(creatorEmail, boardName);
                var task = board.addTask(dueDate, title, description,userEmail,userController.getUser(userEmail));
                log.Info("Added task successfully");
                return Response<Task>.FromValue(new Task(task.taskId, task.getCreationTime(), task.getTitle(), task.getDescription(), task.getDueTime(),task.assigneeEmail));
            }catch(Exception e)
            {
                log.Error("Something went wrong during adding the task");
                return Response<Task>.FromError(e.Message);
            }
        }

        /// <summary>
        /// Advance a task to the next column
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response AdvanceTask(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId)
        {
            try
            {
                userController.getUser(userEmail).checkIfLogedIn();
                Board board = boardController.getBoard(creatorEmail, boardName);
                Column column = board.getColumn(columnOrdinal);
                board.moveTask(column.getTaskById(taskId), columnOrdinal);
                log.Info("The task moved successfully to the next column");
                return new Response();
            }catch(Exception e)
            {
                log.Error("Something wehnt wrong during moving the task");
                return new Response(e.Message);
            }
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
                var task = boardController.getBoard(creatorEmail, boardName).getColumn(columnOrdinal).getTaskById(taskId);
                if (task == null)
                    throw new ArgumentException("there's no such task");
                var user = userController.getUser(userEmail);
                user.checkIfLogedIn();
                var newAssignee = userController.getUser(emailAssignee);

                userController.isUserAssignee(userEmail, task.taskId, task.boardId,creatorEmail);
                user.changeAssignee(newAssignee, task);
                log.Info("Assign task successfully");
                return new Response(); 
                
            }catch(Exception e)
            {
                log.Error("Assign task went wrong");
                return new Response(e.Message);
            }
        }

        /// <summary>
        /// Returns a column given it's name
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <returns>A response object with a value set to the Column, The response should contain a error message in case of an error</returns>
        public Response<IList<Task>> GetColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal)
        {
            try
            {
                userController.getUser(userEmail).checkIfLogedIn();
                Column column = boardController.getBoard(creatorEmail, boardName).getColumn(columnOrdinal);
                var taskList = column.getTasks();
                IList<Task> tasks = new List<Task>();
                foreach(var task in taskList)
                {
                    tasks.Add(new Task(task.taskId, task.getCreationTime(), task.getTitle(), task.getDescription(), task.getDueTime(), task.assigneeEmail));
                }
                log.Info("Get the column successfully");
                return Response<IList<Task>>.FromValue(tasks);
            }
            catch(Exception e)
            {
                log.Error("something went wrong getting the column successfully");
                return Response<IList<Task>>.FromError(e.Message);
            }
        }
        

            /*


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
                    (userController.getUser(userEmail)).changeAssignee(userController.getUser(emailAssignee), task);
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
            }*/
        }

}
