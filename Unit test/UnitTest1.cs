using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace Unit_test
{
    [TestClass]
    public class UnitTest1
    {
        private static Mock<IDal> _dal;
        private static Mock<Convertor> _convertor;
        Exception exception = null;

        [TestInitialize]
        public void Convertor()
        {
            _dal = new Mock<IDal>();
            _convertor = new Mock<Convertor>();

        }

        [TestMethod]
        public void ConvertFile()
        {
            try
            {
                string path = "C:\test";
                _convertor.Setup(x => x.ConvertFile(path));
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            Assert.IsNotNull(exception);
        }
    }
}
