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
    public class DUserController : DalController
    {
        //fields
        private const string oldPasswordsTableName = "OldPassword";
        private const string UserTableName = "Users";

        //constructor
        public DUserController() : base(UserTableName)
        {
        }

        //---------------------------------------------------methods--------------------------------------------------------------------------------

        //------------------------------------------------Select methods---------------------------------------------------------------------------
        public List<UserDTO> SelectAllUsers()
        {
            List<UserDTO> results = new List<UserDTO>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                command.CommandText = $"SELECT * FROM {UserTableName}";

                SQLiteDataReader dataReader = null;
                try
                {
                    connection.Open();
                    dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                        results.Add((UserDTO)ConvertReaderToObject(dataReader));
                }
                catch (Exception e)
                {
                    throw new ArgumentException("selection from database failed: -select all users-");
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
            return results;
        }
        public UserDTO SelectUser(string email)
        {
            UserDTO result = null;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                command.CommandText = $"SELECT * FROM {UserTableName} WHERE email = '{email}'";

                SQLiteDataReader dataReader = null;
                try
                {
                    connection.Open();
                    dataReader = command.ExecuteReader();
                    if (dataReader.Read())
                        result = (UserDTO)ConvertReaderToObject(dataReader);
                }
                catch (Exception e)
                {
                    throw new ArgumentException("selection from database failed: -select user-");
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

        //-------------------------------------------------Insert methods---------------------------------------------------------------------------
        public bool Insert(UserDTO user)
        {

            using (var connection = new SQLiteConnection(_connectionString))
            {
                int res = -1;
                SQLiteCommand command = new SQLiteCommand(null, connection);
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {UserTableName} ({DTO.EmailColumnName} ,{UserDTO.passwordColumnName}, {UserDTO.boardsIdColumnName}) " +
                        $"VALUES (@emailVal,@passwordVal,@boardsIdVal);";

                    SQLiteParameter emailParam = new SQLiteParameter(@"emailVal", user.Email);
                    SQLiteParameter passwordParam = new SQLiteParameter(@"passwordVal", user.Password);
                    SQLiteParameter boardsIdParam = new SQLiteParameter(@"boardsIdVal", user.BoardsId);
                    command.Parameters.Add(emailParam);
                    command.Parameters.Add(passwordParam);
                    command.Parameters.Add(boardsIdParam);
                    command.Prepare();

                    res = command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw new ArgumentException("insertion to database failed: -insert user-");
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
                return res > 0;
            }
        }
        public bool InsertOldPassword(string oldPassword, string email)
        {

            using (var connection = new SQLiteConnection(_connectionString))
            {
                int res = -1;
                SQLiteCommand command = new SQLiteCommand(null, connection);
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO OldPassword (email, password) VALUES ({email},{oldPassword})";
                    command.Prepare();

                    res = command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw new ArgumentException("insertion to database failed: -insert oldPassword-");
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
                return res > 0;
            }
        }
        //------------------------------------------------Update methods------------------------------------------------------------------------------
        public void updateBoardsIdNum(string email, int boardId)
        {
            UserDTO user = SelectUser(email);
            user.BoardsId = boardId;

        }

        //-------------------------------------------------Convert methods----------------------------------------------------------------------------
        protected override DTO ConvertReaderToObject(SQLiteDataReader reader)
        {
            UserDTO result = new UserDTO(reader.GetString(0), reader.GetString(1), reader.GetInt32(2));
            return result;

        }
        public void DeleteAllUsersInfo()
        {
            DeleteAll(UserTableName);
            DeleteAll(oldPasswordsTableName);
        }

        
       




    }
}
