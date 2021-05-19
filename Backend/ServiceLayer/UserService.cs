﻿using introSE.KanbanBoard.Backend.BuisnessLayer;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
//using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class UserService
    {
        public UserController userController;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        public UserService()
        {
            userController = new UserController();

            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
            log.Info("Starting Log!");
        }

        ///<summary>This method loads the data from the persistance.
        ///         You should call this function when the program starts. </summary>
        public Response LoadData()
        {
            try
            {
                userController.pullAllUsers();
                log.Info("All users loaded.");
                return new Response();
            }catch(Exception e)
            {
                return new Response(e.Message);
            }
        }
        ///<summary>Removes all persistent data.</summary>
        public Response DeleteData()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Log in an existing user
        /// </summary>
        /// <param name="userEmail">The email address of the user to login</param>
        /// <param name="password">The password of the user to login</param>
        /// <returns>A response object with a value set to the user, instead the response should contain a error message in case of an error</returns>
        public Response<User> Login(string userEmail, string password)
        {
            try
            {
                var user = userController.login(userEmail, password);
                if (user == null)
                    throw new ArgumentException("User doesnt exists!");
                log.Info("User logged in successfully");
                return Response<User>.FromValue(new User(userEmail));
            }
            catch (Exception e)
            {
                log.Error("Something went wrong during login attempt");
                return Response<User>.FromError(e.Message);
            }
        }
        /// <summary>        
        /// Log out an logged-in user. 
        /// </summary>
        /// <param name="userEmail">The email of the user to log out</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response Logout(string userEmail)
        {
            try
            {
                var user = userController.getUser(userEmail);
                if (user == null)
                    throw new ArgumentException("User doesnt exists!");
                user.logout();
                log.Info("User logged out successfully");
                return new Response();
            }
            catch (Exception e)
            {
                log.Error("An error occurd during logout");
                return new Response(e.Message);
            }
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="userEmail">The email address of the user to register</param>
        /// <param name="password">The password of the user to register</param>
        /// <returns>A response object. The response should contain a error message in case of an error<returns>
        public Response Register(string userEmail, string password)
        {
            try
            {
                userController.register(userEmail, password);
                log.Info("User registered successfully");
                return new Response();
            }
            catch (Exception e)
            {

                return new Response(e.Message);
            }
        }

        /// <summary>
        /// Adds a board created by another user to the logged-in user. 
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the new board</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response JoinBoard(string userEmail, string creatorEmail, string boardName)
        {
            try
            {
                var user = userController.getUser(userEmail);
                var creatorUser = userController.getUser(creatorEmail);
                if (user == null  | creatorUser == null) { throw new ArgumentException("User doesnt exists!"); }
                if (user.login)
                {
                    user.joinBoard(creatorUser, boardName);
                    log.Info($"User joined {boardName} successfully");
                    return new Response();
                }
                else
                    throw new ArgumentException("You must be logged in");
            }
            catch(Exception e)
            {
                log.Error("Something went wrong during board join.");
                return new Response(e.Message);
            }
        }

        public Response<IList<Task>> InProgressTasks(string userEmail)
        {
            try
            {
                var user = userController.getUser(userEmail);
                if (user == null) { throw new ArgumentException("User doesnt exists!"); }
                if (user.login)
                {
                    var toConvertProgTasksList = user.getAllInProgressTasks();
                    IList<Task> inProgList = new List<Task>();
                    foreach(var task in toConvertProgTasksList)
                    {
                        inProgList.Add(new Task(task.taskId, task.getCreationTime(), task.getTitle(), task.getDescription(), task.getDueTime()));
                    }
                    log.Info("In Progress Tasks fetched successfully.");
                    return Response<IList<Task>>.FromValue(inProgList);
                }
                else
                    throw new ArgumentException("You must be logged in");
            }
            catch (Exception e)
            {
                log.Error("InProgress tasks couldnt be fetched");
                return Response<IList<Task>>.FromError(e.Message);
            }
        }

        /*
        public Response Register(string email, string password)
        {
            
            try
            {
                userController.register(email, password);
                log.Info("User registered successfully");
                return new Response();

            } catch(Exception e)
            {
                
                return new Response(e.Message);
            }
            
        }
        /// <summary>
        /// Log in an existing user
        /// </summary>
        /// <param name="email">The email address of the user to login</param>
        /// <param name="password">The password of the user to login</param>
        /// <returns>A response object with a value set to the user, instead the response should contain a error message in case of an error</returns>
        public Response<User> Login(string email, string password)
        {
            try
            {
                var user = userController.login(email, password);
                if (user == null)
                    throw new ArgumentException("User doesnt exists!");

                log.Info("User logged in successfully");
                return Response<User>.FromValue(new User(email));
            } catch(Exception e)
            {
                log.Error("Something went wrong during login attempt");
                return Response<User>.FromError(e.Message);
            }
        }
        /// <summary>        
        /// Log out an logged in user. 
        /// </summary>
        /// <param name="email">The email of the user to log out</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response Logout(string email)
        {
            try
            {   
                var user = userController.getUser(email);
                if (user == null)
                    throw new ArgumentException("User doesnt exists!");

                user.logout();
                log.Info("User logged out successfully");
                return new Response();
            }catch(Exception e)
            {
                log.Error("An error occurd during logout");
                return new Response(e.Message);
            }
        }
        private void ValidateUserLoggin(string email)
        {

            throw new NotImplementedException();
        }


    }*/

    }

}
