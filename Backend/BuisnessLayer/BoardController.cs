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
        public List<(string email, Board board, string boardName)> allBoards;
        private DBoardsController dBoardController;

        public BoardController()
        {
            allBoards = new List<(string email, Board board, string boardName)>();
            dBoardController = new DBoardsController();
            List<Board> boardList= dBoardController.SelectAllBoards();
            foreach (Board board in boardList)
            {
                allBoards.Add((board.creatorEmail, board, board.name));
            }
        }
        public List<(string email, Board board, string boardName)> getAllUserBoards(string email)
        {
            List<(string email, Board board, string boardName)> list = new List<(string email, Board board, string boardName)>();
            foreach ((string email, Board board, string boardName) b in allBoards)
            {
                if (b.board.searchForUser(email))
                    list.Add(b);
            }
            return list;
        }

    }
}
