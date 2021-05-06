using IntroSE.Kanban.Backend.DataAccessLayer.DalObjects;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    class DTask : DalController
    {
        private const string TaskTableName = "Tasks";

        public DTask() : base(TaskTableName)
        {

        }


        /*

        public List<ForumDTO> SelectAllForums()
        {
            List<ForumDTO> result = Select().Cast<ForumDTO>().ToList();

            return result;
        }*/


        /*
        public bool Insert(ForumDTO forum)
        {

            using (var connection = new SQLiteConnection(_connectionString))
            {
                int res = -1;
                SQLiteCommand command = new SQLiteCommand(null, connection);
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {MessageTableName} ({DTO.IDColumnName} ,{ForumDTO.ForumNameColumnName}) " +
                        $"VALUES (@idVal,@nameVal);";

                    SQLiteParameter idParam = new SQLiteParameter(@"idVal", forum.Id);
                    SQLiteParameter titleParam = new SQLiteParameter(@"nameVal", forum.Name);

                    command.Parameters.Add(idParam);
                    command.Parameters.Add(titleParam);
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
        */
        protected override DTO ConvertReaderToObject(SQLiteDataReader reader)
        {
            TaskDTO result = new TaskDTO(reader.GetString(1),reader.GetInt32(1),reader.GetInt32(2),reader.GetString(3),reader.GetInt32(4),
                                         reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8));
            return result;

        }
    }
}
