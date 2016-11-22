using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiRTech.Core.Web;
using NUnit.Framework;

namespace AiRTech.Test.PlatformIndependent
{
    [TestFixture("Cross-Platform")]
    public class SubjectWebTests
    {
        [Test]
        public void CheckStatus()
        {
            var core = new WebCore();
            Assert.IsNotNull(core);
            Assert.IsTrue(core.Connected);
        }
    }
}
