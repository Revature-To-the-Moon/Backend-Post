using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Models;

namespace Tests
{
    public class ModelTest
    {

        //Root tests
        [Fact]
        public void RootShouldCreate()
        {
            Root test = new Root();
            Assert.NotNull(test);
        }
        [Fact]
        public void RootShouldSetValidData()
        {
            //Arrange
            Root test = new Root();
            string testMessage = "test element";

            //Act
            test.Message = testMessage;

            //Assert
            Assert.Equal(testMessage, test.Message);
        }

        //Comment tests
        [Fact]
        public void CommentShouldCreate()
        {
            Comment test = new Comment();
            Assert.NotNull(test);
        }
        [Fact]
        public void CommentShouldSetValidData()
        {
            //Arrange
            Comment test = new Comment();
            string testMessage = "test element";

            //Act
            test.Message = testMessage;

            //Assert
            Assert.Equal(testMessage, test.Message);
        }

        //Vote tests
        [Fact]
        public void VoteShouldCreate()
        {
            Vote test = new Vote();
            Assert.NotNull(test);
        }
        [Fact]
        public void VoteShouldSetValidData()
        {
            //Arrange
            Vote test = new Vote();
            int testValue = 1;

            //Act
            test.Value = testValue;

            //Assert
            Assert.Equal(testValue, test.Value);
        }
    }
}
