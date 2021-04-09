using introSE.KanbanBoard.Backend.BuisnessLayer;
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
        UserController userController;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        
        public UserService()
        {
            userController = new UserController();

            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
            log.Info("Starting Log!");
        }

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
                //var user = new introSE.KanbanBoard.Backend.BuisnessLayer.User(email, password);
                var user = userController.login(email, password);
                return Response<User>.FromValue(new User(email));
            } catch(Exception e)
            {
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
                user.logout();
                return new Response();
            }catch(Exception e)
            {
                return new Response(e.Message);
            }
        }
        private void ValidateUserLoggin(string email)
        {

            throw new NotImplementedException();
        }


    }

    

}
