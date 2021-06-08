using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PresentationLayer;
using IntroSE.Kanban.Backend.ServiceLayer;
using IntroSE.Kanban.PresentationLayer.Model;

namespace IntroSE.Kanban.PresentationLayer.ViewModel
{
    class LoginVM : NotifiableObject
    {
        public BackendController Controller { get; private set; }
        //private UserService userService = new UserService();
        private string _username;
        private string _password;
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        private string message = "";
        public string Message
        {
            get => message;
            set
            {
                message = value;
                RaisePropertyChanged("Message");
            }
        }

        internal UserModel Login()
        {
            Message = "";
            try
            {
                return Controller.Login(Username, Password);
            }
            catch (Exception e)
            {
                Message = e.Message;
                return null;
            }
        }
        internal void Register()
        {
            Message = "";
            try
            {
                Controller.Register(Username, Password);
                Message = "Registered successfully";
            }
            catch (Exception e)
            {
                Message = e.Message;
            }
        }

        public LoginVM()
        {
            this.Controller = new BackendController();

        }
    } 
}
