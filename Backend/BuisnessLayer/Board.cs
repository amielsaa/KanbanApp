using introSE.KanbanBoard.Backend.BuisnessLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace introSE.KanbanBoard.Backend.BuisnessLayer
{
    class Board
    {
        //fields
        public string name;
        private User creator;
        public string id;
        private Column[] columns;
        //constructor
        public Board(string name, User creator, string id, Column backlog, Column inProgress, Column done)
        {
            this.name = name;
            this.creator = creator;
            this.id = id;
            columns = new Column[3];
            columns[0] = backlog;
            columns[1] = inProgress;
            columns[2] = done;
        }
        //methods
        public void addTask(Task toAdd)
        {
            if (columns[0].checkLimit())
                columns[0].getTasks().Add(toAdd);
            else
                Console.WriteLine("The list of tasks are alreaady full");

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



    }
}

