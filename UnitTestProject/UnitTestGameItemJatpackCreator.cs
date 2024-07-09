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
    public class UnitTestGameItemJatpackCreator
    {
        [TestMethod]
        public void GameItemJatpackCreator1()
        {
            GameItemJatpackCreator creator = new GameItemJatpackCreator();
            var expected = "Course_work.GameItemJatpack";
            var actual = creator.Create(1, 1, new Bitmap(1, 1), new Size(1, 1));
            Assert.AreEqual(expected, actual.GetType().ToString());
        }
        [TestMethod]
        public void GameItemJatpackCreator2()
        {
            GameItemJatpackCreator creator = new GameItemJatpackCreator();
            var expected = new Point(1360, 60 + 50);
            var actual = creator.Create(1, 1, new Bitmap(1, 1), new Size(1, 1));
            Assert.AreEqual(expected, actual.Location);
        }
        [TestMethod]
        public void GameItemJatpackCreator3()
        {
            GameItemJatpackCreator creator = new GameItemJatpackCreator();
            var expected = new Bitmap(1, 1);
            var actual = creator.Create(1, 1, new Bitmap(1, 1), new Size(1, 1));
            Assert.AreEqual(expected.GetType().ToString(), actual.Image.GetType().ToString());
        }
        [TestMethod]
        public void GameItemJatpackCreator4()
        {
            GameItemJatpackCreator creator = new GameItemJatpackCreator();
            var expected = new Size(1, 1);
            var actual = creator.Create(1, 1, new Bitmap(1, 1), new Size(1, 1));
            Assert.AreEqual(expected, actual.Size);
        }
        [TestMethod]
        public void GameItemJatpackCreator5()
        {
            GameItemJatpackCreator creator = new GameItemJatpackCreator();
            var expected = new Size(1, 1);
            var actual = creator.Create(1, 1, new Bitmap(1, 1), new Size(1, 1));
            Assert.AreEqual(expected.GetType().ToString(), actual.Size.GetType().ToString());
        }
    }
}
