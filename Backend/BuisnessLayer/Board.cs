using IntroSE.Kanban.Backend.DataAccessLayer;
using IntroSE.Kanban.Backend.DataAccessLayer.DalObjects;
using System;
using System.Collections.Generic;

namespace introSE.KanbanBoard.Backend.BuisnessLayer
{
    public class Board
    {
        //fields
        public string name;
        public string creatorEmail;
        public int id;
        public List<Column> columns;
        public int taskId;
        public List<string> boardUsers;
        
        //constructor

        //creating new board constructor
        public Board(string name, string creator,int id)
        {
            this.name = name;
            this.creatorEmail = creator;
            this.id =id;
            taskId = 0;
            columns = new List<Column>();
            Column backlog = new Column("backlog");
            Column inProggress = new Column("inProggress");
            Column done = new Column("done");
            columns.Add(backlog);
            columns.Add(inProggress);
            columns.Add(done);
            InsertColumsToDal(columns);
            boardUsers = new List<string>();
        }

        //pull board from database constructor
        public Board(string name, string creator, int id,int taskId, Column backlog, Column inProgress, Column done , List<string> bUsers)
        {
            this.name = name;
            creatorEmail = creator;
            this.id = id;
            columns = new List<Column>();
            columns.Add(backlog);
            columns.Add(inProgress);
            columns.Add(done);
            boardUsers = bUsers;
            this.taskId = taskId;
        }
//------------------------------------------------------------methods------------------------------------------------------------------------------------

//------------------------------------------------------------User methods------------------------------------------------------------------------------
        
        /// <summary>
        /// check if user is connected to this board
        /// </summary>
        /// <param name="email">the email of the user we are looking for </param>
        /// <returns>returns thrue if  the user is connected and false otherwise</returns>
        public bool searchForUser(string email)
        {
           return boardUsers.Exists(x=>x==email);
        }
//---------------------------------------------------------Task methods ---------------------------------------------------------------------------------

        /// <summary>
        /// add a task to the board in backlog column
        /// </summary>
        /// <param name="dueDate">the date it should be finished</param>
        /// <param name="title">The name of the task</param>
        /// <param name="description">a summary of the task and what the user need to do to complete it </param>
        /// <returns>A task in the backlog column if there's a place in it, returns error otherwise</returns>
        public Task addTask(DateTime dueDate, string title, string description,string assignee,User user)
        {
            if (columns[0].checkLimit())
            {
                Task task = new Task(dueDate, title, description, taskId, 0, assignee, creatorEmail, id);
                columns[0].addTask(task);
                taskId++;
                user.myAssignments.Add(task);
                return task;
            }
            else
                throw new ArgumentException("There is not enough space in the board");
        }

        /// <summary>
        /// Advance a task to the next column
        /// </summary>
        /// <param name="toDelete">the task the user wants to delete</param>
        /// <param name="columnIndex">the index of the column the task is in</param>
        /// <returns>Doesn't return anything, it returns an error if it can't find the task</returns>
        public void deleteTask(Task toDelete, int columnIndex)
        {
            if (columns[columnIndex].getTasks().Contains(toDelete))
                columns[columnIndex].getTasks().Remove(toDelete);
            else
                throw new ArgumentException("The list of tasks doesnt contains this task");
        }

        /// <summary>
        /// move the task to the next column
        /// </summary>
        /// <param name="toMove"> The task the user want to move</param>
        /// <param name="column_of_the_task">the column thae task is in</param>
        /// <returns>doesn't return anyting, unless the cloumn that was given is the 3rd column which ís the last and it will returm an error.</returns>
        public void moveTask(Task toMove, int column_of_the_task)
        {

            if (column_of_the_task == 0 | column_of_the_task == 1)
            {
                columns[column_of_the_task + 1].getTasks().Add(toMove);
                deleteTask(toMove, column_of_the_task);
                toMove.columnOrdinal = toMove.columnOrdinal + 1;
                (new DTask()).Update(creatorEmail, toMove.boardId, toMove.taskId, TaskDTO.ColumnColumnName, column_of_the_task + 1);
            }
            if (column_of_the_task >= 2)
                throw new ArgumentException("Cannot advance task to a column past Done");
        }

        

        /// <summary>
        /// delete all the tasks from the board (through column)
        /// </summary>
        /// <returns>nothing, only calls all columns to delete its tasks</returns>
        public void deleteAllTasks(UserController userController)
        {
            for (int i = 0; i < columns.Count; i++)
            {
                columns[i].deleteAllTasks(userController);
            }
        }

//--------------------------------------------------------Column methods------------------------------------------------------------------------------------------

        /// <summary>
        /// Advance a task to the next column
        /// </summary>
        /// <param name="index">an index between 0-2 which indicate the  specific cloumn </param>
        /// <returns>A column with the given index</returns>
        public Column getColumn(int index)
        {
            if (index > 2| index<0)
                throw new IndexOutOfRangeException("there is no column in this range");
            return columns[index];
        }

        /// <summary>
        /// insert all the board column to database
        /// </summary>
        /// <param name="columns">all the column that needed to be added </param>
        /// <returns>nothing, only insert the column through Dal</returns>
        private void InsertColumsToDal(List<Column> columns)
        {
            DColumn dColumn = new DColumn();
            foreach (Column i in columns)
            {
                dColumn.Insert(i, id, columns.IndexOf(i), creatorEmail);
            }
        }

    }
}

