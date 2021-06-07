using IntroSE.Kanban.PresentationLayer.Model;
using PresentationLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.PresentationLayer.ViewModel
{
    internal class BoardVM : NotifiableObject
    {
        private Model.BackendController controller;
        private UserModel user;
        private ColumnModel[] columns; 
        
        private MessageModel _selectedMessage;
        public MessageModel SelectedMessage
        {
            get
            {
                return _selectedMessage;
            }
            set
            {
                _selectedMessage = value;
                EnableForward = value != null;
                RaisePropertyChanged("SelectedTask");
            }
        }
        private bool _enableForward = false;
        public bool EnableForward
        {
            get => _enableForward;
            private set
            {
                _enableForward = value;
                RaisePropertyChanged("EnableForward");
            }
        }

        internal void Logout()
        {

        }

        internal void AddTask(TaskModel task)
        {

        }

        public BoardVM(UserModel user)
        {
            this.controller = user.Controller;
            this.user = user;
            
        }

       
    }
}
