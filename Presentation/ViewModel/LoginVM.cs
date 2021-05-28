using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation;
using IntroSE.Kanban.Backend.ServiceLayer;


namespace IntroSE.Kanban.Presentation.ViewModel
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

        internal Response Login()
        {
            var response = userService.Login(Username, Password);
            if (response.ErrorOccured)
                message = response.ErrorMessage;
            return response;

        }
    }
}
