using IntroSE.Kanban.Backend.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.PresentationLayer.Model
{

    
    public class BackendController
    {
        private Service Service { get; set; }
        public BackendController(Service service)
        {
            this.Service = service;
        }
        public BackendController()
        {
            this.Service = new Service();
            Service.LoadData();
        }


        internal UserModel Login(string userEmail, string password)
        {
            throw new NotImplementedException("");
        }

        internal TaskModel AddTask(string userEmail, string creatorEmail, string boardName, string title, string description, DateTime dueDate)
        {
            throw new NotImplementedException("");
        }

        internal void AddBoard(string userEmail, string boardName)
        {

        }

        internal void Register(string userEmail, string password)
        {

        }

    }
}
