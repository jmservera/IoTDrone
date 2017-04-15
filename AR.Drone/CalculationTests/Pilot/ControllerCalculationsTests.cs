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
            var d = c.GetDistance(new Size(50, 150));
            Assert.AreEqual(200d, d);

            d = c.GetDistance(new Size(150, 50));
            Assert.AreEqual(200d, d);

            d = c.GetDistance(new Size(150, 150));
            Assert.AreEqual(200d, d);

            d = c.GetDistance(new Size(250, 150));
            Assert.AreEqual(100d, d);

            d = c.GetDistance(new Size(400, 150));
            Assert.AreEqual(50d, d);
            d = c.GetDistance(new Size(100, 100));
            Assert.AreEqual(300d, d);
        }

        [TestMethod()]
        public void GetDistanceTest1080()
        {
            var c = new ControllerCalculations(new Size(1920, 1080));
            var d = c.GetDistance(new Size(50, 337));
            Assert.IsTrue((199 < d && d < 201));

            d = c.GetDistance(new Size(337, 50));
            Assert.IsTrue((199 < d && d < 201));

            d = c.GetDistance(new Size(337, 337));
            Assert.IsTrue((199 < d && d < 201));

            d = c.GetDistance(new Size(562, 150));
            Assert.IsTrue((99 < d && d < 101));

            d = c.GetDistance(new Size(900, 150));
            Assert.IsTrue((49 < d && d < 51));

            d = c.GetDistance(new Size(225, 100));
            Assert.IsTrue((299 < d && d < 301));

        }

        [TestMethod()]
        public void GetDiffTest()
        {
            var c = new ControllerCalculations(new Size(640, 480));
            var diff = c.GetDiff(200);
            Assert.AreEqual(25, diff);

            diff = c.GetDiff(100);
            Assert.AreEqual(15, diff);

            diff = c.GetDiff(50);
            Assert.AreEqual(5, diff);
        }
    }
}