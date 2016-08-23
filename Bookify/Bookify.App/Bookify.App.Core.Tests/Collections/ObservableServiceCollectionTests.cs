// Bookify.App
// - Bookify.App.Core.Tests
// -- ObservableServiceCollectionTests.cs
// -------------------------------------------
// Author: Bjarke Søgaard <sogaardbjarke@gmail.com>

using Bookify.App.Core.Collections;
using Bookify.App.Core.Interfaces.Services;
using Bookify.Common.Filter;

using Moq;

using Xunit;

namespace Bookify.App.Core.Tests.Collections
{
    public class ObservableServiceCollectionTests
    {
        [Fact]
        public void VerifyRetrievingFetchesData()
        {
            var service = new Mock<IGetByFilterService<TestDto, TestFilter>>();
            var collection = new ObservableServiceCollection<TestDto, TestFilter, IGetByFilterService<TestDto, TestFilter>>(service.Object);
            

        }
        
        private class TestDto
        {
        }

        private class TestFilter : BaseFilter
        {
        }
    }

}