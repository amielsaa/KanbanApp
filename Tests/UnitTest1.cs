using NUnit.Framework;
using IntroSE.Kanban.Backend.BuisnessLayer;
using System;
using IntroSE.Kanban.Backend.ServiceLayer;
using introSE.KanbanBoard.Backend.BuisnessLayer;
using IntroSE.Kanban.Backend;

namespace Tests
{
    public class Tests
    {
        private Board board;
        private introSE.KanbanBoard.Backend.BuisnessLayer.Task task;
        [SetUp]
        public void Setup()
        {
            board = new Board("Board 1", "ido hamelech", 1);
            task = new introSE.KanbanBoard.Backend.BuisnessLayer.Task(new DateTime(2021, 12, 12), "Work", "the tests", 1, "ido", "shapiraido123@gmail.com", 1);
        }

        [Test]
        public void setTitleSuccess()
        {
            try
            {
                // arrange 
                string newTitle = "newTitle";
                // act
                task.setTitle(newTitle);
                // assert
                Assert.AreEqual(newTitle, task.getTitle(), "The title changed");
            }

            catch (Exception)
            {

            }
        }
    }
}