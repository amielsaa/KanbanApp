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
            Column backlog = new Column("backlog",0);
            Column inProgress = new Column("in progress",1);
            Column done = new Column("done",2);
            columns.Add(backlog);
            columns.Add(inProgress);
            columns.Add(done);
            InsertColumsToDal(columns);
            boardUsers = new List<string>();
        }

        //pull board from database constructor
        public Board(string name, string creator, int id,int taskId, List<Column> columns , List<string> bUsers)
        {
            this.name = name;
            creatorEmail = creator;
            this.id = id;
            this.columns = columns;
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
                Task task = new Task(dueDate, title, description, taskId, assignee, creatorEmail, id);
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

            if (column_of_the_task >=0 && column_of_the_task<columns.Count-1)
            {
                columns[column_of_the_task + 1].getTasks().Add(toMove);
                deleteTask(toMove, column_of_the_task);
                toMove.columnOrdinal = toMove.columnOrdinal + 1;
                (new DTask()).Update(creatorEmail, toMove.boardId, toMove.taskId, TaskDTO.ColumnColumnName, column_of_the_task + 1);
            }
            if (column_of_the_task >= columns.Count-1)
                throw new ArgumentException("Cannot advance task to a column past Done");
        }

        // milestone 3 move task
        public void advanceTask(Task toMove, int column_of_the_task)
        {

            if (column_of_the_task < columns.Count - 1 && column_of_the_task>=0)
            {
                columns[column_of_the_task + 1].getTasks().Add(toMove);
                deleteTask(toMove, column_of_the_task);
                toMove.columnOrdinal = toMove.columnOrdinal + 1;
                (new DTask()).Update(creatorEmail, toMove.boardId, toMove.taskId, TaskDTO.ColumnColumnName, column_of_the_task + 1);
                if (column_of_the_task == columns.Count - 1)
                    toMove.status = true;
            }

            else
                throw new ArgumentException("Cannot advance task to a column past Done");
            
        }

        public void RemoveColumn(Column column, int columnOrdinal)
        {
            Column nextColumn;
            if(columnOrdinal==0)
            {
                nextColumn =getColumn(columnOrdinal + 1);
                int nextColumnEmptyPlace = nextColumn.Limit - nextColumn.getTasks().Count;
                if (nextColumnEmptyPlace < column.getTasks().Count)
                    throw new Exception("Exceed the limit");
                else
                {
                    foreach (Task task in column.getTasks())
                        task.columnOrdinal = columnOrdinal + 1;
                    columns.RemoveAt(columnOrdinal);
                    
                }
            }
            else
            {
                nextColumn = getColumn(columnOrdinal - 1);
                int nextColumnEmptyPlace = nextColumn.Limit - nextColumn.getTasks().Count;
                if (nextColumnEmptyPlace < column.getTasks().Count)
                    throw new Exception("Exceed the limit");
                else
                {
                    foreach (Task task in column.getTasks())
                        task.columnOrdinal = columnOrdinal - 1;
                    columns.RemoveAt(columnOrdinal);
                }
            }
            for (int i = columnOrdinal; i < columns.Count; i++)
                columns[i].changeColumnOrdinal(i, creatorEmail, id);
            
        }

        public void MoveColumn(Column column, int shiftSize)
        {
            int columnIndex = column.ColumnOrdinal;
            if (shiftSize > 0)
            {
                if (columnIndex + shiftSize >= columns.Count)
                    throw new Exception("Too many right shifts");
                else
                {
                    for (int i = columnIndex; i <= columnIndex + shiftSize; i = i + 1)
                    {
                        if (i == columnIndex + shiftSize)
                        {
                            columns[columnIndex + shiftSize] = column;
                            column.changeColumnOrdinal(columnIndex + shiftSize, creatorEmail, id);
                        }
                        else
                        {
                            columns[i] = columns[i + 1];
                            columns[i].changeColumnOrdinal(i, creatorEmail,id);
                        }
                    }
                }
            }
            else
            {
                if (columnIndex + shiftSize < 0)
                    throw new Exception("To many left shifts");
                else
                {
                    for (int i = columnIndex; i > columnIndex + shiftSize; i = i - 1)
                    {
                        if (i == columnIndex + shiftSize)
                        {
                            columns[columnIndex + shiftSize] = column;
                            column.changeColumnOrdinal(columnIndex + shiftSize, creatorEmail, id);
                        }
                        else
                        {
                            columns[i - 1] = columns[i];
                            columns[i-1].changeColumnOrdinal(i-1, creatorEmail, id);
                        }
                    }
                }
            }
        }

        public void AddColumn (Column column, int columnOrdinal)
        {
            if (columnOrdinal >= 0 & columnOrdinal <= columns.Count)
            {
                columns.Insert(columnOrdinal, column);
                for (int index = columnOrdinal+1; index < columns.Count; index = index + 1)
                {
                    columns[index].changeColumnOrdinal(columnOrdinal, creatorEmail, id);
                }
                
            }
            else
                throw new Exception("Invalid column ordinal");
        }

        /// <summary>
        /// delete all the tasks from the board (through column)
        /// </summary>
        /// <returns>nothing, only calls all columns to delete its tasks</returns>
        public void deleteAllTasks()
        {
            for (int i = 0; i < columns.Count; i++)
            {
                columns[i].deleteAllTasks();
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
            if (index > columns.Count| index<0)
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

