using Course_work;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject
{

    [TestClass]
    public class UnitTestFormGameWindow
    {
        [TestMethod]
        public void UnitTestFormGameWindow1()
        {
            FormMainMenu formMainMenu = new FormMainMenu();
            var expected = new FormGameWindow(formMainMenu);
            var actual = formMainMenu;
            Assert.AreEqual(expected.f2.GetType(), actual.GetType());
        }
        [TestMethod]
        public void UnitTestFormGameWindow2()
        {
            FormMainMenu formMainMenu = new FormMainMenu();
            FormGameWindow formGameWindow = new FormGameWindow(formMainMenu);
            formGameWindow.Jump();
            var expected = 1;
            var actual = formGameWindow._gravity_modifier;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void UnitTestFormGameWindow3()
        {
            FormMainMenu formMainMenu = new FormMainMenu();
            FormGameWindow formGameWindow = new FormGameWindow(formMainMenu);
            formGameWindow.ChangingThePolarity();
            var expected = 1;
            var actual = formGameWindow._gravity_modifier;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void UnitTestFormGameWindow4()
        {
            FormMainMenu formMainMenu = new FormMainMenu();
            FormGameWindow formGameWindow = new FormGameWindow(formMainMenu);
            formGameWindow.Death();
            var expected = true;
            var actual = formGameWindow._deathBool;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void UnitTestFormGameWindow5()
        {
            FormMainMenu formMainMenu = new FormMainMenu();
            FormGameWindow formGameWindow = new FormGameWindow(formMainMenu);
            formGameWindow.ChangingThePolarity();
            var expected = false;
            var actual = formGameWindow._changing_the_polarityBool;
            Assert.AreEqual(expected, actual);
        }
    }
}
