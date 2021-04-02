using System;
using System.Collections.Generic;
using System.Text;

namespace introSE.KanbanBoard.Backend.BuisnessLayer
{
    class Task
    {
        //fields
        private DateTime creation_time;
        private string due_time;
        private string title;
        private int TITLE_MAX_LENGTH = 50;
        private int DESCRIPTION_MAX_LENGTH = 300;
        private string description;
        //constructor
        public Task( string due_time, string title, string description)
        {
            creation_time = DateTime.Now;
            this.due_time = due_time;
            setTitle(title);
            setDescription(description);
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
        public void setDueTime(string dueTime)
        {
            this.due_time = dueTime;
        }

        public string getCreationTime()
        {
            return creation_time;
        }
        public string getTitle()
        {
            return title;
        }
        public string getDueTime()
        {
            return due_time;
        }


    }
}

