

using introSE.KanbanBoard.Backend.BuisnessLayer;
using System;
using System.Collections.Generic;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class BoardService
    {

        UserController userController;

        public BoardService()
        {
            userController = new UserController();
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
                if (user.login)
                {
                    Board board = user.getBoardByName(boardName);
                    var task = board.addTask(dueDate,title,description);
                    return Response<Task>.FromValue(new Task(task.taskId, task.getCreationTime(), task.getTitle(), task.getDescription(), task.getDueTime()));
                }
                else
                    throw new ArgumentException("You must be logged in");
            }
            catch (Exception e)
            {
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
                if (user.login)
                {
                    
                    Board board = user.getBoardByName(boardName);
                    var task = board.getColumn(columnOrdinal).getTaskById(taskId);
                    board.moveTask(task, columnOrdinal);
                    return new Response();
                }
                else
                    throw new ArgumentException("You must be logged in");
            }
            catch (Exception e)
            {
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
            throw new NotImplementedException("");
            /*try
            {
                var user = userController.getUser(email);
                if (user.login)
                {
                    user.newBoard(name,)//id?
                }
                else
                    throw new ArgumentException("You must be logged in");
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }*/
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
                user.removeBoard(user.getBoardByName(name));
                return new Response();
            }catch(Exception e)
            {
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
                IList<Task> tasks = new List<Task>();
                foreach(var task in user.getAllInProgressTasks())
                {
                    tasks.Add(new Task(task.taskId, task.getCreationTime(), task.getTitle(), task.getDescription(), task.getDueTime()));
                }
                return Response<IList<Task>>.FromValue(tasks);
            }
            catch (Exception e)
            {
                return Response<IList<Task>>.FromError(e.Message);
            }
        }
    }

}
