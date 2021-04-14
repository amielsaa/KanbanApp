using introSE.KanbanBoard.Backend.BuisnessLayer;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using log4net;
using System.Reflection;
using log4net.Config;
using System.IO;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class ColumnService
    {
        //fields
        UserController userController;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        //constructor
        public ColumnService(UserService userService)
        {
            this.userController = userService.userController;

            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
            log.Info("ColumnService initialized");
        }

        //methods

        /// <summary>
        /// Limit the number of tasks in a specific column
        /// </summary>
        /// <param name="email">The email address of the user, must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="limit">The new limit value. A value of -1 indicates no limit.</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response LimitColumn(string email, string boardName, int columnOrdinal, int limit)
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
                    column.changeLimit(limit);
                    log.Info("Column limited successfully");
                    return new Response();
                }
                else
                    throw new ArgumentException("You must be logged in");

            }catch(Exception e)
            {
                log.Error("Column limitation failed");
                return new Response(e.Message);
            }
        }

        /// <summary>
        /// Get the limit of a specific column
        /// </summary>
        /// <param name="email">The email address of the user, must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <returns>The limit of the column.</returns>
        public Response<int> GetColumnLimit(string email, string boardName, int columnOrdinal)
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
                    log.Info("Column limit retrieved successfully");
                    return Response<int>.FromValue(column.getColumnLimit());
                }
                else
                    throw new ArgumentException("You must be logged in");

            }
            catch (Exception e)
            {
                log.Error("Column limitation failed");
                return Response<int>.FromError(e.Message);
            }
        }

        /// <summary>
        /// Get the name of a specific column
        /// </summary>
        /// <param name="email">The email address of the user, must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <returns>The name of the column.</returns>
        public Response<string> GetColumnName(string email, string boardName, int columnOrdinal)
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
                    log.Info("Column name retrieved successfully");
                    return Response<string>.FromValue(column.getColumnTitle());
                }
                else
                    throw new ArgumentException("You must be logged in");

            }
            catch (Exception e)
            {
                log.Error("Coulnt reach the requested column name");
                return Response<string>.FromError(e.Message);
            }
        }

        /// <summary>
        /// Returns a column given it's name
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <returns>A response object with a value set to the Column, The response should contain a error message in case of an error</returns>
        public Response<IList<Task>> GetColumn(string email, string boardName, int columnOrdinal)
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
                    var tasks = column.getTasks();
                    var taskList = new List<Task>();
                    foreach(var task in tasks)
                    {
                        taskList.Add(new Task(task.taskId, task.getCreationTime(), task.getTitle(), task.getDescription(), task.getDueTime()));
                    }
                    log.Info("Column retrieved successfully");
                    return Response<IList<Task>>.FromValue(taskList);
                }
                else
                    throw new ArgumentException("You must be logged in");

            }
            catch (Exception e)
            {
                log.Error("Couldnt reach the requested coulmn");
                return Response<IList<Task>>.FromError(e.Message);
            }
        }
    }
}
