using Microsoft.VisualStudio.TestTools.UnitTesting;
using Coding_Tracker;
using System.Runtime.CompilerServices;

namespace Coding_Tracker.Tests
{
    [TestClass()]
    public class ValidationTest
    {
        [TestMethod]
        [DataRow("a", false)]
        [DataRow("9", false)]
        [DataRow("1", true)]
        public void isVailableCom_ShouldShouldReturnExpected_Result(string com, bool expected)
        {
            var result = Validation.isVailableCom(com);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [DataRow("01-01", false)]
        [DataRow("01", false)]
        [DataRow("2001", false)]
        [DataRow("s", false)]
        [DataRow("01-01-01", true)]
        public void isVailableDate_ShouldShouldReturnExpected_Result(string date, bool expected)
        {
            var result = Validation.isVailableDate(date);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [DataRow("a", false)]
        [DataRow("5:00", false)]
        [DataRow("05:00", true)]
        public void isVailableTime_ShouldShouldReturnExpected_Result(string time, bool expected)
        {
            var result = Validation.isVailableTime(time);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [DataRow("05:00", "12:00", true)]
        [DataRow("15:00", "12:00", false)]
        public void TimeSafe_ShouldReturnExpected_Result(string startTime, string endTime, bool expected)
        {
            var result = Validation.TimeSafe(startTime, endTime);

            Assert.AreEqual(expected, result);
        }
    }
}

