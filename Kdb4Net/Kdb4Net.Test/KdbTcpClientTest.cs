using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kdb4Net.Net;

namespace Kdb4Net.Test
{
    [TestClass]
    public class KdbTcpClientTest
    {
        [TestMethod]
        public void TestConnection()
        {
            var tcpClient = new KdbTcpClient("localhost", 5000, "hugog");

            Assert.IsTrue(tcpClient.Connected);
        }
    }
}
