using introSE.KanbanBoard.Backend.BuisnessLayer;
using IntroSE.Kanban.Backend.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BuisnessLayer
{
    public class BoardController
    {
        private static BoardController instance;
        private List<Board> allBoards;
        private List<(string, Boards)> allBoardsLists;
        private DBoardsController dBoardController;

        private BoardController()
        {
            allBoards = new List<Board>();
            dBoardController = new DBoardsController();
            List<Board> boardList = dBoardController.SelectAllBoards();
            allBoardsLists = new List<(string, Boards)>();
            foreach (Board board in boardList)
            {
                allBoards.Add(board);
            }
        }
        public void AddBoardsToBC(string email, Boards boards)
        {
            allBoardsLists.Add((email, boards));
        }
        public List<Board> getAllUserBoards(string email)
        {
            List<Board> list = new List<Board>();
            foreach (Board b in allBoards)
            {
                if (b.searchForUser(email))
                    list.Add(b);
            }
            return list;
        }
        public void addBoard(Board board)
        {
            allBoards.Add(board);
            dBoardController.Insert(board);
        }
        public void deleteBoard(Board board)
        {
            board.deleteAllTasks();
            List<string> users = board.boardUsers;
            foreach (string i in users)
            {
                Boards boards = allBoardsLists.Find(x => x.Item1 == i).Item2;
                boards.removeBoard(board);
            }
            allBoards.Remove(board);
            dBoardController.DeleteBoard(board);
        }
        public static  BoardController getInstance()
        {
            if (instance == null)
            {
                instance = new BoardController();
            }

            return instance;
        }

        public Board getBoard(string email,string boardName)
        {
            Board toReturn = allBoards.Find(x => x.creatorEmail.Equals(email) & x.name.Equals(boardName)); ;
            if (toReturn == null)
                throw new ArgumentException("Board not found.");
            return toReturn;
        }


    }
}
