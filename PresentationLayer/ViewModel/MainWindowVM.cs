using IntroSE.Kanban.PresentationLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.PresentationLayer.ViewModel
{
    internal class MainWindowVM
    {

        public UserModel user;
        //public List<BoardModel> boardModels;
        public List<string> boardNames;
        private Model.BackendController controller;
        private string _newBoardName;
        private string _clickedBoard;

        public string NewBoardName { get => _newBoardName; set { _newBoardName = value; } }
        public string ClickedBoard { get => _clickedBoard; set { _clickedBoard = value; } }

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
            controller.AddBoard(user.Email, NewBoardName);
            return NewBoardName;
        }

    }
}
