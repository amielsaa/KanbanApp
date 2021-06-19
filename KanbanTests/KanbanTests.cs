using NUnit.Framework;
using IntroSE.Kanban.Backend.BuisnessLayer;
using System;

namespace KanbanTests
{
    public class Tests
    {

       private UserService userService;
       private UserController userController;
       private BoardController boardController;
       private User user;
       [SetUp]
       public void Setup ()
        {
             userService = new UserService();
             userController = UserController.getInstance();
             boardController = new BoardController();
             user = new User("shapiraido123@gmail.com", "Madrid123", userController, boardController);
        }
        
        public void PasswordValidateSuccess()
        {
            try
            {
                // arrange 
                string password = "ido1Ido1";
                // act
                string result = user.validatePasswod(password);
                // assert
                Assert.AreEqual(result,true, "good password");
            }

            catch (Exception)
            {

            }

        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}