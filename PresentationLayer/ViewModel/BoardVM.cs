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
        private string _newAssignee;
        private string _newColumnName;
        private string _newColumnLimit;
        private string _newColumnOrd;
        private bool _enableForward = false;
        private bool _enableButton = false;

        public string NewAssignee { get => _newAssignee; set { _newAssignee = value; } }
        public string NewColumnName { get => _newColumnName; set { _newColumnName = value; EnableButton = NewColumnName != ""; } }
        public string NewColumnOrd { get => _newColumnOrd; set { _newColumnOrd = value; } }
        public string NewColumnLimit { get => _newColumnLimit; set { _newColumnLimit = value; RaisePropertyChanged("NewColumnLimit"); } }

        public bool EnableForward
        {
            get => _enableForward;
            private set
            {
                _enableForward = value;
                RaisePropertyChanged("EnableForward");
            }
        }

        public bool EnableButton
        {
            get => _enableButton;
            set
            {
                _enableButton = value;
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
                EnableForward = value != null;
                RaisePropertyChanged("SelectedColumn");
            }
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

        internal void AdvanceTask()
        {
            try
            {
                Board.AdvanceTask();
            }catch(Exception e)
            {
                Message = e.Message;
            }
        }

        internal void AssignTask()
        {
            try
            {
                if(Board.BackwardTask == null)
                {
                    throw new ArgumentException("You must choose a task first.");
                }
                Board.BackwardTask.Assign(NewAssignee);
            }catch(Exception e)
            {
                Message = e.Message;
            }
        }

        internal TaskModel EditTask()
        {
            try
            {
                var task_to_edit = Board.BackwardTask;
                return task_to_edit;
            }catch(Exception e)
            {
                return null;
            }
        }

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

        internal void AddColumn()
        {
            try
            {
                Board.AddColumn(NewColumnName,Int32.Parse(NewColumnOrd));
            }catch(Exception e)
            {
                Message = e.Message;
            }
        }

        internal void LimitColumn()
        {
            try
            {
                Board.LimitColumn(Int32.Parse(NewColumnLimit), SelectedColumn);
            }catch(Exception e)
            {

            }
        }

        internal void AddTask()
        {
            
        }




    }
}
