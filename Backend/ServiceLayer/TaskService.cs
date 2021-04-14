using introSE.KanbanBoard.Backend.BuisnessLayer;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class TaskService 
    {
        //fields
        UserController userController;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public TaskService(UserService userService)
        {
            this.userController = userService.userController;

            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
            log.Info("TaskService initialized.");
        }
        /// <summary>
        /// Update the due date of a task
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="dueDate">The new due date of the column</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response UpdateTaskDueDate(string email, string boardName, int columnOrdinal, int taskId, DateTime dueDate)
        {
            try
            {
                var user = userController.getUser(email);
                if (user == null)
                    throw new ArgumentException("User doesnt exists!");

                if (user.login)
                {
                    Board board = user.getBoardByName(boardName);
                    Column column = board.getColumn(columnOrdinal);
                    column.getTaskById(taskId).setDueTime(dueDate);
                    log.Info("Task due date updated successfully");
                    return new Response();
                }
                else
                    throw new ArgumentException("You must be logged in");
            }
            catch(Exception e)
            {
                log.Error("Couldnt update task due date");
                return new Response(e.Message);
            }
            
        }
        /// <summary>
        /// Update task title
        /// </summary>
        /// <param name="email">Email of user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="title">New title for the task</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response UpdateTaskTitle(string email, string boardName, int columnOrdinal, int taskId, string title)
        {
            try
            {
                var user = userController.getUser(email);
                if (user == null)
                    throw new ArgumentException("User doesnt exists!");

                if (user.login)
                {
                    Board board = user.getBoardByName(boardName);
                    Column column = board.getColumn(columnOrdinal);
                    column.getTaskById(taskId).setTitle(title);
                    log.Info("Task title updated successfully");
                    return new Response();
                }
                else
                    throw new ArgumentException("You must be logged in");
            }
            catch (Exception e)
            {
                log.Error("Couldnt update task title");
                return new Response(e.Message);
            }
        }
        /// <summary>
        /// Update the description of a task
        /// </summary>
        /// <param name="email">Email of user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="description">New description for the task</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response UpdateTaskDescription(string email, string boardName, int columnOrdinal, int taskId, string description)
        {
            try
            {
                
                var user = userController.getUser(email);
                if (user == null)
                    throw new ArgumentException("User doesnt exists!");

                if (user.login)
                {
                    Board board = user.getBoardByName(boardName);
                    Column column = board.getColumn(columnOrdinal);
                    column.getTaskById(taskId).setDescription(description);
                    log.Info("Task description updated successfully");
                    return new Response();
                }
                else
                    throw new ArgumentException("You must be logged in");
            }
            catch (Exception e)
            {
                log.Error("Couldnt updated task description");
                return new Response(e.Message);
            }
        }
    }
}
