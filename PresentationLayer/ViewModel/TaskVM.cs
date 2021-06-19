using IntroSE.Kanban.PresentationLayer.Model;
using PresentationLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.PresentationLayer.ViewModel
{
    internal class TaskVM : NotifiableObject
    {
        private Model.BackendController controller;
        private UserModel user;
        private ColumnModel columnModel;
        private TaskModel editTask;

        private string _title;
        private string _description;
        private DateTime _dueDate;
        private string _message;
        private string _content;
        private string _boardName;



        public string Title { get => _title; set { _title = value; RaisePropertyChanged("Title"); } }
        public string Description { get => _description; set { _description = value; RaisePropertyChanged("Description"); } }
        public DateTime DueDate { get => _dueDate; set { _dueDate = value; RaisePropertyChanged("DueDate"); } }

        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                RaisePropertyChanged("Message");
            }
        }
        
        
        public string Content
        {
            get => _content;
            set
            {
                _content = value;
                RaisePropertyChanged("Content");
            }
        }

        internal bool AddTask()
        {
            try
            {
                columnModel.AddTask(new TaskModel(controller, user.Email,user.Email, Title, Description, DueDate,columnModel.ColumnOrdinal,columnModel.parent.TaskIdCount,columnModel));
                return true;
            }catch(Exception e)
            {
                Message = e.Message;
                return false;
            }
            
        }

        internal bool EditTask()
        {
            try
            {
                editTask.Update(Description,Title,DueDate, _boardName);
                return true;
                
            }catch(Exception e)
            {
                Message = e.Message;
                return false;
            }
        }

        //add task constructor
        public TaskVM(ColumnModel columnModel, UserModel user,string content)
        {
            this.user = user;
            controller = user.Controller;
            this.columnModel = columnModel;
            this.Content = content;
            this.DueDate = DateTime.Now;
        }


        //edit task constructor
        public TaskVM(TaskModel task,UserModel user, string content, string boardName)
        {
            this.user = user;
            this.controller = user.Controller;
            this.Content = content;
            this.editTask = task;
            this._boardName = boardName;
            Title = task.Title;
            Description = task.Description;
            DueDate = task.DueDate;
        }


    }
}
