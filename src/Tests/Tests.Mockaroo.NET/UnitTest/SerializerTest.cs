using Gigobyte.Mockaroo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

using System.Text.RegularExpressions;

namespace Tests.Mockaroo.UnitTest
{
    [TestClass]
    public class SerializerTest
    {
        [TestMethod]
        [Owner(Dev.Ackara)]
        public void LoadPersonObject()
        {
            var sample = Person.GetSample();
            string json = sample.ToJson();

            RunTypeLoaderTest<Person>(json, acceptanceCriteria: (x =>
                x.FullName == sample.FullName
                &&
                x.Age == sample.Age
                &&
                x.Dob == sample.Dob
                &&
                x.NetWorth == sample.NetWorth));
        }

        [TestMethod]
        [Owner(Dev.Ackara)]
        public void LoadCharObject()
        {
            var sample = 'a';
            RunTypeLoaderTest<char>(sample, (x => x == sample));
        }

        [TestMethod]
        [Owner(Dev.Ackara)]
        public void LoadStringObject()
        {
            var sample = "writing a unit test.";
            RunTypeLoaderTest<string>(sample, (x => x == sample));
        }

        [TestMethod]
        [Owner(Dev.Ackara)]
        public void LoadBooleanObject()
        {
            var sample = true;
            RunTypeLoaderTest<bool>(sample, (x => x == sample));
        }

        [TestMethod]
        [Owner(Dev.Ackara)]
        public void LoadIntegerObject()
        {
            var sample = 2016;
            RunTypeLoaderTest<int>(sample, (x => x == sample));
        }

        [TestMethod]
        [Owner(Dev.Ackara)]
        public void LoadLongObject()
        {
            var sample = 12L;
            RunTypeLoaderTest<long>(sample, (x => x == sample));
        }

        [TestMethod]
        [Owner(Dev.Ackara)]
        public void LoadFloatObject()
        {
            var sample = 12.3f;
            RunTypeLoaderTest<float>(sample, (x => x == sample));
        }

        [TestMethod]
        [Owner(Dev.Ackara)]
        public void LoadDoubleObject()
        {
            var sample = 12.3d;
            RunTypeLoaderTest<double>(sample, (x => x == sample));
        }

        [TestMethod]
        [Owner(Dev.Ackara)]
        public void LoadDecimalObject()
        {
            var sample = 12.3m;
            RunTypeLoaderTest<decimal>(sample, (x => x == sample));
        }

        [TestMethod]
        [Owner(Dev.Ackara)]
        public void LoadDateTimeObject()
        {
            var sample = new DateTime(2016, 12, 12);
            RunTypeLoaderTest<DateTime>(sample, (x => x.Year == 2016 && x.Month == 12));
        }

        internal void RunTypeLoaderTest(string data, string regexPattern)
        {
            // Arrange
            var sut = new Serializer();

            // Act
            string result = sut.Deserialize<string>(data);
            bool pass = new Regex(regexPattern).IsMatch(result);

            // Assert
            Assert.IsTrue(pass);
        }

        internal void RunTypeLoaderTest<T>(object data, Func<T, bool> acceptanceCriteria)
        {
            // Arrange
            var sut = new Serializer();

            // Act
            T result = sut.Deserialize<T>(data.ToString());

            // Assert
            Assert.IsTrue(acceptanceCriteria(result));
        }
    }
}