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
        public ColumnModel _columnModel;
        public BoardModel Board { get; private set; }
        private string _message;

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
        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                RaisePropertyChanged("Message");
            }
        }
        public ColumnModel SelectedColumn
        {
            get
            {
                return _columnModel;
            }
            set
            {
                _columnModel = value;
                //EnableForward = value != null;
                RaisePropertyChanged("SelectedColumn");
            }
        }



        internal IList<ColumnModel> Load()
        {
            return controller.GetAllColumns(user.Email, user.Email, "somefuckingboard");
        }

        internal void Logout()
        {

        }

        



        public BoardVM(UserModel user, BoardModel board) 
        {
            this.controller = user.Controller;
            this.user = user;
            this.Board = board;
            
            
        }

        /*
        internal void AdvanceTask()
        {
            try
            {
                var selectedTask = Board.Columns[SelectedTask.ColumnOrdinal].AdvanceTask(SelectedTask);
                Board.Columns[SelectedTask.ColumnOrdinal + 1].AddTask(SelectedTask);
            }catch(Exception e)
            {
                Message = e.Message;
            }
        }*/

        internal void MoveRight()
        {
            try
            {
                Board.MoveRight(SelectedColumn);
            }catch(Exception e)
            {

            }
        }

        internal void MoveLeft()
        {
            try
            {
                Board.MoveLeft(SelectedColumn);
            }
            catch (Exception e)
            {

            }
        }

        internal void RemoveColumn()
        {
            try
            {
                Board.RemoveColumn(SelectedColumn);
            }catch(Exception e)
            {

            }
        }

        internal void AddTask()
        {
            
        }




    }
}
