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
        public void changeLimit(int new_limit_task_num)
        {
            limit_task_num = new_limit_task_num;
        }

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
            return tasks[id];
        }

        public void addTask(Task task)
        {
            tasks.Add(task);
        }

    }
}

