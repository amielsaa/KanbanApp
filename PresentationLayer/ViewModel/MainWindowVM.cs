using IntroSE.Kanban.PresentationLayer.Model;
using PresentationLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IntroSE.Kanban.PresentationLayer.ViewModel
{
    internal class MainWindowVM : NotifiableObject
    {

        public UserModel user;
        private BoardModel _selectedBoard;
        public MainModel Main { get; private set; }
        public ObservableCollection<BoardModel> Boards { get; set; }

        //
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

        public BoardModel SelectedBoard
        {
            get
            {
                return _selectedBoard;
            }
            set
            {
                _selectedBoard = value;
                //EnableForward = value != null;
                RaisePropertyChanged("SelectedBoard");
            }
        }

        public MainWindowVM(UserModel user)
        {
            //boardModels = controller.GetBoards(user);
            this.user = user;
            this.controller = user.Controller;
            Main = new MainModel(user.Controller,user);
            //Boards = new ObservableCollection<BoardModel>(controller.GetBoards(user));
            
            //boardNames = controller.GetBoardNames(user.Email);
            
        }

        

        internal void AddBoard()
        {
            try
            {
                Main.AddBoard(new BoardModel(controller,user,NewBoardName,user.Email));
                
            } catch(Exception e)
            {
                Message = e.Message;
                
            }
            
        }

        internal void DeleteBoard()
        {
            try
            {
                Main.DeleteBoard(SelectedBoard);

            } catch(Exception e)
            {
                Message = e.Message;

            }
            
        }

        internal BoardModel EnterBoard()
        {
            return SelectedBoard;
        }


    }
}
