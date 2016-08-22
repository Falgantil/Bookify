// Bookify.App
// - Bookify.App.Core.Tests
// -- BootstrapperTests.cs
// -------------------------------------------
// PersonName: Bjarke Søgaard <sogaardbjarke@gmail.com>

using Bookify.App.Core.Initialization;
using Bookify.App.Core.Interfaces.Initialization;

using Ninject;

using Shouldly;

using Xunit;

namespace Bookify.App.Core.Tests.Initialization
{
    public class BootstrapperTests
    {
        [Fact]
        public void VerifyThatInitializationWorks()
        {
            var bootstrapper = Bootstrapper.Initialize(new TestPlatformInitializer());
            bootstrapper.ShouldNotBeNull();
        }
    }

    public class TestPlatformInitializer : IPlatformInitializer
    {
        public void BeforeInit(IKernel kernel)
        {
            
        }

        public void AfterInit(IKernel kernel)
        {

        }
    }
}