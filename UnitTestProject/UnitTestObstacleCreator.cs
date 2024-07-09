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
    public class UnitTestObstacleCreator
    {
        [TestMethod]
        public void UnitTestObstacleCreator1()
        {
            ObstacleCreator creator = new ObstacleCreator();
            var expected = "Course_work.Obstacle";
            var actual = creator.Create(1, 1, new Bitmap(1, 1), new Size(1, 1));
            Assert.AreEqual(expected, actual.GetType().ToString());
        }
        [TestMethod]
        public void UnitTestObstacleCreator2()
        {
            ObstacleCreator creator = new ObstacleCreator();
            var expected = new Point(1360, 60 + 50);
            var actual = creator.Create(1, 1, new Bitmap(1, 1), new Size(1, 1));
            Assert.AreEqual(expected, actual.Location);
        }
        [TestMethod]
        public void UnitTestObstacleCreator3()
        {
            ObstacleCreator creator = new ObstacleCreator();
            var expected = new Bitmap(1, 1);
            var actual = creator.Create(1, 1, new Bitmap(1, 1), new Size(1, 1));
            Assert.AreEqual(expected.GetType().ToString(), actual.Image.GetType().ToString());
        }
        [TestMethod]
        public void UnitTestObstacleCreator4()
        {
            ObstacleCreator creator = new ObstacleCreator();
            var expected = new Size(1, 1);
            var actual = creator.Create(1, 1, new Bitmap(1, 1), new Size(1, 1));
            Assert.AreEqual(expected, actual.Size);
        }
        [TestMethod]
        public void UnitTestObstacleCreator5()
        {
            ObstacleCreator creator = new ObstacleCreator();
            var expected = new Size(1, 1);
            var actual = creator.Create(1, 1, new Bitmap(1, 1), new Size(1, 1));
            Assert.AreEqual(expected.GetType().ToString(), actual.Size.GetType().ToString());
        }
    }
}
