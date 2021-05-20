using introSE.KanbanBoard.Backend.BuisnessLayer;
using IntroSE.Kanban.Backend.DataAccessLayer.DalObjects;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    class DColumn : DalController
    {
        private const string ColumnTableName = "Column";
        public DColumn() : base(ColumnTableName)
        {
        }
        public ColumnDTO SelectColumn(string email, int boardId, int columnNum)
        {
            ColumnDTO result = null;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                command.CommandText = $"SELECT * FROM {ColumnTableName} WHERE email = '{email}' and boardId = '{boardId}' and columnNumber = '{columnNum}'";

                SQLiteDataReader dataReader = null;
                try
                {
                    connection.Open();
                    dataReader = command.ExecuteReader();
                    if (dataReader.Read())
                        result = (ColumnDTO)ConvertReaderToObject(dataReader);
                }
                finally
                {
                    if (dataReader != null)
                    {
                        dataReader.Close();
                    }

                    command.Dispose();
                    connection.Close();
                }

            }
            return result;
        }
        public void updateColumnLimit(string email, int boardId, int columnNum, int newLimit)
        {
            ColumnDTO column = SelectColumn(email, boardId, columnNum);
            column.TaskLimit = newLimit;

        }
        public bool Insert(Column column, int boardId, int columnNumber, string email)
        {
            ColumnDTO columnDTO = convertBlcolumnToDTO(column, boardId, columnNumber, email);

            using (var connection = new SQLiteConnection(_connectionString))
            {
                int res = -1;
                SQLiteCommand command = new SQLiteCommand(null, connection);
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {ColumnTableName} ({ColumnDTO.EmailColumnName} ,{ColumnDTO.BoardIdColumnName},{ColumnDTO.ColumnNumberColumnName}, {ColumnDTO.TaskLimitColumnName},{ColumnDTO.ColumnNameColumnName}) " +
                        $"VALUES (@CreatorEmail,@boardId,@columnNumber,@taskLimit,@columnName); ";

                    SQLiteParameter newEmail = new SQLiteParameter(@"taskEmail", columnDTO.Email);
                    SQLiteParameter newBoardId = new SQLiteParameter(@"boardId", columnDTO.BoardId);
                    SQLiteParameter newColumnNumber = new SQLiteParameter(@"columnNumber", columnDTO.ColumnNumber);
                    SQLiteParameter newTaskLimit = new SQLiteParameter(@"taskLimit", columnDTO.TaskLimit);
                    SQLiteParameter newColumnName = new SQLiteParameter(@"columnName", columnDTO.ColumnName);

                    command.Parameters.Add(newEmail);
                    command.Parameters.Add(newBoardId);
                    command.Parameters.Add(newColumnName);
                    command.Parameters.Add(newTaskLimit);
                    command.Parameters.Add(newColumnName);

                    command.Prepare();

                    res = command.ExecuteNonQuery();
                }
                catch
                {
                    //log error
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
                return res > 0;
            }
        }


        protected override DTO ConvertReaderToObject(SQLiteDataReader reader)
        {
            ColumnDTO result = new ColumnDTO(reader.GetString(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetString(4));
            return result;

        }
        private ColumnDTO convertBlcolumnToDTO(Column column,int boardId,int columnNumber,string email)
        {
            ColumnDTO columnDTO = new ColumnDTO(email, boardId, columnNumber, column.Limit, column.Title);
            return columnDTO;
        }

    }
}
