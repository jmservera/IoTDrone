using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoPilotApp.Pilot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AutoPilotApp.Pilot.Tests
{
    [TestClass()]
    public class ControllerCalculationsTests
    {
        [TestMethod()]
        public void GetDistanceTest()
        {
            var c = new ControllerCalculations(new Size(640, 480));
            var d = c.GetDistance(new Size(180, 150));
            Assert.AreEqual(100d, d);

            d = c.GetDistance(new Size(150, 180));
            Assert.AreEqual(100d, d);


            d = c.GetDistance(new Size(75, 75));
            Assert.AreEqual(300d, d);
        }

        [TestMethod()]
        public void GetDistanceTest1080()
        {
            var c = new ControllerCalculations(new Size(1920, 1080));

            var d = c.GetDistance(new Size(500, 150));
            Assert.IsTrue((19 < d && d < 21));
        }

        [TestMethod()]
        public void GetDiffTest()
        {
            var c = new ControllerCalculations(new Size(640, 480));
            var diff = c.GetDiff(200);
            Assert.AreEqual(50, diff);

            diff = c.GetDiff(100);
            Assert.AreEqual(30, diff);

            diff = c.GetDiff(50);
            Assert.AreEqual(20, diff);
        }
    }
}