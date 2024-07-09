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
    public class UnitTestGameItemHugeLeapCreator
    {
        [TestMethod]
        public void UnitTestGameItemHugeLeapCreator1()
        {
            GameItemHugeLeapCreator creator = new GameItemHugeLeapCreator();
            var expected = "Course_work.GameItemHugeLeap";
            var actual = creator.Create(1, 1, new Bitmap(1, 1), new Size(1, 1));
            Assert.AreEqual(expected, actual.GetType().ToString());
        }
        [TestMethod]
        public void UnitTestGameItemHugeLeapCreator2()
        {
            GameItemHugeLeapCreator creator = new GameItemHugeLeapCreator();
            var expected = new Point(1360, 60 + 50);
            var actual = creator.Create(1, 1, new Bitmap(1, 1), new Size(1, 1));
            Assert.AreEqual(expected, actual.Location);
        }
        [TestMethod]
        public void UnitTestGameItemHugeLeapCreator3()
        {
            GameItemHugeLeapCreator creator = new GameItemHugeLeapCreator();
            var expected = new Bitmap(1, 1);
            var actual = creator.Create(1, 1, new Bitmap(1, 1), new Size(1, 1));
            Assert.AreEqual(expected.GetType().ToString(), actual.Image.GetType().ToString());
        }
        [TestMethod]
        public void UnitTestGameItemHugeLeapCreator4()
        {
            GameItemHugeLeapCreator creator = new GameItemHugeLeapCreator();
            var expected = new Size(1, 1);
            var actual = creator.Create(1, 1, new Bitmap(1, 1), new Size(1, 1));
            Assert.AreEqual(expected, actual.Size);
        }
        [TestMethod]
        public void UnitTestGameItemHugeLeapCreator5()
        {
            GameItemHugeLeapCreator creator = new GameItemHugeLeapCreator();
            var expected = new Size(1, 1);
            var actual = creator.Create(1, 1, new Bitmap(1, 1), new Size(1, 1));
            Assert.AreEqual(expected.GetType().ToString(), actual.Size.GetType().ToString());
        }
    }
}
