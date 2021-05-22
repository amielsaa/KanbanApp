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
    public class DBoardsController : DalController
    {
        //field
        private const string BoardsTableName = "Boards";
        

        //constructor
        public DBoardsController() : base(BoardsTableName)
        {

        }

//-------------------------------------------Update methods-------------------------------------------------------------------------------
        public bool Update(string email,int boardId, string attributeName, string attributeValue)
        {
            int res = -1;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"update {BoardsTableName} set {attributeName}=@attVal where email=@emailVal and boardId=@boardIdVal"
                };
                try
                {
                    command.Parameters.Add(new SQLiteParameter(@"attVal", attributeValue));
                    command.Parameters.Add(new SQLiteParameter(@"boardIdVal", boardId));
                    command.Parameters.Add(new SQLiteParameter(@"emailVal", email));
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw new ArgumentException("update in database failed: -update board (string)-");
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }

            }
            return res > 0;
        }

        public bool Update(string email, int boardId, string attributeName, int attributeValue)
        {
            int res = -1;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"update {BoardsTableName} set {attributeName}=@attVal where email=@emailVal and boardId=@boardIdVal"
                };
                try
                {
                    command.Parameters.Add(new SQLiteParameter(@"attVal", attributeValue));
                    command.Parameters.Add(new SQLiteParameter(@"boardIdVal", boardId));
                    command.Parameters.Add(new SQLiteParameter(@"emailVal", email));
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw new ArgumentException("update in database failed: -update board (int)-");
                }
                finally
                {
                    command.Dispose();
                    connection.Close();

                }

            }
            return res > 0;
        }

//-----------------------------------------------Select methods-------------------------------------------------------------------------------

        public List<Board> SelectAllBoards()
        {
            string command = $"select * from {BoardsTableName}";
            List<BoardsDTO> list = Select(command).Cast<BoardsDTO>().ToList();
            return convertDALlistToBL(list);
        }
        public List<string> SelectAllBoardUsers(string creatorEmail,int boardId)
        {
            string command = $"select * from {BoardsTableName} where boardId = {boardId} and email = '{creatorEmail}'";
            List<BoardsDTO> list = Select(command).Cast<BoardsDTO>().ToList();
            List<string> usersList = new List<string>();
            if (list.Count != 0)
            {
                usersList = list[0].UsersEmail.Split(',').ToList();
            }
            return usersList ;
        }
        
        public List<Board> SelectAllBoardsByEmail(string email)
        {
            string command = $"select * from {BoardsTableName} where email = {email}";
            List<BoardsDTO> list = Select(command).Cast<BoardsDTO>().ToList();
            return convertDALlistToBL(list);
        }


//-------------------------------------------Insert & Delete methods-------------------------------------------------------------------------------

        public bool Insert(Board board)
        {
            string boardUsers = string.Join(",", board.boardUsers);
            BoardsDTO boardsDTO = new BoardsDTO(board.creatorEmail, board.id, board.name, board.taskId, boardUsers);
            using (var connection = new SQLiteConnection(_connectionString))
            {
                int res = -1;
                SQLiteCommand command = new SQLiteCommand(null, connection);
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {BoardsTableName} ({DTO.EmailColumnName} ,{BoardsDTO.BoardIdColumnName},{BoardsDTO.BoardNameColumnName},{BoardsDTO.TaskIdColumnName},{BoardsDTO.UsersEmailColumnName}) " +
                        $"VALUES ('{boardsDTO.Email}',{boardsDTO.BoardId},'{boardsDTO.BoardName}',{boardsDTO.TaskId},'{boardsDTO.UsersEmail}');";

                    /*
                    SQLiteParameter emailParam = new SQLiteParameter(@"emailVal", boardsDTO.Email);
                    SQLiteParameter boardIdParam = new SQLiteParameter(@"boardIdVal", boardsDTO.BoardId);
                    SQLiteParameter boardNameParam = new SQLiteParameter(@"boardNameVal", boardsDTO.BoardName);
                    SQLiteParameter taskIdParam = new SQLiteParameter(@"taskIdVal", boardsDTO.TaskId);
                    SQLiteParameter usersEmailParam = new SQLiteParameter(@"usersEmailVal", boardsDTO.UsersEmail);

                    command.Parameters.Add(emailParam);
                    command.Parameters.Add(boardIdParam);
                    command.Parameters.Add(boardNameParam);
                    command.Parameters.Add(taskIdParam);
                    command.Parameters.Add(usersEmailParam);
                    */
                    command.Prepare();

                    res = command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw new ArgumentException("insertion to database failed: -insert Board-");
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
                return res > 0;
            }
        }
        public bool DeleteBoard(Board board)
        {
            string boardUsers = string.Join(",", board.boardUsers);
            BoardsDTO boardsDTO = new BoardsDTO(board.creatorEmail, board.id, board.name, board.taskId, boardUsers);
            DTask dTask = new DTask();
            dTask.DeleteBoardTasks(boardsDTO);
            return DeleteWithBoardId(boardsDTO);
        }
        public void DeleteAllBoards()
        {
            DeleteAll(BoardsTableName);
            DeleteAll("Column");
            DeleteAll("Tasks");
        }

        

//-------------------------------------------Convert methods-------------------------------------------------------------------------------
        protected override DTO ConvertReaderToObject(SQLiteDataReader reader)
        {
            string email = reader.GetString(0);
            int boardid = reader.GetInt32(1);
            string boardName = reader.GetString(2);
            int taskId = reader.GetInt32(3);
            string usersEmail = reader.GetString(4);
            BoardsDTO result = new BoardsDTO(email, boardid,boardName,taskId,usersEmail);
            return result;

        }
        private List<Board> convertDALlistToBL(List<BoardsDTO> list)
        {
            List<Board> BLlist = new List<Board>();
            foreach (BoardsDTO boards in list)
            {
                BLlist.Add(boards.convertToBLBoard());
            }
            return BLlist;
        }
        
    }
}
