using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.PresentationLayer.Model
{
    public class MainModel : NotifiableModelObject
    {
        private readonly UserModel user;
        public ObservableCollection<BoardModel> Boards { get; set; }
        public ObservableCollection<TaskModel> InProgressTasks { get; set; }
        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                this._title = value;
                RaisePropertyChanged("Title");
            }
        }

        private MainModel(BackendController controller, ObservableCollection<BoardModel> boards) : base(controller)
        {

            Boards = boards;
            Boards.CollectionChanged += HandleChange;
        }

        public MainModel(BackendController controller, UserModel user) : base(controller)
        {
            this.user = user;
            this.Boards = new ObservableCollection<BoardModel>(controller.GetBoards(user));
            InProgressTasks = GetInProgressTasks();
            Boards.CollectionChanged += HandleChange;
        }

        private ObservableCollection<TaskModel> GetInProgressTasks()
        {
            ObservableCollection<TaskModel> taskModels = new ObservableCollection<TaskModel>();
            foreach(BoardModel boardModel in Boards)
            {
                for(int columnOrdinal = 1;columnOrdinal<boardModel.Columns.Count-1;columnOrdinal++)
                {
                    foreach(TaskModel taskModel in boardModel.Columns[columnOrdinal].Tasks)
                    {
                        if(taskModel.Assignee==user.Email)
                            taskModels.Add(taskModel);
                    }
                }
            }
            return taskModels;
        }


        internal void JoinBoard(string creatorEmail,string boardName)
        {
            var board = Controller.GetBoardCreator(creatorEmail,boardName,user);
            Boards.Add(board);
        }

        public void DeleteBoard(BoardModel selectedBoard)
        {
            Controller.DeleteBoard(user.Email,selectedBoard.Creator,selectedBoard.BoardName);
            Boards.Remove(selectedBoard);
        }
        
        public void AddBoard(string newBoardName)
        {
            Controller.AddBoard(user.Email,newBoardName);
            Boards.Add(new BoardModel(Controller,user,newBoardName,user.Email,0));

        }
        private void HandleChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (BoardModel y in e.OldItems)
                {

                    
                }

            }
        }
    }
}
