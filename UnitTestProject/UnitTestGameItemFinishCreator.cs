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
    public class UnitTestGameItemFinishCreator
    {
        [TestMethod]
        public void UnitTestGameItemFinishCreator1()
        {
            GameItemFinishCreator creator = new GameItemFinishCreator();
            var expected = "Course_work.GameItemFinish";
            var actual = creator.Create(1, 1, new Bitmap(1, 1), new Size(1, 1));
            Assert.AreEqual(expected, actual.GetType().ToString());
        }
        [TestMethod]
        public void UnitTestGameItemFinishCreator2()
        {
            GameItemFinishCreator creator = new GameItemFinishCreator();
            var expected = new Point(1360, 60 + 50);
            var actual = creator.Create(1, 1, new Bitmap(1, 1), new Size(1, 1));
            Assert.AreEqual(expected, actual.Location);
        }
        [TestMethod]
        public void UnitTestGameItemFinishCreator3()
        {
            GameItemFinishCreator creator = new GameItemFinishCreator();
            Bitmap expected = null;
            var actual = creator.Create(1, 1, new Bitmap(1, 1), new Size(1, 1));
            Assert.AreEqual(expected, actual.Image);
        }
        [TestMethod]
        public void UnitTestGameItemFinishCreator4()
        {
            GameItemFinishCreator creator = new GameItemFinishCreator();
            var expected = new Size(1, 1);
            var actual = creator.Create(1, 1, new Bitmap(1, 1), new Size(1, 1));
            Assert.AreEqual(expected, actual.Size);
        }
        [TestMethod]
        public void UnitTestGameItemFinishCreator5()
        {
            GameItemFinishCreator creator = new GameItemFinishCreator();
            var expected = new Size(1, 1);
            var actual = creator.Create(1, 1, new Bitmap(1, 1), new Size(1, 1));
            Assert.AreEqual(expected.GetType().ToString(), actual.Size.GetType().ToString());
        }
    }
}
