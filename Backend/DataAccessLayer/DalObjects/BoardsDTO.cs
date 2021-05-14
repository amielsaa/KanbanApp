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
        private int _boardId;
        private int _taskId;
        private string _usersEmail;

        public string BoardName { get => _boardName; set { _boardName = value; _controller.Update(Email, BoardNameColumnName, value); } }
        public int BoardId { get => _boardId; set { _boardId = value; _controller.Update(Email, BoardIdColumnName, value); } }
        public int TaskId { get => _taskId; set { _taskId = value; _controller.Update(Email, TaskIdColumnName, value); } }
        public string UsersEmail { get => _usersEmail; set { _usersEmail = value; _controller.Update(Email, UsersEmailColumnName, _usersEmail); } }
        

        public BoardsDTO(string email, int boardId, string boardName, int taskId, string usersEmail) : base(new DBoardsController())
        {
            Email = email;
            _boardName = boardName;
            _boardId = boardId;
            _taskId = taskId;
            _usersEmail = usersEmail;
        }



    }
}
