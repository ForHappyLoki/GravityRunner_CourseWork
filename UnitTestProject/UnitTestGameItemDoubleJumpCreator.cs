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
    public class UnitTestGameItemDoubleJumpCreator
    {
        [TestMethod]
        public void UnitTestGameItemDoubleJumpCreator1()
        {
            GameItemDoubleJumpCreator creator = new GameItemDoubleJumpCreator();
            var expected = "Course_work.GameItemDoubleJump";
            var actual = creator.Create(1, 1, new Bitmap(1, 1), new Size(1, 1));
            Assert.AreEqual(expected, actual.GetType().ToString());
        }
        [TestMethod]
        public void UnitTestGameItemDoubleJumpCreator2()
        {
            GameItemDoubleJumpCreator creator = new GameItemDoubleJumpCreator();
            var expected = new Point(1360, 60 + 50);
            var actual = creator.Create(1, 1, new Bitmap(1, 1), new Size(1, 1));
            Assert.AreEqual(expected, actual.Location);
        }
        [TestMethod]
        public void UnitTestGameItemDoubleJumpCreator3()
        {
            GameItemDoubleJumpCreator creator = new GameItemDoubleJumpCreator();
            var expected = new Bitmap(1, 1);
            var actual = creator.Create(1, 1, new Bitmap(1, 1), new Size(1, 1));
            Assert.AreEqual(expected.GetType().ToString(), actual.Image.GetType().ToString());
        }
        [TestMethod]
        public void UnitTestGameItemDoubleJumpCreator4()
        {
            GameItemDoubleJumpCreator creator = new GameItemDoubleJumpCreator();
            var expected = new Size(1, 1);
            var actual = creator.Create(1, 1, new Bitmap(1, 1), new Size(1, 1));
            Assert.AreEqual(expected, actual.Size);
        }
        [TestMethod]
        public void UnitTestGameItemDoubleJumpCreator5()
        {
            GameItemDoubleJumpCreator creator = new GameItemDoubleJumpCreator();
            var expected = new Size(1, 1);
            var actual = creator.Create(1, 1, new Bitmap(1, 1), new Size(1, 1));
            Assert.AreEqual(expected.GetType().ToString(), actual.Size.GetType().ToString());
        }
    }
}
