using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;

namespace AutomatedValetParking.Services.Tests
{
    [TestClass]
    public class BaseTest
    {
        protected static UnityContainer Container { get; set; }

        protected static void LoadUnityConfiguration()
        {
            Container = new UnityContainer();
            var section = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            section.Containers.Default.Configure(Container);
        }
    }
}
