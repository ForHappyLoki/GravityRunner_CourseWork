using Course_work;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTestPlatformCreator
    {
        [TestMethod]
        public void TestMethodPlatformCreator1()
        {
            PlatformCreator creator = new PlatformCreator();
            var expected = "Course_work.Platform";
            var actual = creator.Create(1, 1, new Bitmap(1, 1), new Size(1, 1));
            Assert.AreEqual(expected, actual.GetType().ToString());
        }
        [TestMethod]
        public void TestMethodPlatformCreator2()
        {
            PlatformCreator creator = new PlatformCreator();
            var expected = new Point(1360, 60 + 50);
            var actual = creator.Create(1, 1, new Bitmap(1, 1), new Size(1, 1));
            Assert.AreEqual(expected, actual.Location);
        }
        [TestMethod]
        public void TestMethodPlatformCreator3()
        {
            PlatformCreator creator = new PlatformCreator();
            var expected = new Bitmap(1, 1);
            var actual = creator.Create(1, 1, new Bitmap(1, 1), new Size(1, 1));
            Assert.AreEqual(expected.GetType().ToString(), actual.Image.GetType().ToString());
        }
        [TestMethod]
        public void TestMethodPlatformCreator4()
        {
            PlatformCreator creator = new PlatformCreator();
            var expected = new Size(1, 1);
            var actual = creator.Create(1, 1, new Bitmap(1, 1), new Size(1, 1));
            Assert.AreEqual(expected, actual.Size);
        }
        [TestMethod]
        public void TestMethodPlatformCreator5()
        {
            PlatformCreator creator = new PlatformCreator();
            var expected = new Size(1, 1);
            var actual = creator.Create(1, 1, new Bitmap(1, 1), new Size(1, 1));
            Assert.AreEqual(expected.GetType().ToString(), actual.Size.GetType().ToString());
        }
    }
}
