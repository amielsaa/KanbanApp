using introSE.KanbanBoard.Backend.BuisnessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DalObjects
{
    public class ColumnDTO : DTO
    {
        //ColumnNames in db
        public const string BoardIdColumnName = "boardId";
        public const string ColumnNumberColumnName = "columnNumber";
        public const string TaskLimitColumnName = "taskLimit";
        public const string ColumnNameColumnName = "columnName";

        //fields
        private int _columnNumber;
        private int _taskLimit;
        private string _columnName;

        //getters-setters
        public int ColumnNumber { get => _columnNumber; set { ((DColumn)_controller).Update(Email, BoardId, _columnNumber, ColumnNumberColumnName, value); _columnNumber = value; } }
        public int TaskLimit { get => _taskLimit; set { _taskLimit = value; ((DColumn)_controller).Update(Email, BoardId, _columnNumber, TaskLimitColumnName, value); } }
        public string ColumnName { get => _columnName; set { _columnName = value; ((DColumn)_controller).Update(Email, BoardId, _columnNumber, ColumnNameColumnName, value);  }
    }

        public ColumnDTO(string email, int boardId, int column, int taskLimit, string columnName) : base(new DColumn())
        {
            Email = email;
            BoardId = boardId;
            _columnNumber = column;
            _taskLimit = taskLimit;
            _columnName = columnName;
        }
        
    }

}
