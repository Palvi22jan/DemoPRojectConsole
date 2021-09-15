using System;
using System.Configuration;
using Unity;

namespace DemoPRojectConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            IUnityContainer objContainer = new UnityContainer();
            objContainer.RegisterType<Convertor>();
            objContainer.RegisterType<IDal, MySQLDal>();
            objContainer.RegisterType<IDal, MongoDB>("MongoDB");
            IDal mySQL = objContainer.Resolve<IDal>();
            IDal mongoDB = objContainer.Resolve<IDal>("MongoDB");

            Convertor cObj = objContainer.Resolve<Convertor>();
            // We can read this 
            string input = ConfigurationManager.AppSettings["FilePath"];
            cObj.ConvertFile(input);
            Console.WriteLine("Hello World!");
        }
    }
}
