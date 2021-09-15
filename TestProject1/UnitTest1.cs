using DemoPRojectConsole;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace Unit_test
{
    [TestClass]
    public class UnitTest1
    {
        private static Mock<IDal> _dal;
        private static Convertor _convertor;
        Exception exception = null;

        [TestInitialize]
        public void Convertor()
        {
            _dal = new Mock<IDal>();
            _convertor = new Convertor(_dal.Object);

        }

        [TestMethod]
        public void PositiveTestAsInput()
        {
            try
            {
                string path = "C:\test.json";
                //we can do mock of methods in calling like below
                // mock.Setup(x =>x.Data(It.IsAny<string>()));

                _convertor.ConvertFile(path);
                throw (new Exception());
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            Assert.IsNotNull(exception);
        }

        [TestMethod]
        public void NegativeTestForPDFAsInput()
        {
            try
            {
                string path = "C:\test.pdf";
                _convertor.ConvertFile(path);
                throw (new Exception());
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            Assert.IsNotNull(exception);
        }
    }
}
