using Course_work;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UnitTestProject
{

    [TestClass]
    public class UnitTestFormMainMenu
    {
        [TestMethod]
        public void UnitTestFormMainMenu1()
        {
            FormMainMenu formMainMenu = new FormMainMenu();
            var expected = new FormGameWindow(formMainMenu);
            var actual = formMainMenu;
            Assert.AreEqual(expected.f2.GetType(), actual.GetType());
        }
        [TestMethod]
        public void UnitTestFormMainMenu2()
        {
            FormMainMenu formMainMenu = new FormMainMenu();
            formMainMenu.InitializeMenu();
            var expected = formMainMenu.buttonStart;
            var actual = new Button();
            Assert.AreEqual(expected.GetType(), actual.GetType());
        }
        [TestMethod]
        public void UnitTestFormMainMenu3()
        {
            FormMainMenu formMainMenu = new FormMainMenu();
            formMainMenu.InitializeMenu();
            var expected = formMainMenu.buttonExit;
            var actual = new Button();
            Assert.AreEqual(expected.GetType(), actual.GetType());
        }
        [TestMethod]
        public void UnitTestFormMainMenu4()
        {
            FormMainMenu formMainMenu = new FormMainMenu();
            formMainMenu.InitializeMenu();
            var expected = new Point(550, 250);
            var actual = formMainMenu.buttonStart;
            Assert.AreEqual(expected, actual.Location);
        }
        [TestMethod]
        public void UnitTestFormMainMenu5()
        {
            FormMainMenu formMainMenu = new FormMainMenu();
            formMainMenu.InitializeMenu();
            var expected = new Point(550, 400);
            var actual = formMainMenu.buttonExit;
            Assert.AreEqual(expected, actual.Location);
        }
    }
}
