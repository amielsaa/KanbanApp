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
        private readonly UserModel user;
        private string _boardName;
        private string _creator;
        private TaskModel _backwardTask;

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

        public TaskModel BackwardTask
        {
            get => _backwardTask;
            set
            {
                this._backwardTask = value;
                RaisePropertyChanged("BackwardTask");
            }
        }
        

        public ObservableCollection<ColumnModel> Columns { get; set; }

        private BoardModel(BackendController controller, ObservableCollection<ColumnModel> columns) : base(controller)
        {
            this.Columns = columns;
            Columns.CollectionChanged += HandleChange;
        }

        public BoardModel(BackendController controller, UserModel user,string boardName, string creatorEmail) : base(controller)
        {
            this.user = user;
            this.BoardName = boardName;
            this.Creator = creatorEmail;
            Columns = new ObservableCollection<ColumnModel>(controller.GetAllColumns(user.Email,user.Email,boardName,this));
            //Columns.CollectionChanged += HandleChange;
            //laasot getboard me ha backendcontroller
        }

        public BoardModel(BackendController controller, UserModel user) : base(controller)
        {
            this.user = user;
            //Columns = new ObservableCollection<ColumnModel>(controller.GetAllMessagesIds(user.Email).
              //  Select((c, i) => new ColumnModel(controller, controller.GetMessage(user.Email, i), user)).ToList());
            //Columns.CollectionChanged += HandleChange;
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
                selectedColumn.ColumnOrdinal += 1;
                int newIndex = selectedColumn.ColumnOrdinal;
                Columns[newIndex].ColumnOrdinal -= 1;
                Columns.Move(newIndex - 1, newIndex);
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
                selectedColumn.ColumnOrdinal -= 1;
                int newIndex = selectedColumn.ColumnOrdinal;
                Columns[newIndex].ColumnOrdinal += 1;
                Columns.Move(newIndex + 1, newIndex);
            }
        }

        internal void LimitColumn(int columnLimit, ColumnModel column)
        {
            Controller.LimitColumn(user.Email,Creator,BoardName,column.ColumnOrdinal,columnLimit);
        }

        public void AddColumn(string columnName, int columnOrdinal)
        {
            Controller.AddColumn(user.Email, Creator, BoardName, columnOrdinal, columnName);
            Columns.Insert(columnOrdinal,new ColumnModel(Controller, this, columnOrdinal, columnName));
            for(int i = columnOrdinal + 1; i < Columns.Count; i++)
            {
                Columns[columnOrdinal].ColumnOrdinal += 1;
            }
        }

        public void AddTask(TaskModel task)
        {
            Columns[0].Tasks.Add(task);
        }

        private void HandleChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            
            //read more here: https://stackoverflow.com/questions/4279185/what-is-the-use-of-observablecollection-in-net/4279274#4279274
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (ColumnModel y in e.OldItems)
                {

                    //Controller.RemoveMessage(user.Email, y.Id);
                }

            }
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (ColumnModel y in e.OldItems)
                {

                    //Controller.addColumn(user.Email, y.Id);
                }

            }
        }
    }
}
