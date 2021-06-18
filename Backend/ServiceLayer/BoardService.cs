

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

        public BoardService()
        {
            this.boardController = BoardController.getInstance();
            this.userController = UserController.getInstance();

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
                var board = boardController.getBoard(creatorEmail, boardName);
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
                var board = boardController.getBoard(creatorEmail, boardName);
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
                var board = boardController.getBoard(creatorEmail, boardName);
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
                var board = boardController.getBoard(creatorEmail, boardName);
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
                var board = boardController.getBoard(creatorEmail, boardName);
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
                var board = boardController.getBoard(creatorEmail, boardName);
                var column = board.getColumn(columnOrdinal);
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
                var board = boardController.getBoard(creatorEmail, boardName);
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
                var board = boardController.getBoard(creatorEmail, boardName);
                var column = board.getColumn(columnOrdinal);
                board.advanceTask(column.getTaskById(taskId), columnOrdinal);
                log.Info("The task moved successfully to the next column");
                return new Response();
            }catch(Exception e)
            {
                log.Error("Something wehnt wrong during moving the task");
                return new Response(e.Message);
            }
        }

        //milestone 3 advanceTask
        public Response advanceTask(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId)
        {
            try
            {
                userController.getUser(userEmail).checkIfLogedIn();
                var board = boardController.getBoard(creatorEmail, boardName);
                var column = board.getColumn(columnOrdinal);
                board.advanceTask(column.getTaskById(taskId), columnOrdinal);
                log.Info("The task moved successfully to the next column");
                return new Response();
            }
            catch (Exception e)
            {
                log.Error("Something wehnt wrong during moving the task");
                return new Response(e.Message);
            }
        }
        /// <summary>
        /// Removes a specific column
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column location. The first column location is identified by 0, the location increases by 1 for each column</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response RemoveColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal)
        {
            try
            {
                userController.getUser(userEmail).checkIfLogedIn();
                var board = boardController.getBoard(creatorEmail, boardName);
                var column = board.getColumn(columnOrdinal);
                board.RemoveColumn( column,columnOrdinal);
                log.Info("The column removed successfully");
                return new Response();

            }
            catch (Exception e)
            {
                log.Error("Cannot remove the column");
                return new Response(e.Message);
            }
        }

        /// <summary>
        /// Renames a specific column
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column location. The first column location is identified by 0, the location increases by 1 for each column</param>
        /// <param name="newColumnName">The new column name</param>        
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response RenameColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal, string newColumnName)
        {
            try
            {
                userController.getUser(userEmail).checkIfLogedIn();
                var board = boardController.getBoard(creatorEmail, boardName);
                var column = board.getColumn(columnOrdinal);
                column.Title = newColumnName;
                log.Info("The column name changed successfully");
                return new Response();
            }
            catch (Exception e)
            {
                log.Error("Cannot changed the name");
                return new Response(e.Message);
            }
        }
        /// <summary>
        /// Moves a column shiftSize times to the right. If shiftSize is negative, the column moves to the left
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column location. The first column location is identified by 0, the location increases by 1 for each column</param>
        /// <param name="shiftSize">The number of times to move the column, relativly to its current location. Negative values are allowed</param>  
        /// <returns>A response object. The response should contain a error message in case of an error</returns>


        public Response MoveColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int shiftSize)
        {
            try
            {
                userController.getUser(userEmail).checkIfLogedIn();
                var board = boardController.getBoard(creatorEmail, boardName);
                var column = board.getColumn(columnOrdinal);
                board.MoveColumn(column,shiftSize) ;
                log.Info("The column moved successfully");
                return new Response();

            }
            catch (Exception e)
            {
                log.Error("Cannot moved the column");
                return new Response(e.Message);
            }
        }

        /// <summary>
        /// Adds a new column
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The location of the new column. Location for old columns with index>=columnOrdinal is increased by 1 (moved right). The first column is identified by 0, the location increases by 1 for each column.</param>
        /// <param name="columnName">The name for the new columns</param>        
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response AddColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal, string columnName)
        {

            try
            {
                userController.getUser(userEmail).checkIfLogedIn();
                var board = boardController.getBoard(creatorEmail, boardName);
                var column = new Column(columnName,columnOrdinal);
                board.AddColumn(column, columnOrdinal);
                log.Info("The column added successfully");
                return new Response();

            }
            catch (Exception e)
            {
                log.Error("Cannot add the column");
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
                var column = boardController.getBoard(creatorEmail, boardName).getColumn(columnOrdinal);
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

        


        public Response<List<Board>> GetBoards(string userEmail)
        {
            try
            {
                List<Board> list = new List<Board>();
                var boards = boardController.getAllUserBoards(userEmail);
                foreach(var board in boards)
                {
                    list.Add(new Board(board.name, board.creatorEmail,board.taskId));
                }
                return Response<List<Board>>.FromValue(list);
            }catch(Exception e)
            {
                return Response<List<Board>>.FromError(e.Message);
            }
        }



    }

}
