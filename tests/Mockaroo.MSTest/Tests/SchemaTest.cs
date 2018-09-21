using Acklann.Mockaroo.Fakes;
using Acklann.Mockaroo.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;

namespace Acklann.Mockaroo.Tests
{
    [TestClass]
    [DeploymentItem(Data.DirectoryName)]
    public class SchemaTest
    {
        [TestMethod]
        public void Can_reassign_a_schema_filed()
        {
            string join(params string[] args) => string.Join('.', args);

            // Arrange
            var s2 = new Schema<BasicObject>();
            var s1 = new Schema<CompositeObject>();

            // Act
            /// case 1: The property is on the 1st level.
            s1.Reassign(x => x.Id, new RowNumberField());
            var case1 = join(nameof(CompositeObject.Id));

            /// case 2: The property is two levels down the member tree.
            s1.Reassign(x => x.Basic.NumericValue, DataType.SSN);
            var case2 = join(nameof(CompositeObject.Basic), nameof(BasicObject.NumericValue));

            /// case 3: The property is a list of objects.
            s2.Reassign(x => x.NumericArray, DataType.Latitude);
            var case3 = join(nameof(BasicObject.NumericArray), "_item_");

            /// case 4: The property is a list of objects.
            s1.Reassign(x => x.Collection1[0].StringValue, DataType.AppName);
            var case4 = join(nameof(CompositeObject.Collection1), "_item_", nameof(BasicObject.StringValue));

            // Assert
            s1[case1].Type.ShouldBe(DataType.RowNumber);
            s1[case2].Type.ShouldBe(DataType.SSN);
            s2[case3].Type.ShouldBe(DataType.Latitude);
            s1[case4].Type.ShouldBe(DataType.AppName);
        }
    }
}