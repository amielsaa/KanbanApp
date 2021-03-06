using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.PresentationLayer.Model
{
    public class BoardModel : NotifiableModelObject
    {
        public readonly UserModel user;
        private string _boardName;
        private string _creator;
        private TaskModel _backwardTask;
        private bool _enableForward = false;
        private int _taskIdCount;

        public int TaskIdCount
        {
            get => _taskIdCount;
            set
            {
                this._taskIdCount = value;
                RaisePropertyChanged("TaskIdCount");
            }
        }
        public string BoardName
        {
            get => _boardName;
            set
            {
                this._boardName = value;
                RaisePropertyChanged("BoardName");
            }
        }
        public string Creator
        {
            get => _creator;
            set
            {
                this._creator = value;
                RaisePropertyChanged("Creator");
            }
        }

        public bool EnableForward
        {
            get => _enableForward;
            private set
            {
                _enableForward = value;
                RaisePropertyChanged("EnableForward");
            }
        }

        public TaskModel BackwardTask
        {
            get => _backwardTask;
            set
            {
                this._backwardTask = value;
                EnableForward = value != null;
                RaisePropertyChanged("BackwardTask");
            }
        }
        

        public ObservableCollection<ColumnModel> Columns { get; set; }

        private BoardModel(BackendController controller, ObservableCollection<ColumnModel> columns) : base(controller)
        {
            this.Columns = columns;
            Columns.CollectionChanged += HandleChange;
        }

        public BoardModel(BackendController controller, UserModel user,string boardName, string creatorEmail,int taskIdCounter) : base(controller)
        {
            this.user = user;
            this.BoardName = boardName;
            this.Creator = creatorEmail;
            this.TaskIdCount = taskIdCounter;
            if(user.Email != creatorEmail)
            {
                Columns = new ObservableCollection<ColumnModel>(controller.GetAllColumns(user.Email, creatorEmail, boardName, this));
            }
            else
            {
                Columns = new ObservableCollection<ColumnModel>(controller.GetAllColumns(user.Email, user.Email, boardName, this));
            }
            
            Columns.CollectionChanged += HandleChange;
            //laasot getboard me ha backendcontroller
        }

        public BoardModel(BackendController controller, UserModel user) : base(controller)
        {
            this.user = user;
            //Columns = new ObservableCollection<ColumnModel>(controller.GetAllMessagesIds(user.Email).
              //  Select((c, i) => new ColumnModel(controller, controller.GetMessage(user.Email, i), user)).ToList());
            //Columns.CollectionChanged += HandleChange;
        }

        internal void SearchTasks(string keyWord)
        {
            foreach(var column in Columns)
            {
                foreach(var task in column.Tasks)
                {
                    task.Visible = "Collapsed";
                    if(task.Description.Contains(keyWord) | task.Title.Contains(keyWord))
                    {
                        task.Visible = "Visible";
                    }
                }
            }
        }

        public void RemoveColumn(ColumnModel selectedColumn)
        {

            if (selectedColumn == null)
            {
                throw new ArgumentException("You must choose a column first.");
            }

            Columns.Remove(selectedColumn);
           
        }

        internal void MoveRight(ColumnModel selectedColumn)
        {
            
            if(selectedColumn==null) {
                throw new ArgumentException("You must choose a column first.");
            }

            if (Columns.Count-1 > selectedColumn.ColumnOrdinal)
            {
                int newIndex = selectedColumn.ColumnOrdinal;
                Controller.moveColumn(user.Email, Creator, BoardName, newIndex, 1);
                selectedColumn.ColumnOrdinal += 1;
                Columns[newIndex+1].ColumnOrdinal -= 1;
                Columns.Move(newIndex , newIndex+1);
                
            }
            
           
            
        }

        internal void MoveLeft(ColumnModel selectedColumn)
        {
            if (selectedColumn == null)
            {
                throw new ArgumentException("You must choose a column first.");
            }

            if (selectedColumn.ColumnOrdinal>0)
            {
                
                Controller.moveColumn(user.Email, Creator, BoardName, selectedColumn.ColumnOrdinal, -1);
                int newIndex = selectedColumn.ColumnOrdinal;
                selectedColumn.ColumnOrdinal -= 1;
                Columns[newIndex-1].ColumnOrdinal += 1;
                Columns.Move(newIndex , newIndex-1);
               
            }
        }

        internal void LimitColumn(int columnLimit, ColumnModel column)
        {
            Controller.LimitColumn(user.Email,Creator,BoardName,column.ColumnOrdinal,columnLimit);
        }

        public void AddColumn(string columnName, int columnOrdinal)
        {
            //Controller.AddColumn(user.Email, Creator, BoardName, columnOrdinal, columnName);
            Columns.Insert(columnOrdinal,new ColumnModel(Controller, this,user.Email, columnOrdinal, columnName, true));
            for(int i = columnOrdinal + 1; i < Columns.Count; i++)
            {
                Columns[i].ColumnOrdinal += 1;
            }
        }

        internal void AdvanceTask()
        {
            int columnOrdinal = BackwardTask.ColumnOrdinal;
            var backTask = BackwardTask;
            Controller.AdvanceTask(user.Email,Creator,BoardName,columnOrdinal,BackwardTask.Id);
            
            Columns[columnOrdinal].Tasks.Remove(backTask);
            Columns[columnOrdinal + 1].Tasks.Add(backTask);
            backTask.parentColumn = Columns[columnOrdinal + 1];
        }

        private void HandleChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            
            //read more here: https://stackoverflow.com/questions/4279185/what-is-the-use-of-observablecollection-in-net/4279274#4279274
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (ColumnModel y in e.OldItems)
                {

                    Controller.RemoveColumn(y,user.Email);
                }

            }
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (ColumnModel y in e.NewItems)
                {

                    Controller.AddColumn(user.Email, Creator, BoardName, y.ColumnOrdinal, y.Title);
                }

            }
        }
    }
}
