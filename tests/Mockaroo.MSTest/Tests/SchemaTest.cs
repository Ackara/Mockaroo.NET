using Acklann.Diffa;
using Acklann.Mockaroo;
using Acklann.Mockaroo.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;
using System.Text;
using Acklann.Mockaroo.Constants;

namespace Acklann.Mockaroo.Tests
{
    [TestClass]
    [DeploymentItem(Data.DirectoryName)]
    public class SchemaTest
    {
        [TestMethod]
        public void Assign_should_override_a_field_instance_within_a_schema_when_invoked()
        {
            // Arrange
            var sut = new Schema<Message>();

            // Act

            /// case 1: The property is one level down the member tree.
            sut.Assign(x => x.Text, DataType.FullName);
            var newTextType = sut[0].Type;

            /// case 2: The property is two levels down the member tree.
            sut.Assign(x => x.Writer.Name, DataType.FullName);
            var newNameType = sut[2].Type;

            /// case 3: The property is a list of objects.
            sut.Assign(x => x.Tags, DataType.AppName);
            var newTagType = sut[5].Type;

            /// case 4: The property is a list of objects.
            sut.Assign(x => x.Writer.Reviews.Item().Rating, DataType.RowNumber);
            var newRatingType = sut[4].Type;

            // Assert
            newTextType.ShouldBe(DataType.FullName);
            newNameType.ShouldBe(DataType.FullName);
            newTagType.ShouldBe(DataType.AppName);
            newRatingType.ShouldBe(DataType.RowNumber);
        }

        #region Samples

        

        public class Message
        {
            public string Text { get; set; }

            public Author Writer { get; set; }

            public string[] Tags { get; set; }
        }

        public class Author
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public Review[] Reviews { get; set; }
        }

        public class Review
        {
            public int Rating { get; set; }
        }

       

        #endregion Samples
    }
}