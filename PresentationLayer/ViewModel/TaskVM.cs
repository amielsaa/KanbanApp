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

        private string _title;
        private string _description;
        private DateTime _dueDate;
        private string _message;

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

        internal void AddTask()
        {
            try
            {
                columnModel.AddTask(new TaskModel(controller, user.Email, Title, Description, DueDate,columnModel.ColumnOrdinal,0));
            }catch(Exception e)
            {
                Message = e.Message;
            }
            
        }

        public TaskVM(ColumnModel columnModel, UserModel user)
        {
            this.user = user;
            controller = user.Controller;
            this.columnModel = columnModel;
        }

        
    }
}
