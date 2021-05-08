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

        //getters-setters
        public string Password { get => _password; set { _password = value; _controller.Update(Email, passwordColumnName, value); } }

        public UserDTO(string email, string password) : base(new DUserController())
        {
            Email = email;
            Password = password;
        }


    }
}
