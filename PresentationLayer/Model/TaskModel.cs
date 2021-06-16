using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.PresentationLayer.Model
{
    public class TaskModel : NotifiableModelObject
    {
        public ColumnModel parentColumn;
        private string _email;
        private string _title;
        private string _description;
        private string _assignee;
        private int _columnOrdinal;
        private int _id;
        private DateTime _duedate;
        private string color = "CornflowerBlue";


        public string Title { get => _title; set { _title = value; RaisePropertyChanged("Title"); } }
        public string Description { get => _description; set { _description = value; RaisePropertyChanged("Description"); } }
        public DateTime DueDate { get => _duedate; set { _duedate = value; RaisePropertyChanged("DueDate"); } }
        public string Assignee { get => _assignee; set { _assignee = value; RaisePropertyChanged("Assignee"); } }
        public int ColumnOrdinal { get => parentColumn.ColumnOrdinal; set {  } }
        public int Id { get => _id; set {_id = value; RaisePropertyChanged("Id"); }}
        public string TaskColor { get => color;set { color = value; RaisePropertyChanged("TaskColor"); } }

        public TaskModel(BackendController controller, string email,string title,string description, DateTime dueDate, int columnOrdinal, int id,ColumnModel parentColumn) : base(controller)
        {
            this._email = email;
            this.Title = title;
            this.Description = description;
            this.DueDate = dueDate;
            this.Assignee = email;
            //this.ColumnOrdinal = parentColumn.ColumnOrdinal;
            this.Id = id;
            this.parentColumn = parentColumn;
        }

        internal void Update(string description,string title,DateTime DueDate, string boardName)
        {
            //Controller.UpdateDescription()
            this.Description = description;
            this.Title = title;
            this.DueDate = DueDate;
        }

        internal void Assign(string newAssignee)
        {
            Controller.AssignTask(_email, parentColumn.parent.Creator, parentColumn.parent.BoardName, ColumnOrdinal, Id, newAssignee);
            this.Assignee = newAssignee;

        }
        
    }
}
