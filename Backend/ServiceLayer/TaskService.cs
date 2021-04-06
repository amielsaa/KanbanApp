﻿using introSE.KanbanBoard.Backend.BuisnessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class TaskService
    {
        //fields
        UserController userController;
        public TaskService()
        {
            userController = new UserController();
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
                if (user.login)
                {
                    Board board = user.getBoardByName(boardName);
                    Column column = board.getColumn(columnOrdinal);
                    column.getTaskById(taskId).setDueTime(dueDate);
                    return new Response();
                }
                else
                    throw new ArgumentException("You must be logged in");
            }
            catch(Exception e)
            {
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
                if (user.login)
                {
                    Board board = user.getBoardByName(boardName);
                    Column column = board.getColumn(columnOrdinal);
                    column.getTaskById(taskId).setTitle(title);
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
                if (user.login)
                {
                    Board board = user.getBoardByName(boardName);
                    Column column = board.getColumn(columnOrdinal);
                    column.getTaskById(taskId).setDescription(description);
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
    }
}
