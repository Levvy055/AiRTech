using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiRTech.Core.DataHandling;
using NUnit.Framework;

namespace AiRTech.Test.PlatformIndependent
{
    [TestFixture(AiRTechTestFixture.CROSS_PLATFORM)]
    public class SubjectDbTests
    {
        private DbHandler db;

        [SetUp]
        public void InitTest()
        {
            /*db = new DbHandler();
            Assert.IsNotNull(db);*/
        }

        [Test]
        public void DbFileCreationTetst()
        {

        }
    }
}
