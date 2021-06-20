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
        private DateTime _creationTime;


        public string Title { get => _title; set { _title = value; RaisePropertyChanged("Title"); } }
        public string Description { get => _description; set { _description = value; RaisePropertyChanged("Description"); } }
        public DateTime DueDate { get => _duedate; set { _duedate = value; RaisePropertyChanged("DueDate"); } }
        public string Assignee { get => _assignee; set { _assignee = value; RaisePropertyChanged("Assignee"); } }
        public int ColumnOrdinal { get => parentColumn.ColumnOrdinal; set {  } }
        public int Id { get => _id; set {_id = value; RaisePropertyChanged("Id"); }}
        public DateTime CreationTime { get => _creationTime; set { _creationTime = value; RaisePropertyChanged("CreationTime"); } }
        public string TaskColor { get => color;set { color = value; RaisePropertyChanged("TaskColor"); } }

        public TaskModel(BackendController controller, string email,string assignee,string title,string description, DateTime dueDate, DateTime creationTime,int columnOrdinal, int id,ColumnModel parentColumn) : base(controller)
        {
            this._email = email;
            this.Title = title;
            this.Description = description;
            this.DueDate = dueDate;
            this.Assignee = assignee;
            //this.ColumnOrdinal = parentColumn.ColumnOrdinal;
            this.Id = id;
            this.CreationTime = creationTime;
            this.parentColumn = parentColumn;
            setColor(dueDate);
        }

        internal void Update(string description,string title,DateTime DueDate, string boardName)
        {
            Controller.Update(_email,parentColumn.parent.Creator,parentColumn.parent.BoardName,parentColumn.ColumnOrdinal,Id,title,description,DueDate);
            this.Description = description;
            this.Title = title;
            this.DueDate = DueDate;
            setColor(DueDate);
        }

        internal void Assign(string newAssignee)
        {
            Controller.AssignTask(parentColumn.parent.user.Email, parentColumn.parent.Creator, parentColumn.parent.BoardName, ColumnOrdinal, Id, newAssignee);
            this.Assignee = newAssignee;

        }
        private void setColor(DateTime dueDate)
        {
            DateTime today = DateTime.Now;
            TimeSpan timeLeft = dueDate.Subtract(today);
            TimeSpan timePassed = today.Subtract(CreationTime);
            TimeSpan timeSpan = dueDate.Subtract(CreationTime);
            double timePassedDays = timePassed.Days;
            double timeSpanDays = timeSpan.Days;
            double presentage = 0;
            if (timeSpanDays != 0 )
                presentage = (timePassedDays / timeSpanDays);
            if (timeLeft.Days < 0)
                TaskColor = "Maroon";
            else if  (presentage>0.75)
                TaskColor = "OrangeRed";
            else
                TaskColor = "CornflowerBlue";
        }
        
    }
}
