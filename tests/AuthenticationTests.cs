using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SynoDs.Core.CrossCutting;
using SynoDs.Core.Contracts.IoC;

namespace SynologyTests
{
    [TestClass]
    public class AuthenticationTests
    {
        // fields.
        private Mock<IContainer> _container;
        private Mock<IoCFactory> _factory;
        private MockRepository mockRepository;

        [TestInitialize]
        public void Initialize()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);
            this._container = this.mockRepository.Create<IContainer>();
            this._factory = this.mockRepository.Create<IoCFactory>();
        }

        [TestMethod]
        public void TestLoginAsyncPasses()
        {
            var container = new NinjectContainer();
        }
    }
}
