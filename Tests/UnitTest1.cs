using NUnit.Framework;
using System;
using introSE.KanbanBoard.Backend.BuisnessLayer;

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
                Assert.Fail("failed to change title");

            }
        }

        public void setTitleFailed()
        {
            try
            {
                // arrange 
                string newTitle = "";
                // act
                task.setTitle(newTitle);
                // assert
                Assert.AreEqual(newTitle, task.getTitle(), "The title changed");
            }

            catch (Exception)
            {
                Assert.Fail("Empty title is not legal");
            }
        }

        public void setTitleFailed2()
        {
            try
            {
                // arrange 
                string newTitle = "i need to create a task with more then 50 letters so i write a lot of words. now i will count the number of letters i already wrote and" +
                    "will hope its more then 50";
                // act
                task.setTitle(newTitle);
                // assert
                Assert.AreEqual(newTitle, task.getTitle(), "The title changed");
            }

            catch (Exception)
            {
                Assert.Fail("more then 50 letters is not legal");
            }
        }
        public void setDueTimeSuccess()
        {
            try
            {
                // arrange 
                DateTime newDateTime = new DateTime(2021, 11, 11);
                // act
                task.setDueTime(newDateTime);
                // assert
                Assert.AreEqual(newDateTime, task.getDueTime(), "The date changed");
            }

            catch (Exception)
            {
                Assert.Fail("failed to change duetime");
            }
        }

        public void setDueTimeFailed()
        {
            try
            {
                // arrange 
                DateTime newDateTime = new DateTime(2020, 11, 11);
                // act
                task.setDueTime(newDateTime);
                // assert
                Assert.AreEqual(newDateTime, task.getDueTime(), "The date changed");
            }

            catch (Exception)
            {
                Assert.Fail("this date already past");
            }
        }

        public void setDescriptionSuccess()
        {
            try
            {
                // arrange 
                string newDescription = "new description";
                // act
                task.setDescription(newDescription);
                // assert
                Assert.AreEqual(newDescription, task.getDescription(), "The description changed");
            }

            catch (Exception)
            {
                Assert.Fail("failed to change description");
            }
        }

        public void setDescriptionFailed()
        {
            try
            {
                // arrange 
                string newDescription = null;
                // act
                task.setDescription(newDescription);
                // assert
                Assert.AreEqual(newDescription, task.getDescription(), "The description changed");
            }

            catch (Exception)
            {
                Assert.Fail("null description is not ok");
            }
        }
    }
}