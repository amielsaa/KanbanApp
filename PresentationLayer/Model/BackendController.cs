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
        /*
        public ColumnModel GetColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal)
        {
            var list = Service.GetColumn(userEmail, creatorEmail, boardName, columnOrdinal);
            ObservableCollection<TaskModel> tasks = new ObservableCollection<TaskModel>();
            foreach(var task in list.Value)
            {
                tasks.Add(new TaskModel(this, task.emailAssignee, task.Title, task.Description, task.DueDate));
            }
            //return new ColumnModel(this, tasks, columnOrdinal);
        }*/
        
        internal IList<ColumnModel> GetAllColumns(string userEmail,string creatorEmail,string boardName)
        {
            /* CODE
            IList<ColumnModel> columnModels = new List<ColumnModel>();
            ObservableCollection<TaskModel> taskModels = new ObservableCollection<TaskModel>();
            int columnIndex = 0;
            var column = Service.GetColumn(userEmail, creatorEmail, boardName, columnIndex);
            while(column.ErrorOccured.Equals(""))
            {
                foreach(var task in column.Value)
                {
                    taskModels.Add(new TaskModel(this, task.emailAssignee, task.Title, task.Description, task.DueDate));
                }
                columnModels.Add(new ColumnModel(this, taskModels, columnIndex, Service.GetColumnName(userEmail, creatorEmail, boardName, columnIndex).Value));
                columnIndex++;
            }
            return columnModels;*/

            //dummy for testing
            DateTime dateTime = new DateTime(2021, 9, 2, 2, 2, 2);
            IList<ColumnModel> columnModels = new List<ColumnModel>();

            ObservableCollection<TaskModel> tasks1 = new ObservableCollection<TaskModel>();
            tasks1.Add(new TaskModel(this, "amiel@gmail.com", "A", "backlog tasks", dateTime));
            ObservableCollection<TaskModel> tasks2 = new ObservableCollection<TaskModel>();
            tasks2.Add(new TaskModel(this, "amiel@gmail.com", "B", "inprogress tasks", dateTime));
            ObservableCollection<TaskModel> tasks3 = new ObservableCollection<TaskModel>();
            tasks3.Add(new TaskModel(this, "amiel@gmail.com", "C", "done tasks", dateTime));
            ObservableCollection<TaskModel> tasks4 = new ObservableCollection<TaskModel>();
            tasks4.Add(new TaskModel(this, "amiel@gmail.com", "D", "done tasks", dateTime));
            tasks4.Add(new TaskModel(this, "amiel@gmail.com", "G", "done tasks", dateTime));

            columnModels.Add(new ColumnModel(this, tasks1, 0, "backlog"));
            columnModels.Add(new ColumnModel(this, tasks2, 1, "inprogress"));
            columnModels.Add(new ColumnModel(this, tasks3, 2, "done"));
            columnModels.Add(new ColumnModel(this, tasks4, 3, "shit"));
            return columnModels;
        }

        /*
        internal List<BoardModel> GetBoards(UserModel user)
        {
            List<BoardModel> boardModels = new List<BoardModel>();
            var boardNames = Service.GetBoardNames(user.Email);
            foreach(var name in boardNames.Value)
            {
                boardModels.Add(new BoardModel(this, user, name));
            }
            return boardModels;
        }*/

        internal List<string> GetBoardNames(string userEmail)
        {
            var list = Service.GetBoardNames(userEmail).Value;
            List<string> newList = new List<string>();
            foreach(var boardName in list)
            {
                if(Service.BoardCreator(userEmail,boardName).Value == userEmail)
                {
                    newList.Add(boardName);
                }
            }
            return newList;
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
