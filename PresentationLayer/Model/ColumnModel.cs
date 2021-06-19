using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace IntroSE.Kanban.PresentationLayer.Model
{
    public class ColumnModel : NotifiableModelObject
    {

        private readonly UserModel user;
        private int columnOrdinal;
        private string title;
        private string userEmail;
        private TaskModel _selectedTask;
        private bool _enableForward = false;
        public BoardModel parent;

        public ObservableCollection<TaskModel> Tasks { get; set; }
        public string Title { get => title; set { } }
        public int ColumnOrdinal { get => columnOrdinal; set { columnOrdinal = value; RaisePropertyChanged("ColumnOrdinal"); } }
        public TaskModel SelectedTask
        {
            get
            {
                return _selectedTask;
            }
            set
            {
                _selectedTask = value;
                EnableForward = value != null;
                parent.BackwardTask = SelectedTask;
                RaisePropertyChanged("SelectedTask");
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


        private ColumnModel(BackendController controller, ObservableCollection<TaskModel> tasks, int columnOrdinal) : base(controller)
        {
            this.Tasks = tasks;
            Tasks.CollectionChanged += HandleChange;
        }


        //loading data constructor
        public ColumnModel(BackendController controller,BoardModel parentBoard, string userEmail,int columnOrdinal, string title) : base(controller)
        {
            this.columnOrdinal = columnOrdinal;
            this.title = title;
            this.userEmail = userEmail;
            Tasks = new ObservableCollection<TaskModel>(controller.GetColumnTask(userEmail, parentBoard.Creator,parentBoard.BoardName, columnOrdinal,this));
            this.parent = parentBoard;
            
        }

        public ColumnModel(BackendController controller, UserModel user,int columnOrdinal) : base(controller)
        {
            this.user = user;
            this.columnOrdinal = columnOrdinal;
            //Tasks = new ObservableCollection<ColumnModel>(controller.GetAllMessagesIds(user.Email).
               // Select((c, i) => new ColumnModel(controller, controller.GetMessage(user.Email, i), user)).ToList());
            //Tasks.CollectionChanged += HandleChange;
        }

        /*
        internal TaskModel AdvanceTask()
        {
            var res = Controller.AdvanceTask("","","", SelectedTask.ColumnOrdinal, SelectedTask.Id);
            if (res)
            {
                Tasks.Remove(SelectedTask);
                return SelectedTask;
            }
        }*/

        

        public void AddTask(TaskModel task)
        {
            Controller.AddTask(task,userEmail,parent.Creator,parent.BoardName);
            Tasks.Add(task);
            parent.TaskIdCount++;
        }

        public void RemoveMessage(TaskModel t)
        {

            Tasks.Remove(t);

        }

        private void HandleChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            //read more here: https://stackoverflow.com/questions/4279185/what-is-the-use-of-observablecollection-in-net/4279274#4279274
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                
                foreach (TaskModel y in e.OldItems)
                {

                    //Controller.RemoveMessage(user.Email, y.Id);
                }

            }
        }

    }
}