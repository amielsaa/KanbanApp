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

        /// <summary>
        /// Advance a task to the next column
        /// </summary>
        /// <param name="board">the board that should be added to the boards list</param>
        /// <param name="name">the name of the board</param>
        /// <returns>Doesn't return anything. </returns>
        public void addboard(Board board, string name)
        {
            boards.Add(board);
            boardsName.Add(name);
            boardsId.Add(id);
            id++;
        }

        /// <summary>
        /// check validity (generic)
        /// </summary>
        /// <param name="list"> the list of variables it checks if "check" exist in it</param>
        /// <param name="check">a string we check if it was already used before</param>
        /// <returns> A boolean variable true if check wasn't found in the list and false otherwise</returns>
        public Boolean checkValidation(List<string> list, string check)
        {
            foreach (string i in list)
            {
                if (i.Equals(check))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// check validity of a name - if it was used before by the user 
        /// </summary>
        /// <param name="name"> a name of a board </param>
        /// <returns>The name it was given if it wasn't exist in the list, returns error otherwise</returns>
        public string getValidatename(string name)
        {
            if (!checkValidation(boardsName, name))
                throw new ArgumentException("This board name already exist, choose a diffrent name");
            return name;
        }

        /// <summary>
        /// delete a board from the boards ,name and id lists 
        /// </summary>
        /// <param name="board">the board the user wants to delete</param>
        /// <returns>Doesn't return anything, it returns an error if it can't find the board</returns>
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

        /// <summary>
        /// find a board by its name
        /// </summary>
        /// <param name="name">the name of the board the user search for</param>
        /// <returns>Doesn't return anything, it returns an error if it can't find the task</returns>
        public Board getBoardByName(string name)
        {
            int index = boardsName.IndexOf(name);
            return boards[index];
        }
    }
}
