// Bookify.App
// - Bookify.App.Core.Tests
// -- ObservableServiceCollectionTests.cs
// -------------------------------------------
// Author: Bjarke Søgaard <sogaardbjarke@gmail.com>

using System.Threading.Tasks;

using Bookify.App.Core.Collections;
using Bookify.App.Core.Interfaces.Services;
using Bookify.Common.Filter;

using Moq;

using Shouldly;

using Xunit;

namespace Bookify.App.Core.Tests.Collections
{
    public class ObservableServiceCollectionTests
    {
        [Fact]
        public async Task VerifyRetrievingFetchesData()
        {
            var service = new Mock<IGetByFilterService<TestDto, TestFilter>>();
            var paginatedEnumerable = new PaginatedEnumerable<TestDto>();
            service.Setup(s => s.GetItems(It.IsAny<TestFilter>())).Returns(async () => paginatedEnumerable);
            var collection = new ObservableServiceCollection<TestDto, TestFilter, IGetByFilterService<TestDto, TestFilter>>(service.Object);

            paginatedEnumerable.AddRange(new[] { new TestDto { Id = 1 }, new TestDto { Id = 2 }, new TestDto { Id = 3 } });

            collection.Count.ShouldBe(0);
            await collection.LoadMore();
            collection.Count.ShouldBe(3);

            collection[0].Id.ShouldBe(1);
            collection[1].Id.ShouldBe(2);
            collection[2].Id.ShouldBe(3);
        }

        [Fact]
        public async Task VerifyMaxCountDisablesLoadingMore()
        {
            var service = new Mock<IGetByFilterService<TestDto, TestFilter>>();
            var source = new TaskCompletionSource<PaginatedEnumerable<TestDto>>();
            service.Setup(s => s.GetItems(It.IsAny<TestFilter>())).Returns(async () => await source.Task);
            var collection = new ObservableServiceCollection<TestDto, TestFilter, IGetByFilterService<TestDto, TestFilter>>(service.Object);

            collection.Count.ShouldBe(0);
            var task = collection.LoadMore();

            collection.Count.ShouldBe(0);
            collection.IsLoading.ShouldBeTrue();
            collection.ReachedBottom.ShouldBeFalse();

            source.SetResult(new PaginatedEnumerable<TestDto>(new[] { new TestDto { Id = 1 } }, 1));

            await task;

            collection.Count.ShouldBe(1);
            collection.IsLoading.ShouldBeFalse();
            collection.ReachedBottom.ShouldBeTrue();

            task = collection.LoadMore();
            task.IsCompleted.ShouldBeTrue();

            collection.IsLoading.ShouldBeFalse();
            collection.ReachedBottom.ShouldBeTrue();

            await task;

            collection.Count.ShouldBe(1);
            collection.IsLoading.ShouldBeFalse();
            collection.ReachedBottom.ShouldBeTrue();

            service.Verify(s => s.GetItems(It.IsAny<TestFilter>()), Times.Once);
        }

        [Fact]
        public async Task VerifyRestartingResetsTheCollectionAndAllowsForMoreFetching()
        {
            var service = new Mock<IGetByFilterService<TestDto, TestFilter>>();
            var source = new TaskCompletionSource<PaginatedEnumerable<TestDto>>();
            service.Setup(s => s.GetItems(It.IsAny<TestFilter>())).Returns(async () => await source.Task);
            var collection = new ObservableServiceCollection<TestDto, TestFilter, IGetByFilterService<TestDto, TestFilter>>(service.Object);

            source.SetResult(new PaginatedEnumerable<TestDto>(new[] { new TestDto { Id = 1 } }, 1));
            await collection.LoadMore();

            collection.Count.ShouldBe(1);
            collection.IsLoading.ShouldBeFalse();
            collection.ReachedBottom.ShouldBeTrue();
            collection[0].Id.ShouldBe(1);

            source = new TaskCompletionSource<PaginatedEnumerable<TestDto>>();
            var task = collection.Restart();
            collection.ReachedBottom.ShouldBeFalse();
            collection.IsLoading.ShouldBeTrue();
            collection.Count.ShouldBe(0);
            source.SetResult(new PaginatedEnumerable<TestDto>(new[] { new TestDto { Id = 2 } }, 1));
            await task;

            collection.Count.ShouldBe(1);
            collection.IsLoading.ShouldBeFalse();
            collection.ReachedBottom.ShouldBeTrue();
            collection[0].Id.ShouldBe(1);
        }

        public class TestDto
        {
            public int Id { get; set; }
        }

        public class TestFilter : BaseFilter
        {
        }
    }

}