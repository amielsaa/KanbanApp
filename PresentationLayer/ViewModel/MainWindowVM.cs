using IntroSE.Kanban.PresentationLayer.Model;
using PresentationLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.PresentationLayer.ViewModel
{
    internal class MainWindowVM : NotifiableObject
    {

        public UserModel user;
        //public List<BoardModel> boardModels;
        public List<string> boardNames;
        private Model.BackendController controller;
        private string _newBoardName;
        private string _boardIndex;
        private string _message;

        public string NewBoardName { get => _newBoardName; set { _newBoardName = value; } }
        public string BoardIndex { get => _boardIndex; set { _boardIndex = value; } }
        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                RaisePropertyChanged("Message");
            }
        }

        public MainWindowVM(UserModel user)
        {
            //boardModels = controller.GetBoards(user);
            this.user = user;
            this.controller = user.Controller;
            boardNames = controller.GetBoardNames(user.Email);
            
        }

        internal BoardModel LoadBoard(string boardName)
        {
            return new BoardModel(controller, user, boardName);
        }

        internal string AddBoard()
        {
            try
            {
                controller.AddBoard(user.Email, NewBoardName);
                return NewBoardName;
            } catch(Exception e)
            {
                Message = e.Message;
                return null;
            }
            
        }

        internal bool RemoveBoard(string boardName)
        {
            try
            {
                controller.RemoveBoard(user.Email, boardName);
                return true;
            } catch(Exception e)
            {
                Message = boardName;
                return false;
            }
            
        }


    }
}
