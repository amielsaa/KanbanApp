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


        
        internal UserModel Login(string userEmail, string password)
        {
            Response<User> response =Service.Login(userEmail, password);
            if (response.ErrorOccured)
                throw new ArgumentException(response.ErrorMessage);
            return new UserModel(this, response.Value.Email);
        }
        internal void Register(string userEmail, string password)
        {
            Response response = Service.Register(userEmail, password);
            if (response.ErrorOccured)
                throw new ArgumentException(response.ErrorMessage);

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
            
            List<ColumnModel> columnModels = new List<ColumnModel>();
            ObservableCollection<TaskModel> taskModels = new ObservableCollection<TaskModel>();
            int columnIndex = 0;
            var column = Service.GetColumn(userEmail, creatorEmail, boardName, columnIndex);
            while (!column.ErrorOccured)
            {
                var col = new ColumnModel(this, board,userEmail, columnIndex, Service.GetColumnName(userEmail, creatorEmail, boardName, columnIndex).Value);
                columnModels.Add(col);
                columnIndex++;
                column = Service.GetColumn(userEmail, creatorEmail, boardName, columnIndex);
            }
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

        /**************************************/
        /******** BOARD FUNCTIONALITY *********/
        /**************************************/

        internal List<BoardModel> GetBoards(UserModel user)
        {

            List<BoardModel> list = new List<BoardModel>();
            var res = Service.GetBoards(user.Email);
            if (res.ErrorOccured)
            {
                throw new ArgumentException(res.ErrorMessage);
            }
            foreach(var board in res.Value)
            {
                list.Add(new BoardModel(this, user,board.BoardName,board.CreatorEmail,board.TaskId));
            }

            return list;
        }

        internal void AddBoard(string userEmail, string boardName)
        {
            var action = Service.AddBoard(userEmail, boardName);
            if (action.ErrorOccured)
            {
                throw new ArgumentException(action.ErrorMessage);
            }

        }

        internal BoardModel GetBoardCreator(string creatorEmail, string boardName, UserModel user)
        {
            var board = Service.GetBoardCreator(creatorEmail, boardName, user.Email);
            if (board.ErrorOccured)
            {
                throw new ArgumentException(board.ErrorMessage);
            }
            return new BoardModel(this,user,boardName,creatorEmail,board.Value.TaskId);
        }

        internal void DeleteBoard(string userEmail, string creatorEmail, string boardName)
        {
            var res = Service.RemoveBoard(userEmail, creatorEmail, boardName);
            if(res.ErrorOccured)
            {
                throw new ArgumentException(res.ErrorMessage);
            }
        }


        internal void JoinBoard()
        {

        }




        /**************************************/
        /******** COLUMN FUNCTIONALITY ********/
        /**************************************/

        internal void RenameColumn()
        {

        }

        internal void AddColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal, string columnName)
        {
            var res = Service.AddColumn(userEmail, creatorEmail, boardName, columnOrdinal, columnName);
            if (res.ErrorOccured)
            {
                throw new ArgumentException(res.ErrorMessage);
            }
            
        }

        internal bool RemoveColumn(ColumnModel columnModel, string userEmail)
        {
            var res = Service.RemoveColumn(userEmail, columnModel.userEmail, columnModel.parent.BoardName, columnModel.ColumnOrdinal);
            if (res.ErrorOccured)
            {
                throw new ArgumentException(res.ErrorMessage);
            }
            return true;
        }


        internal void LimitColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int limit)
        {
            var res = Service.LimitColumn(userEmail, creatorEmail, boardName, columnOrdinal, limit);
            if (res.ErrorOccured)
            {
                throw new ArgumentException(res.ErrorMessage);
            }
        }
        internal void moveColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int shiftSize)
        {
            var res = Service.MoveColumn(userEmail, creatorEmail, boardName, columnOrdinal, shiftSize);
            if (res.ErrorOccured)
            {
                throw new ArgumentException(res.ErrorMessage);
            }
        }


        internal List<TaskModel> GetColumnTask(string userEmail,string creatorEmail,string boardName,int columnOrdinal,ColumnModel parentColumn)
        {
            var res = Service.GetColumn(userEmail, creatorEmail, boardName, columnOrdinal);
            if (res.ErrorOccured)
            {
                throw new ArgumentException(res.ErrorMessage);
            }
            List<TaskModel> list = new List<TaskModel>();
            foreach(var task in res.Value)
            {
                list.Add(new TaskModel(this, userEmail, task.emailAssignee, task.Title, task.Description, task.DueDate, task.CreationTime,columnOrdinal, task.Id, parentColumn));
            }
            return list;
        }

        

        /************************************/
        /******** TASK FUNCTIONALITY ********/
        /************************************/

        internal void AdvanceTask(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId)
        {
            var res = Service.AdvanceTask(userEmail, creatorEmail, boardName, columnOrdinal, taskId);
            if (res.ErrorOccured)
            {
                throw new ArgumentException(res.ErrorMessage);
            }
        }

        internal void Update(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, string title,string description,DateTime dueDate)
        {
            var desc = Service.UpdateTaskDescription(userEmail, creatorEmail, boardName, columnOrdinal, taskId, description);
            var ti = Service.UpdateTaskTitle(userEmail, creatorEmail, boardName, columnOrdinal, taskId, title);
            var due = Service.UpdateTaskDueDate(userEmail, creatorEmail, boardName, columnOrdinal, taskId, dueDate);
            if(desc.ErrorOccured | ti.ErrorOccured | due.ErrorOccured)
            {
                throw new ArgumentException(desc.ErrorMessage + "\n" + ti.ErrorMessage + "\n" + due.ErrorMessage);
            }
        }

        

        internal void AssignTask(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, string emailAssignee)
        {
            
            var res = Service.AssignTask(userEmail,creatorEmail,boardName,columnOrdinal,taskId,emailAssignee);
            if (res.ErrorOccured)
            {
                throw new ArgumentException(res.ErrorMessage);
            }


        }

        internal void AddTask(TaskModel task,string userEmail,string creatorEmail,string boardName)
        {
            var res = Service.AddTask(userEmail, creatorEmail, boardName, task.Title, task.Description, task.DueDate);
            if (res.ErrorOccured)
            {
                throw new ArgumentException(res.ErrorMessage);
            }
        }

        

        

        

        

        

    }
}
