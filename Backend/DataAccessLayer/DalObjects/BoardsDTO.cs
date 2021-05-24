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


        private string _boardName;
        private int _taskId;
        private string _usersEmail;

        public string BoardName { get => _boardName; set { _boardName = value; _controller.Update(Email, BoardNameColumnName, value); } }
        public int TaskId { get => _taskId; set { _taskId = value; _controller.Update(Email, TaskIdColumnName, value); } }
        public string UsersEmail { get => _usersEmail; set { _usersEmail = value; _controller.Update(Email, UsersEmailColumnName, _usersEmail); } }
        

        public BoardsDTO(string email, int boardId, string boardName, int taskId, string usersEmail) : base(new DBoardsController())
        {
            Email = email;
            BoardId = boardId;
            _boardName = boardName;
            _taskId = taskId;
            _usersEmail = usersEmail;
        }
        public Board convertToBLBoard()
        {
            DTask dTask = new DTask();
            DBoardsController dBoards = new DBoardsController();
            DColumn column = new DColumn();
            Column backlog = column.SelectColumn(Email, BoardId, 0).convertToBLColumn();
            Column inProgress = column.SelectColumn(Email, BoardId, 1).convertToBLColumn();
            Column done = column.SelectColumn(Email, BoardId, 2).convertToBLColumn();
            List<string> boardUsers = dBoards.SelectAllBoardUsers(_usersEmail, BoardId);
            Board board = new Board(_boardName, Email, BoardId, _taskId, backlog, inProgress, done, boardUsers);
            return board;
        }



    }
}
