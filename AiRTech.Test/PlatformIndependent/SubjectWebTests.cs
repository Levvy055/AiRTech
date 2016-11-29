using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiRTech.Core.DataHandling;
using AiRTech.Core.Web;
using NUnit.Framework;

namespace AiRTech.Test.PlatformIndependent
{
    [TestFixture(AiRTechTestFixture.CROSS_PLATFORM_WEB)]
    public class SubjectWebTests
    {
        [Test]
        public void CheckStatus()
        {
            Assert.Throws<NullReferenceException>(() => new WebCore(null));
            var db = new DbHandler();
            Assert.IsNotNull(db);
            var core = new WebCore(db);
            Assert.IsNotNull(core);
            Assert.IsTrue(core.IsConnected());
        }
    }
}
