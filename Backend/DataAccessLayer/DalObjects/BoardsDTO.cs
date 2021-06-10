using introSE.KanbanBoard.Backend.BuisnessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DalObjects
{
    public class BoardsDTO : DTO
    {
        public const string BoardNameColumnName = "boardName";
        public const string BoardIdColumnName = "boardId";
        public const string TaskIdColumnName = "taskId";
        public const string UsersEmailColumnName = "usersEmail";
        public const string ColumnsNumberColumnName = "columnsNumber";


        private string _boardName;
        private int _taskId;
        private string _usersEmail;
        private int _columnsNumber;
        private DBoardsController dBoardsController = new DBoardsController();

        public string BoardName { get => _boardName; set { _boardName = value; dBoardsController.Update(Email,BoardId ,BoardNameColumnName, value); } }
        public int TaskId { get => _taskId; set { _taskId = value; dBoardsController.Update(Email,BoardId ,TaskIdColumnName, value); } }
        public string UsersEmail { get => _usersEmail; set { _usersEmail = value; dBoardsController.Update(Email,BoardId ,UsersEmailColumnName, _usersEmail); } }
        public int ColumnsNumber { get => _columnsNumber;set { _columnsNumber = value;dBoardsController.Update(Email,BoardId ,ColumnsNumberColumnName, _columnsNumber); } }

        public BoardsDTO(string email, int boardId, string boardName, int taskId,int columnsNumber, string usersEmail) : base(new DBoardsController())
        {
            Email = email;
            BoardId = boardId;
            _boardName = boardName;
            _taskId = taskId;
            _usersEmail = usersEmail;
            _columnsNumber = columnsNumber;
        }
        public Board convertToBLBoard()
        {
            DTask dTask = new DTask();
            DBoardsController dBoards = new DBoardsController();
            DColumn column = new DColumn();
            List<Column> columns = new List<Column>();
            List<ColumnDTO> columnDTOs = column.SelectAllColumn(Email,BoardId);
            for (int i = 0; i < ColumnsNumber; i++)
            {
                ColumnDTO c = columnDTOs.Find(x => x.ColumnNumber == i);
                columns.Add(c.convertToBLColumn());
            }
            List<string> boardUsers = dBoards.SelectAllBoardUsers(_usersEmail, BoardId);
            Board board = new Board(_boardName, Email, BoardId, _taskId, columns ,boardUsers);
            return board;
        }



    }
}
