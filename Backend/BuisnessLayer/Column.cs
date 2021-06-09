using IntroSE.Kanban.Backend.DataAccessLayer;
using IntroSE.Kanban.Backend.DataAccessLayer.DalObjects;
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
        private int columnOrdinal;
        protected List<Task> tasks;
        public string Title { get { return title; } set { title = value; } }
        public int Limit {  get { return limit_task_num; } set { limit_task_num = value; } }
        public int ColumnOrdinal { get { return columnOrdinal; } set { columnOrdinal = value; } }

        
        //constructor

        //create new column constructor
        public Column(string title, int columnOrdinal)
        {
            this.title = title;
            this.columnOrdinal = columnOrdinal;
            tasks = new List<Task>();
        }

        //pull column from database constructor
        public Column(string title, List<Task> tasks , int limit)
        {
            this.title = title;
            this.tasks = tasks;
            limit_task_num = limit;
        }
        //------------------------------------------------------------methods-------------------------------------------------------------------------------------

        //---------------------------------------------------------Limit methods-----------------------------------------------------------------------------
        /// <summary>
        /// change the limit of tasks number in the cloumn
        /// </summary>
        /// <param name="new_limit_task_num">the new limit number of the tasks</param>
        /// <returns>Doesn't return anything,</returns>
        public void changeLimit(int new_limit_task_num, string email, int boardId, int columnNumber)
        {
            if (new_limit_task_num < 0&&new_limit_task_num!=-1)
                throw new ArgumentException("limit can't be negative");
            if (new_limit_task_num < tasks.Count)
                throw new ArgumentException("the are already more tasks in this column");
            limit_task_num = new_limit_task_num;
            DColumn dColumn = new DColumn();
            dColumn.updateColumnLimit(email,boardId,columnNumber,new_limit_task_num);
            
        }

        /// <summary>
        /// check if a task stll can be added to the column
        /// </summary>
        /// <returns>A boolean variable, true if there is enough place and false otherwise</returns>
        public bool checkLimit()
        {
          
            return limit_task_num == -1 || tasks.Count < limit_task_num ;
        }

//-----------------------------------------------------------------Task methods---------------------------------------------------------------------------
       
        public List<Task> getTasks()
        {
            return tasks;
        }

        /// <summary>
        /// gets task by its ID
        /// </summary>
        /// <returns>return the task with this ID</returns>
        public Task getTaskById(int id)
        {
            Task task = tasks.Find(x => x.taskId.Equals(id));
            return task;
        }

        /// <summary>
        /// adding the task to this column
        /// </summary>
        /// <returns>returns nothing, only checking limit and insert the task</returns>
        public void addTask(Task task)
        {
            if (!checkLimit())
                throw new ArgumentException("there's not enough space in this column");
            tasks.Add(task);
            
        }
        /// <summary>
        /// deleting all the task from the column
        /// </summary>
        /// <returns>returns nothing, for each task in the column it calls task function to delete itself from its assignee</returns>
        public void deleteAllTasks()
        {
             UserController userController = UserController.getInstance();
            foreach (Task i in tasks)
            {
                (userController.getUser(i.assigneeEmail)).myAssignments.Remove(i);
            }

        }
        public void changeColumnOrdinal(int newColumnOrdinal ,string CreatorEmail , int boardId)
        {
            DColumn dColumn = new DColumn();
            ColumnDTO columnDTO = dColumn.SelectColumn(CreatorEmail, boardId, columnOrdinal);
            columnDTO.ColumnNumber = newColumnOrdinal ;
            this.columnOrdinal = newColumnOrdinal;
            foreach (Task t in tasks)
            {
                t.setColumnOrdinal(newColumnOrdinal);
            }
        }

    }
}

