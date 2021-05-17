using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using introSE.KanbanBoard.Backend.BuisnessLayer;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DalObjects
{
    public class TaskDTO : DTO
    {
        

        //ColumnNames in db
        public const string BoardIdColumnName = "boardId";
        public const string TaskIdColumnName = "taskId";
        public const string AssigneeColumnName = "assignee";
        public const string ColumnColumnName = "column";
        public const string CreationTimeColumnName = "creationTime";
        public const string DescriptionColumnName = "description";
        public const string TitleColumnName = "title";
        public const string DueDateColumnName = "dueDate";

        //fields
        private bool persisted = false;
        private int _boardId;
        private int _taskId;
        private string _assignee;
        private int _column;
        private string _creationTime;
        private string _description;
        private string _title;
        private string _dueDate;
        //getters-setters
        public int BoardId { get => _boardId; set { _boardId = value; _controller.Update(Email, BoardIdColumnName, value); } }
        public int TaskId { get => _taskId; set { _taskId = value; _controller.Update(Email, TaskIdColumnName, value); } }
        public string Assignee { get => _assignee; set { _assignee = value; _controller.Update(Email, AssigneeColumnName, value); } }
        public int Column { get => _column; set { _column = value; _controller.Update(Email, ColumnColumnName, value); } }
        public string CreationTime { get => _creationTime; set { _creationTime = value; _controller.Update(Email, CreationTimeColumnName, value); } }
        public string Description { get => _description; set { _description = value; _controller.Update(Email, DescriptionColumnName, value); } }
        public string Title { get => _title; set { _title = value; _controller.Update(Email, TitleColumnName, value); } }
        public string DueDate { get => _dueDate; set { _dueDate = value; _controller.Update(Email, DueDateColumnName, value); } }

        public TaskDTO(string email, int boardid, int taskid, string assignee, int column, string creationtime, string description, string title, string duedate) : base(new DTask())
        {
            
            Email = email;
            _boardId = boardid;
            _taskId = taskid;
            _assignee = assignee;
            _column = column;
            _creationTime = creationtime;
            _description = description;
            _title = title;
            _dueDate = duedate;
        }
        public introSE.KanbanBoard.Backend.BuisnessLayer.Task pushTaskFromDal()
        {

            introSE.KanbanBoard.Backend.BuisnessLayer.Task task = new introSE.KanbanBoard.Backend.BuisnessLayer.Task(Email, BoardId, TaskId, Assignee, Column, Convert.ToDateTime(CreationTime), Description, Title, Convert.ToDateTime(DueDate));
            return task;
        }


    }
}
