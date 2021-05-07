using IntroSE.Kanban.Backend.DataAccessLayer.DalObjects;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public class DBoardsController : DalController
    {
        private const string BoardsTableName = "Boards";

        public DBoardsController() : base(BoardsTableName)
        {

        }


        /*

        public List<ForumDTO> SelectAllForums()
        {
            List<ForumDTO> result = Select().Cast<ForumDTO>().ToList();

            return result;
        }*/


        
        public bool Insert(BoardsDTO board)
        {

            using (var connection = new SQLiteConnection(_connectionString))
            {
                int res = -1;
                SQLiteCommand command = new SQLiteCommand(null, connection);
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {BoardsTableName} ({DTO.EmailColumnName} ,{BoardsDTO.BoardIdColumnName},{BoardsDTO.BoardNameColumnName},{BoardsDTO.TaskIdColumnName}) " +
                        $"VALUES (@emailVal,@boardIdVal,@boardNameVal,@taskIdVal);";
                    
                    SQLiteParameter emailParam = new SQLiteParameter(@"emailVal", board.Email);
                    SQLiteParameter boardIdParam = new SQLiteParameter(@"boardIdVal", board.BoardId);
                    SQLiteParameter boardNameParam = new SQLiteParameter(@"boardNameVal", board.BoardName);
                    SQLiteParameter taskIdParam = new SQLiteParameter(@"taskIdVal", board.TaskId);


                    command.Parameters.Add(emailParam);
                    command.Parameters.Add(boardIdParam);
                    command.Parameters.Add(boardNameParam);
                    command.Parameters.Add(taskIdParam);
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
            BoardsDTO result = new BoardsDTO(reader.GetString(0), reader.GetInt32(1),reader.GetString(2),reader.GetInt32(3));
            return result;

        }
    }
}
