using IntroSE.Kanban.Backend.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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


        //dummy
        internal UserModel Login(string userEmail, string password)
        {
            return new UserModel(this, userEmail);
        }

        //dummy
        public ColumnModel GetColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal)
        {
            var list = Service.GetColumn(userEmail, creatorEmail, boardName, columnOrdinal);
            ObservableCollection<TaskModel> tasks = new ObservableCollection<TaskModel>();
            foreach(var task in list.Value)
            {
                tasks.Add(new TaskModel(this, task.emailAssignee, task.Title, task.Description, task.DueDate));
            }
            return new ColumnModel(this, tasks, columnOrdinal);
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
