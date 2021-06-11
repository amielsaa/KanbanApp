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
        public UserModel user;
        public BoardModel board;
        public IList<ColumnModel> columns; 
        

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

        internal IList<ColumnModel> Load()
        {
            return controller.GetAllColumns(user.Email, user.Email, "somefuckingboard");
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
            this.board = new BoardModel(this.controller, this.user);
            
        }

       
    }
}
