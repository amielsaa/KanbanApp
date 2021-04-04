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
        private List<string> boardsId;
        private List<string> boardsName;
        //constructor
        public Boards() {
            boards = new List<Board>();
            boardsId = new List<string>();
            boardsName = new List<string>();
        }
        //methods
        public void addboard(Board board, string name, string id)
        {
            boards.Add(board);
            boardsName.Add(name);
            boardsId.Add(id);
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
        public string getValidateId(string id)
        {
            while (!checkValidation(boardsId, id))
            {
                Console.WriteLine("This board id already exist, choose a diffrent id");
                id = Console.ReadLine();
            }
            return id;
        }
        public string getValidatename(string name)
        {
            while (!checkValidation(boardsName, name))
            {
                Console.WriteLine("This board name already exist, choose a diffrent name");
                name = Console.ReadLine();
            }
            return name;
        }
        public void removeBoard(Board board)
        {
            if (boards.Exists(x=>x.id==board.id))
            {
                String name = board.name;
                String id = board.id;
                boards.Remove(board);
                boardsName.Remove(name);
                boardsId.Remove(id);
            }
            else
                Console.WriteLine("no such board exist in your boards list");

        }
        public Board getBoardByName(string name)
        {
            int index = boardsName.IndexOf(name);
            return boards[index];
        }
    }
}
