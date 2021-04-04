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
        public void setTitle(string title)
        {
            bool goodTitle = title.Length >= 1 & title.Length <= TITLE_MAX_LENGTH;
            while (!goodTitle)
            {
                Console.WriteLine("Enter new title that answer the requierments");
                title = Console.ReadLine();
            }
            this.title = title;
        }
        public void setDescription(string description)
        {
            bool goodDescription = description.Length <= DESCRIPTION_MAX_LENGTH;
            while (!goodDescription)
            {
                Console.WriteLine("Enter new description that answer the requierments");
                description = Console.ReadLine();
            }

            this.description = description;
        }
        public void setDueTime(DateTime dueTime)
        {
            this.due_time = dueTime;
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


    }
}

