using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoonDate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoonDate.Tests
{
    [TestClass()]
    public class MoonPhaseTests
    {
        [TestMethod()]
        public void GetCurrentMoonPhaseImageTest()
        {
            String imgUrl = MoonPhase.GetCurrentMoonPhaseImageAsync();
            Assert.Fail();
        }
    }
}