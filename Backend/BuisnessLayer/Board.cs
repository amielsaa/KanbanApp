using introSE.KanbanBoard.Backend.BuisnessLayer;
using IntroSE.Kanban.Backend.DataAccessLayer;
using IntroSE.Kanban.Backend.DataAccessLayer.DalObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace introSE.KanbanBoard.Backend.BuisnessLayer
{
    public class Board
    {
        //fields
        public string name;
        public List<User> BoardUsers;
        private User creator;
        public int id;
        private Column[] columns;
        public int taskId;
        //constructor
        public Board(string name, User creator,int id, Column backlog, Column inProgress, Column done)
        {
            this.name = name;
            this.creator = creator;
            this.id =id;
            taskId = 0;
            columns = new Column[3];
            columns[0] = backlog;
            columns[1] = inProgress;
            columns[2] = done;
            BoardUsers = new List<User>();
        }
        //methods

        /// <summary>
        /// add a task to the board in backlog column
        /// </summary>
        /// <param name="dueDate">the date it should be finished</param>
        /// <param name="title">The name of the task</param>
        /// <param name="description">a summary of the task and what the user need to do to complete it </param>
        /// <returns>A task in the backlog column if there's a place in it, returns error otherwise</returns>
        public Task addTask(DateTime dueDate, string title, string description)
        {
            if (columns[0].checkLimit())
            {
                Task task = new Task(dueDate, title, description, taskId,0,creator,creator.email,id);
                columns[0].addTask(task);
                taskId++;
                TaskDTO taskdto =new TaskDTO(creator.email, id, taskId, task.getAssignee().email, task.getColumn(),
                    task.getCreationTime().ToString(), task.getDescription(), task.getTitle(), task.getDueTime().ToString());
                DTask dtask = new DTask();
                dtask.Insert(taskdto);
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
            }
            if (column_of_the_task >= 2)
                throw new ArgumentException("Cannot advance task to a column past Done");
        }
        public List<Task> getInProgressTasks()
        {
            if (columns[1] == null)
                throw new ArgumentException("There're no InProgress tasks");
            return columns[1].getTasks();
        }

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

    }
}

