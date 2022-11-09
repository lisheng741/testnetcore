using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass()]
    public class ProgramTests
    {
        [TestMethod()]
        public void ReadAndShowTest()
        {
            Assert.AreEqual(Program.ReadAndShow("a"), "aaa");

            Assert.AreEqual(Program.ReadAndShow("b"), "eeee");
        }

        [TestMethod]
        public void BoolTestTest()
        {
            Assert.AreEqual(Program.BoolTest(true), true);

            Assert.AreEqual(Program.BoolTest(false), false);
        }
    }
}