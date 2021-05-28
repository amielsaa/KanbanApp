using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PresentationLayer;
using IntroSE.Kanban.Backend.ServiceLayer;


namespace IntroSE.Kanban.PresentationLayer.ViewModel
{
    class LoginVM : NotifiableObject
    {
        private UserService userService = new UserService();
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

        internal void Login()
        {
            var response = userService.Login(Username, Password);
            if (response.ErrorOccured)
                Message = response.ErrorMessage;
            else
                Message = "Login succeeded!";
        }
        internal void Regiester()
        {
            var response = userService.Register(Username, Password);
            if (response.ErrorOccured)
                Message = response.ErrorMessage;
            else
                Message = "Regiester succeeded!";
        }
    }
}
