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

        public string BoardName { get => _boardName; set { _boardName = value; } }
        public ObservableCollection<ColumnModel> Columns { get; set; }

        private BoardModel(BackendController controller, ObservableCollection<ColumnModel> columns) : base(controller)
        {
            this.Columns = columns;
            Columns.CollectionChanged += HandleChange;
        }

        public BoardModel(BackendController controller, UserModel user,string boardName) : base(controller)
        {
            this.user = user;
            this._boardName = boardName;
            Columns = new ObservableCollection<ColumnModel>(controller.GetAllColumns(user.Email,user.Email,boardName));
            //Columns.CollectionChanged += HandleChange;
        }

        public BoardModel(BackendController controller, UserModel user) : base(controller)
        {
            this.user = user;
            //Columns = new ObservableCollection<ColumnModel>(controller.GetAllMessagesIds(user.Email).
              //  Select((c, i) => new ColumnModel(controller, controller.GetMessage(user.Email, i), user)).ToList());
            //Columns.CollectionChanged += HandleChange;
        }

        public void AddColumn(ColumnModel column)
        {
            Columns.Add(column);
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
