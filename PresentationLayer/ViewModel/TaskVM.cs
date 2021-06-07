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

        private string _title = "";
        private string _description = "";
        private DateTime _dueDate;

        public string Title { get => _title; set { _title = value; RaisePropertyChanged(""); } }
        public string Description { get => _description; set { _description = value; RaisePropertyChanged(""); } }
        public DateTime DueDate { get => _dueDate; set { _dueDate = value; RaisePropertyChanged(""); } }


        internal TaskModel AddTask(ColumnModel columnModel)
        {
            //TaskModel taskModel = new TaskModel(controller, user.Email, Title, Description, DueDate);
            TaskModel taskModel = new TaskModel(Title, Description, DueDate);
            //columnModel.AddTask(taskModel);
            return taskModel;
        }

        public TaskVM(UserModel user)
        {
            //this.user = user;
            //this.controller = user.Controller;
            

        }
    }
}
