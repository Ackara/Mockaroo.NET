using Acklann.Diffa;
using Acklann.Mockaroo.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;

namespace Acklann.Mockaroo.Tests
{
    [TestClass]
    public class DateFieldTest
    {
        private const string FORMAT = "dddd, dd MMMM yyyy hh:mm:ss tt";

        [DataTestMethod]
        [DataRow("2019-3-3")]
        public void Can_create_various_date_ranges(string date)
        {
            // Arrange

            var anchor = DateTime.Parse(date);
            dynamic result = new System.Dynamic.ExpandoObject();
            string format(DateField d) => $"{d.Min.ToString(FORMAT)} => {d.Max.ToString(FORMAT)}";

            // Act
            result.Input = date;

            var today = DateField.ForToday("today", anchor);
            result.Today = format(today);

            var week = DateField.ForThisWeek("week", anchor);
            result.ThisWeek = format(week);
            
            var month = DateField.ForThisMonth("month", anchor);
            result.ThisMonth = format(month);
            
            var year = DateField.ForThisYear("year", anchor);
            result.ThisYear = format(year);

            var json = JsonConvert.SerializeObject(result, Formatting.Indented);

            // Assert
            Diff.Approve(json, ".json", date);
        }
    }
}