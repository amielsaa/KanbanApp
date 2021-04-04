using introSE.KanbanBoard.Backend.BuisnessLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace introSE.KanbanBoard.Backend.BuisnessLayer
{
    public class Board
    {
        //fields
        public string name;
        private User creator;
        public string id;
        private Column[] columns;
        public int taskId;
        //constructor
        public Board(string name, User creator, string id, Column backlog, Column inProgress, Column done)
        {
            this.name = name;
            this.creator = creator;
            this.id = id;
            taskId = 0;
            columns = new Column[3];
            columns[0] = backlog;
            columns[1] = inProgress;
            columns[2] = done;
        }
        //methods
        public Task addTask(DateTime dueDate, string title, string description)
        {
            if (columns[0].checkLimit())
            {
                Task task = new Task(dueDate, title, description, taskId);
                columns[0].getTasks().Add(task);
                taskId++;
                return task;
            }

            else
                throw new ArgumentException("There is not enough space in the board");

        }

        public void deleteTask(Task toDelete, int columnIndex)
        {
            if (columns[columnIndex].getTasks().Contains(toDelete))
                columns[columnIndex].getTasks().Remove(toDelete);
            else
                Console.WriteLine("The list of tasks doesnt contains this task");

        }
        public void moveTask(Task toMove, int column_of_the_task)
        {
            if (column_of_the_task == 0 | column_of_the_task == 1)
            {
                columns[column_of_the_task + 1].getTasks().Add(toMove);
                deleteTask(toMove, column_of_the_task);
            }
        }
        public List<Task> getInProgressTasks()
        {
            return columns[1].getTasks();
        }

        public Column getColumn(int index)
        {
            return columns[index];
        }

    }
}

