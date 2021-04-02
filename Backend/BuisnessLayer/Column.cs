using System;
using System.Collections.Generic;
using System.Text;

namespace introSE.KanbanBoard.Backend.BuisnessLayer
{
    class Column
    {
        //fields
        private string title;
        private int limit_task_num = -1;
        protected List<Task> tasks;
        //constructor
        public Column(string title)
        {
            this.title = title;
            LinkedList<Task> tasks = new LinkedList<Task>();

        }
        //methods
        public void changeLimit(int new_limit_task_num)
        {
            limit_task_num = new_limit_task_num;
        }

        public bool checkLimit()
        {
            return tasks.Count < limit_task_num | limit_task_num == -1;
        }

        public List<Task> getTasks()
        {
            return tasks;
        }


    }
}

