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
            Response<User> response =Service.Login(userEmail, password);
            if (response.ErrorOccured)
                throw new ArgumentException(response.ErrorMessage);
            return new UserModel(this, response.Value.Email);
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
        
        internal List<ColumnModel> GetAllColumns(string userEmail,string creatorEmail,string boardName,BoardModel board)
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
            List<ColumnModel> columnModels = new List<ColumnModel>();

            

            columnModels.Add(new ColumnModel(this, board, 0, "backlog"));
            columnModels.Add(new ColumnModel(this, board, 1, "inprogress"));
            columnModels.Add(new ColumnModel(this, board, 2, "done"));
            columnModels.Add(new ColumnModel(this, board, 3, "shit"));
            columnModels.Add(new ColumnModel(this, board, 4, "done"));
            columnModels.Add(new ColumnModel(this, board, 5, "shit"));

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
        /*
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


            ///////
            newList.Add("1Nice board");
            newList.Add("Okay board");
            ///////
            return newList;
        }*/

        internal List<BoardModel> GetBoards(UserModel user)
        {
            List<BoardModel> list = new List<BoardModel>();
            list.Add(new BoardModel(this, user, "oneboard",user.Email));
            list.Add(new BoardModel(this, user, "twoboard", user.Email));
            list.Add(new BoardModel(this, user, "threeboard", user.Email));
            list.Add(new BoardModel(this, user, "frewag", user.Email));
            list.Add(new BoardModel(this, user, "asfsa", user.Email));
            list.Add(new BoardModel(this, user, "thrfxdfeeboard", user.Email));
            list.Add(new BoardModel(this, user, "thrasfdaeeboard", user.Email));
            list.Add(new BoardModel(this, user, "thrasfeeboard", user.Email));
            list.Add(new BoardModel(this, user, "thrfdsfdeeboard", user.Email));

            return list;
        }


        /************************************/
        /******** TASK FUNCTIONALITY ********/
        /************************************/

        internal string UpdateDescription(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, string description)
        {
            return "";
        }


        internal void AddColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal, string columnName)
        {
            if(columnName == "SEMEK")
            {
                throw new ArgumentException("MAKORE");
            }
            
        }

        internal void AdvanceTask(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId)
        {

        }

        internal void LimitColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int limit)
        {

        }

        internal List<TaskModel> GetColumnTask(string userEmail,string creatorEmail,string boardName,int columnOrdinal,ColumnModel parentColumn)
        {
            List<TaskModel> list = new List<TaskModel>();
            list.Add(new TaskModel(this, "amiel", "taskone", "makoreahsheli", DateTime.Now,columnOrdinal,0,parentColumn));
            //list.Add(new TaskModel(this, "amiel", "tasktwo", "makoreahsheli", DateTime.Now));
            return list;
        }

        internal bool RemoveColumn(ColumnModel columnModel)
        {
            return true;
        }

        internal void AssignTask(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, string emailAssignee)
        {
            // if(res=="") else throw exception that boardVM catches
            //Service.AssignTask()
            
        }

        internal bool AddTask(TaskModel task)
        {
            return true;
        }

        

        internal void AddBoard(string userEmail, string boardName)
        {
            /////// testing
            if(boardName == "ok")
            {
                throw new ArgumentException("fasfdsa");
            }
            ///////
            /*
            var action = Service.AddBoard(userEmail, boardName);
            if (action.ErrorOccured)
            {
                throw new ArgumentException(action.ErrorMessage);
            }*/

        }

        internal BoardModel GetBoard(string creatorEmail,string boardName,UserModel user)
        {

            //implement in service N add IFS
            return new BoardModel(this, user, boardName, creatorEmail);
        }

        internal void DeleteBoard(string userEmail,string creatorEmail, string boardName)
        {
            
        }

        internal void Register(string userEmail, string password)
        {

        }

    }
}
