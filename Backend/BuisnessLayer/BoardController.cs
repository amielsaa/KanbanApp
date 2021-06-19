using introSE.KanbanBoard.Backend.BuisnessLayer;
using IntroSE.Kanban.Backend.DataAccessLayer;
using IntroSE.Kanban.Backend.DataAccessLayer.DalObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BuisnessLayer
{
    public class BoardController
    {
        //fields
        private static BoardController instance;
        private List<Board> allBoards;
        private List<(string, Boards)> allBoardsLists;
        private DBoardsController dBoardController;

        //constructor
       private BoardController()
        {
            allBoards = new List<Board>();
            dBoardController = new DBoardsController();
            allBoardsLists = new List<(string, Boards)>();
        }

        internal void deleteAllData()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// using UserController as a singletone 
        /// </summary>
        /// <returns>it returns the instance of it therefor there's only one instance of it in the whole program </returns>
        public static BoardController getInstance()
        {
            if (instance == null)
            {
                instance = new BoardController();
            }

            return instance;
        }
        
        public void pullAllBoards()
        {
            List<BoardsDTO> boardDTOList = dBoardController.SelectAllBoards();
            List<Board> boardList = convertDALlistToBL(boardDTOList);
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
                if (b.searchForUser(email) | b.creatorEmail == email)
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
            foreach (string email in users)
            {
                Boards boards = allBoardsLists.Find(x => x.Item1 == email).Item2;
                boards.removeBoard(board);
            }
            allBoards.Remove(board);
            dBoardController.DeleteBoard(board);
        }


       

        public Board getBoard(string email,string boardName)
        {
            Board toReturn = allBoards.Find(x => x.creatorEmail.Equals(email) & x.name.Equals(boardName));
            if (toReturn == null)
                throw new ArgumentException("Board not found.");
            return toReturn;
        }

        private List<Board> convertDALlistToBL(List<BoardsDTO> list)
        {
            List<Board> BLlist = new List<Board>();
            foreach (BoardsDTO board in list)
            {
                BLlist.Add(convertToBLBoard(board));
            }
            return BLlist;
        }

        public Board convertToBLBoard(BoardsDTO boardsDTO)
        {
            DTask dTask = new DTask();
            DBoardsController dBoards = new DBoardsController();
            DColumn column = new DColumn();
            List<Column> columns = new List<Column>();
            List<ColumnDTO> columnDTOs = column.SelectAllColumn(boardsDTO.Email, boardsDTO.BoardId);
            for (int i = 0; i < boardsDTO.ColumnsNumber; i++)
            {
                ColumnDTO c = columnDTOs.Find(x => x.ColumnNumber == i);
                Column column1 = convertToBLColumn(c);
                columns.Add(column1);
                if (i == boardsDTO.ColumnsNumber - 1)
                {
                    List<introSE.KanbanBoard.Backend.BuisnessLayer.Task> taskList = column1.getTasks();
                    foreach (introSE.KanbanBoard.Backend.BuisnessLayer.Task task in taskList)
                        task.status = true;
                }
            }
            List<string> boardUsers = dBoards.SelectAllBoardUsers(boardsDTO.UsersEmail, boardsDTO.BoardId);
            Board board = new Board(boardsDTO.BoardName, boardsDTO.Email, boardsDTO.BoardId, boardsDTO.TaskId, columns, boardUsers);
            return board;
        }
        public Column convertToBLColumn(ColumnDTO columnDTO)
        {
            DTask dTask = new DTask();
            List<TaskDTO> taskListDTO = dTask.SelectAllTaskByEmailAndColumn(columnDTO.Email, columnDTO.ColumnNumber, columnDTO.BoardId);
            List<introSE.KanbanBoard.Backend.BuisnessLayer.Task> taskList = new List<introSE.KanbanBoard.Backend.BuisnessLayer.Task>();
            foreach (TaskDTO t in taskListDTO)
            {
                taskList.Add(new introSE.KanbanBoard.Backend.BuisnessLayer.Task(t.Email, t.BoardId, t.TaskId, t.Assignee, t.Column, Convert.ToDateTime(t.CreationTime), t.Description, t.Title, Convert.ToDateTime(t.DueDate)));
            }
            Column column = new Column(columnDTO.ColumnName, taskList, columnDTO.TaskLimit, columnDTO.ColumnNumber);
            return column;
        }
    }

   
}
