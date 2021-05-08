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

        //fields
        private string _password;
        private DUserController DUserControl = new DUserController();

        //getters-setters
        public string Password { get => _password; set { _password = value; _controller.Update(Email, passwordColumnName, value); } }

        public UserDTO(string email, string password) : base(new DUserController())
        {
            Email = email;
            _password = password;
        }

        public List<BoardsDTO> getAllBoards()
        {
            string command = $"SELECT * FROM Boards WHERE email = '{Email}'";
            List<BoardsDTO> result = DUserControl.Select(command).Cast<BoardsDTO>().ToList();

            return result;
        }

        public List<TaskDTO> getMyAssignments()
        {
            string command = $"SELECT * FROM Tasks WHERE assignee = '{Email}'";
            List<TaskDTO> result = DUserControl.Select(command).Cast<TaskDTO>().ToList();

            return result;
        }
        public List<string> getOldPasswords()
        {
            string command = $"SELECT password FROM OldPassword WHERE email = '{Email}'";
            List<string> list = DUserControl.SelectString(command);
            return null;
        }


    }
}
