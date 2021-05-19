using IntroSE.Kanban.Backend.DataAccessLayer.DalObjects;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public class DTask : DalController
    {
        private const string TaskTableName = "Tasks";

        public DTask() : base(TaskTableName)
        {   
        }

        public List<TaskDTO> SelectAllTaskByEmailAndColumn(string email, int column)
        {
                List<TaskDTO> results = new List<TaskDTO>();
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    SQLiteCommand command = new SQLiteCommand(null, connection);
                    command.CommandText = $"select * from {TaskTableName} WHERE email = @emailVal and column = @columnVal;";
                    
                    SQLiteDataReader dataReader = null;
                    try
                    {

                        command.Parameters.Add(new SQLiteParameter(@"emailVal", email));
                        command.Parameters.Add(new SQLiteParameter(@"columnVal", column));
                        connection.Open();
                        dataReader = command.ExecuteReader();
                        while (dataReader.Read())
                            results.Add((TaskDTO)ConvertReaderToObject(dataReader));
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

        public bool Update(string email, int boardId, int taskId, string attributeName, string attributeValue)
        {
            int res = -1;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"update {TaskTableName} set [{attributeName}]=@attVal where email=@emailVal and boardId =@boardIdVal and taskId =@taskIdVal"
                };
                try
                {


                    command.Parameters.Add(new SQLiteParameter(@"attVal", attributeValue));
                    command.Parameters.Add(new SQLiteParameter(@"boardIdVal", boardId));
                    command.Parameters.Add(new SQLiteParameter(@"emailVal", email));
                    command.Parameters.Add(new SQLiteParameter(@"taskIdVal", taskId));

                    connection.Open();
                    res = command.ExecuteNonQuery();
                }
                catch
                {
                    //log
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }

            }
            return res > 0;
        }
        public bool Update(string email, int boardId, int taskId, string attributeName, int attributeValue)
        {
            int res = -1;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"update {TaskTableName} set [{attributeName}]=@attVal where email=@emailVal and boardId =@boardIdVal and taskId =@taskIdVal"
                };
                try
                {


                    command.Parameters.Add(new SQLiteParameter(@"attVal", attributeValue));
                    command.Parameters.Add(new SQLiteParameter(@"boardIdVal", boardId));
                    command.Parameters.Add(new SQLiteParameter(@"emailVal", email));
                    command.Parameters.Add(new SQLiteParameter(@"taskIdVal", taskId));

                    //command.Parameters.Add(new SQLiteParameter(attributeName, attributeValue));
                    connection.Open();
                    res = command.ExecuteNonQuery();
                }
                catch
                {
                    //log
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }

            }
            return res > 0;
        }
        public List<TaskDTO> getMyAssignments(string Email)
        {
            string command = $"SELECT * FROM Tasks WHERE assignee = '{Email}'";
            List<TaskDTO> result = Select(command).Cast<TaskDTO>().ToList();

            return result;
        }

        /*

        public List<ForumDTO> SelectAllForums()
        {
            List<ForumDTO> result = Select().Cast<ForumDTO>().ToList();

            return result;
        }*/



        public bool Insert(TaskDTO task)
        {

            using (var connection = new SQLiteConnection(_connectionString))
            {
                int res = -1;
                SQLiteCommand command = new SQLiteCommand(null, connection);
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {TaskTableName} ({TaskDTO.EmailColumnName} ,{TaskDTO.BoardIdColumnName}, {TaskDTO.TaskIdColumnName},{TaskDTO.AssigneeColumnName},{TaskDTO.ColumnColumnName},{TaskDTO.CreationTimeColumnName},{TaskDTO.DescriptionColumnName},{TaskDTO.TitleColumnName},{TaskDTO.DueDateColumnName}) " +
                        $"VALUES (@taskEmail,@boardId,@taskId,@assignee,@column,@creationTime,@description,@title,@dueDate); ";

                    SQLiteParameter newEmail = new SQLiteParameter(@"taskEmail", task.Email);
                    SQLiteParameter newBoardId = new SQLiteParameter(@"boardId", task.BoardId);
                    SQLiteParameter newTaskId = new SQLiteParameter(@"taskId", task.TaskId);
                    SQLiteParameter newAssignee = new SQLiteParameter(@"assignee", task.Assignee);
                    SQLiteParameter newColumn = new SQLiteParameter(@"column", task.Column);
                    SQLiteParameter newCreationTime = new SQLiteParameter(@"creationTime", task.CreationTime);
                    SQLiteParameter newDescrption = new SQLiteParameter(@"description", task.Description);
                    SQLiteParameter newTitle = new SQLiteParameter(@"title", task.Title);
                    SQLiteParameter newDueDate = new SQLiteParameter(@"dueDate", task.DueDate);

                    command.Parameters.Add(newEmail);
                    command.Parameters.Add(newBoardId);    
                    command.Parameters.Add(newTaskId);
                    command.Parameters.Add(newAssignee);
                    command.Parameters.Add(newColumn);
                    command.Parameters.Add(newCreationTime);
                    command.Parameters.Add(newDescrption);
                    command.Parameters.Add(newTitle);
                    command.Parameters.Add(newDueDate);

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
        public bool DeleteBoardTasks(BoardsDTO boardsDTO)
        {
            return DeleteWithBoardId(boardsDTO);
        }

        protected override DTO ConvertReaderToObject(SQLiteDataReader reader)
        {
            TaskDTO result = new TaskDTO(reader.GetString(0),reader.GetInt32(1),reader.GetInt32(2),reader.GetString(3),reader.GetInt32(4),
                                         reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8));
            return result;

        }
        public List<introSE.KanbanBoard.Backend.BuisnessLayer.Task> convertTasksToBL(List<TaskDTO> taskDTOs)
        {
            List<introSE.KanbanBoard.Backend.BuisnessLayer.Task> TaskList = new List<introSE.KanbanBoard.Backend.BuisnessLayer.Task>();
            foreach (TaskDTO task in taskDTOs)
            {
                TaskList.Add(task.convertToBLTask());
            }
            return TaskList;
        }
        
       
    }
}
