using LookScoreCommon.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LookScoreCommon.UnitTests
{
    [TestClass]
    public class DateUtilTests
    {
        [TestMethod]
        public void Should_Return_Difference_Minutes_Between_Two_Dates()
        {
            DateTime start = DateTime.Now;
            DateTime end = start.AddMinutes(2);

            double expected = (end - start).TotalMinutes;

            Assert.AreEqual(expected, DateUtil.DifferenceInMinutes(start, end));
        }

        [TestMethod]
        public void Should_Return_Difference_Seconds_Between_Two_Dates()
        {
            DateTime start = DateTime.Now;
            DateTime end = start.AddSeconds(125);

            double expected = (end - start).TotalSeconds;
            Console.Write(expected);

            Assert.AreEqual(expected, DateUtil.DifferenceInSeconds(start, end));
        }

        [TestMethod]
        public void Should_Return_Rounded_Difference_Minutes_Between_Two_Dates()
        {
            DateTime start = DateTime.Now;
            DateTime end = start.AddMinutes(2).AddSeconds(20);

            double expected = Math.Floor((end - start).TotalMinutes);
            Console.Write(expected);

            Assert.AreEqual(expected, DateUtil.DifferenceInMinutes(start, end));
        }
    }
}
