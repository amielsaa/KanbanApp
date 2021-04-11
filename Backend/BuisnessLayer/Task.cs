using System;
using System.Collections.Generic;
using System.Text;

namespace introSE.KanbanBoard.Backend.BuisnessLayer
{
    public class Task
    {
        //fields
        public int taskId;
        private DateTime creation_time;
        private DateTime due_time;
        private string title;
        private int TITLE_MAX_LENGTH = 50;
        private int DESCRIPTION_MAX_LENGTH = 300;
        private string description;
        
        //constructor
        public Task(DateTime due_time, string title, string description,int id)
        {
            creation_time = DateTime.Now;
            this.due_time = due_time;
            setTitle(title);
            setDescription(description);
            taskId = id;
        }
        //methods
        
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
            bool goodTitle = title != null && (title.Length >= 1 & title.Length <= TITLE_MAX_LENGTH);
            if (!goodTitle)
                throw new ArgumentException("Enter new title that answer the requierments");
            this.title = title;
        }
        public void setDescription(string description)
        {
            bool goodDescription = description != null && description.Length <= DESCRIPTION_MAX_LENGTH;
            if (!goodDescription)
                throw new ArgumentException("Enter new description that answer the requierments");

            this.description = description;
        }
        public void setDueTime(DateTime DueTime)
        {
            if (DueTime.CompareTo(DateTime.Now) >= 0)
                this.due_time = DueTime;
            else
                throw new ArgumentException("The due time not possible");
        }



    }
}

