using System;
using System.Collections.Generic;
using System.Text;

namespace introSE.KanbanBoard.Backend.BuisnessLayer
{
    public class Column
    {
        //fields
        private string title;
        private int limit_task_num = -1;
        protected List<Task> tasks;
        
        //constructor
        public Column(string title)
        {
            this.title = title;
            tasks = new List<Task>();

        }
        //methods

        /// <summary>
        /// change the limit of tasks number in the cloumn
        /// </summary>
        /// <param name="new_limit_task_num">the new limit number of the tasks</param>
        /// <returns>Doesn't return anything,</returns>
        public void changeLimit(int new_limit_task_num)
        {
            if (new_limit_task_num < 0&&new_limit_task_num!=-1)
                throw new ArgumentException("limit can't be negative");
            if (new_limit_task_num < tasks.Count)
                throw new ArgumentException("the are already more tasks in this column");
            limit_task_num = new_limit_task_num;
        }

        /// <summary>
        /// check if a task stll can be added to the column
        /// </summary>
        /// <returns>A boolean variable, true if there is enough place and false otherwise</returns>
        public bool checkLimit()
        {
          
            return limit_task_num == -1 || tasks.Count < limit_task_num ;
        }

        public List<Task> getTasks()
        {
            return tasks;
        }

        public int getColumnLimit()
        {
            return limit_task_num;
        }
        public string getColumnTitle()
        {
            return title;
        }

        public Task getTaskById(int id)
        {
            Task task = tasks.Find(x => x.taskId.Equals(id));
            return task;
        }

        public void addTask(Task task)
        {
            if (!checkLimit())
                throw new ArgumentException("there's not enough space in this column");
            tasks.Add(task);
        }



    }
}

