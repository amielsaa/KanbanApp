using introSE.KanbanBoard.Backend.BuisnessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DalObjects
{
    class ColumnDTO : DTO
    {
        //ColumnNames in db
        public const string BoardIdColumnName = "boardId";
        public const string ColumnNumberColumnName = "columnNumber";
        public const string TaskLimitColumnName = "taskLimit";
        public const string ColumnNameColumnName = "columnName";

        //fields
        private int _boardId;
        private int _columnNumber;
        private int _taskLimit;
        private string _columnName;

        //getters-setters
        public new int BoardId { get => _boardId; set { _boardId = value; _controller.Update(Email, BoardIdColumnName, value); } }
        public int ColumnNumber { get => _columnNumber; set { _columnNumber = value; _controller.Update(Email, ColumnNumberColumnName, value); } }
        public int TaskLimit { get => _taskLimit; set { _taskLimit = value; _controller.Update(Email, TaskLimitColumnName, value); } }
        public string ColumnName { get => _columnName; set { _columnName = value; _controller.Update(Email, ColumnNameColumnName, value);  }
    }

        public ColumnDTO(string email, int boardId, int column, int taskLimit, string columnName) : base(new DColumn())
        {
            Email = email;
            _boardId = boardId;
            _columnNumber = column;
            _taskLimit = taskLimit;
            _columnName = columnName;
        }
        public Column convertToBLColumn()
        {
            DTask dTask = new DTask();
            List<TaskDTO> taskListDTO = dTask.SelectAllTaskByEmailAndColumn(Email, _columnNumber, BoardId);
            List<introSE.KanbanBoard.Backend.BuisnessLayer.Task> taskList = dTask.convertTasksToBL(taskListDTO);
            Column column = new Column(_columnName, taskList, _taskLimit);
            return column;
        }
    }

}
