using introSE.KanbanBoard.Backend.BuisnessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DalObjects
{
    public class UserDTO : DTO 
    {
        //ColumnNames in db
        public const string passwordColumnName = "password";
        public const string boardsIdColumnName = "boardsId";

        //fields
        private string _password;
        private int _boardsId;
        private DUserController DUserControl = new DUserController();

        //getters-setters
        public string Password { get => _password; set { _password = value; _controller.Update(Email, passwordColumnName, value); } }
        public int BoardsId { get => _boardsId; set { _controller.Update(Email, boardsIdColumnName, value); } }
        public UserDTO(string email, string password, int boardId) : base(new DUserController())
        {
            Email = email;
            _boardsId = boardId;
            _password = password;
        }

        public List<Board> getAllBoards()
        {
            DBoardsController dBoards = new DBoardsController();
            return dBoards.SelectAllBoardsByEmail(Email);
        }

        public List<introSE.KanbanBoard.Backend.BuisnessLayer.Task> getMyAssignments()
        {
            DTask dTask = new DTask();
            List<TaskDTO> list =dTask.getMyAssignments(Email);
            return dTask.convertTasksToBL(list);
        }
        public List<string> getOldPasswords()
        {
            string command = $"SELECT password FROM OldPassword WHERE email = '{Email}'";
            List<string> list = DUserControl.SelectString(command);
            return null;
        }


    }
}
