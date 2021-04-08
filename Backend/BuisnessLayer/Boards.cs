using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace introSE.KanbanBoard.Backend.BuisnessLayer
{
    class Boards
    {
        //fields
        public List<Board> boards;
        private List<int> boardsId;
        private List<string> boardsName;
        public int id;
        //constructor
        public Boards() {
            boards = new List<Board>();
            boardsId = new List<int>();
            boardsName = new List<string>();
            id = 0;
        }
        //methods
        public void addboard(Board board, string name)
        {
            boards.Add(board);
            boardsName.Add(name);
            boardsId.Add(id);
            id++;
        }
        public Boolean checkValidation(List<string> list, string check)
        {
            foreach (string i in list)
            {
                if (i.Equals(check))
                    return false;
            }
            return true;
        }
        
        public string getValidatename(string name)
        {
            if (!checkValidation(boardsName, name))
                throw new ArgumentException("This board name already exist, choose a diffrent name");
            return name;
        }
        public void removeBoard(Board board)
        {
            if (boards.Exists(x=>x.id==board.id))
            {
                String name = board.name;
                int id = board.id;
                boards.Remove(board);
                boardsName.Remove(name);
                boardsId.Remove(id);
            }
            else
                throw new ArgumentException("no such board exist in your boards list");

        }
        public Board getBoardByName(string name)
        {
            int index = boardsName.IndexOf(name);
            return boards[index];
        }
    }
}
