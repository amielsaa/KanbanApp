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
            Boards.Add(new BoardModel(controller, user, "SHH", user.Email));
            //Boards.CollectionChanged += HandleChange;
        }

        
        internal void JoinBoard(string creatorEmail,string boardName)
        {
            var board = Controller.GetBoard(creatorEmail,boardName,user);
            Boards.Add(board);
        }

        public void DeleteBoard(BoardModel selectedBoard)
        {
            if(selectedBoard.Creator != user.Email)
            {
                throw new Exception("Only board creator can delete its boards.");
            }
            Boards.Remove(selectedBoard);
        }
        
        public void AddBoard(BoardModel newBoard)
        {
            Controller.AddBoard(user.Email, newBoard.BoardName);
            Boards.Add(newBoard);

        }
        private void HandleChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (BoardModel y in e.OldItems)
                {

                    Controller.DeleteBoard(user.Email,y.Creator,y.BoardName);
                }

            }
        }
    }
}
