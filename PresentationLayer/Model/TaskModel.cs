﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.PresentationLayer.Model
{
    public class TaskModel : NotifiableModelObject
    {
        private ColumnModel parentColumn;
        private string _email;
        private string _title;
        private string _description;
        private string _assignee;
        private int _columnOrdinal;
        private int _id;
        private DateTime _duedate;
        
        public string Title { get => _title; set { _title = value; RaisePropertyChanged("Title"); } }
        public string Description { get => _description; set { _description = value; RaisePropertyChanged("Description"); } }
        public DateTime DueDate { get => _duedate; set { _duedate = value; RaisePropertyChanged("DueDate"); } }
        public string Assignee { set { _assignee = value; RaisePropertyChanged("Assignee"); } }
        public int ColumnOrdinal { get => parentColumn.ColumnOrdinal; set {  } }
        public int Id { get => _id; set {_id = value; RaisePropertyChanged("Id"); }}

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
        
    }
}
