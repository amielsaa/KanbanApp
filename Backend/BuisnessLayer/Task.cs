using System;
using System.Collections.Generic;
using System.Text;

namespace introSE.KanbanBoard.Backend.BuisnessLayer
{
    public class Task
    {
        //fields
        public int columnOrdinal;
        public int taskId;
        private DateTime creation_time;
        private DateTime due_time;
        private string title;
        private int TITLE_MAX_LENGTH = 50;
        private int DESCRIPTION_MAX_LENGTH = 300;
        private string description;
        
        //constructor
        public Task(DateTime due_time, string title, string description,int id, int columnOrdinal)
        {
            creation_time = DateTime.Now;
            if (due_time > DateTime.Now)
                this.due_time = due_time;
            else
                throw new ArgumentException("The due time  is not possible");
            setTitle(title);
            setDescription(description);
            taskId = id;
            this.columnOrdinal = columnOrdinal;
        }
        //methods

        /// <summary>
        /// checks if chsnges can be made in the task - if it's not it the "done" column
        /// </summary>
        /// <returns>A boolean variable, true if it can be changed or error message otherwise. </returns>
        public Boolean isChangable()
        {
            if (columnOrdinal == 2)
                throw new ArgumentException("A task that is done cannot be changed.");
            else
                return true;
            //return columnOrdinal != 2;
        }

        public DateTime getCreationTime()
        {
            return creation_time;
        }
        public string getTitle()
        {
            return title;
        }
        public DateTime getDueTime()
        {
            return due_time;
        }

        public string getDescription()
        {
            return description;
        }
        public void setTitle(string title)
        {
            if (!isChangable())
                throw new ArgumentException("Task that is done cannot be changed.");
            bool goodTitle = title != null && (title.Length >= 1 & title.Length <= TITLE_MAX_LENGTH);
            if (!goodTitle)
                throw new ArgumentException("Enter new title that answer the requierments");

            this.title = title;
        }
        public void setDescription(string description)
        {
            if (!isChangable())
                throw new ArgumentException("Task that is done cannot be changed.");
            bool goodDescription = description != null && description.Length <= DESCRIPTION_MAX_LENGTH;
            if (!goodDescription)
                throw new ArgumentException("Enter new description that answer the requierments");

            this.description = description;
        }
        public void setDueTime(DateTime DueTime)
        {
            if (!isChangable())
                throw new ArgumentException("Task that is done cannot be changed.");
            if (DueTime>DateTime.Now)
                this.due_time = DueTime;
            else
                throw new ArgumentException("The due time  is not possible");
        }



    }
}

