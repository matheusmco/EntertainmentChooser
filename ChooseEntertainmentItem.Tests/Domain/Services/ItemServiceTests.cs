using System;
using System.Collections.Generic;
using System.Linq;
using ChooseEntertainmentItem.Domain.Models;
using ChooseEntertainmentItem.Domain.Repositories;
using ChooseEntertainmentItem.Domain.Services;
using Moq;
using NUnit.Framework;

namespace ChooseEntertainmentItem.Tests.Domain.Services
{
    [TestFixture]
    public class ItemServiceTests
    {
        [Test]
        public void CalculateBacklogItemsPriority_ShouldIncludePrice_ReturnScoreWithPrice()
        {
            var repository = new Mock<IItemRepository>();
            repository.Setup(_ => _.GetDoneItems()).Returns(new List<DoneItem> { new DoneItem
            {
                Name = "DoneTest",
                Tags = "test",
                DoneDate = DateTime.Now.ToString("yyyy-MM-dd")
            }});
            var price = 1;
            repository.Setup(_ => _.GetBacklogItems()).Returns(new List<BacklogItem> { new BacklogItem
            {
                Name = "BacklogTest",
                Tags = "test",
                Price = price
            }});
            var service = MakeService(repository.Object);
            var tagScore = 1;
            var expectedScore = tagScore + price;

            var items = service.CalculateBacklogItemsPriority();

            Assert.AreEqual(expectedScore, items.First().GetFinalScore());
        }

        [Test]
        public void CalculateBacklogItemsPriority_ShouldNotIncludePrice_ReturnScoreWithoutPrice()
        {
            var repository = new Mock<IItemRepository>();
            repository.Setup(_ => _.GetDoneItems()).Returns(new List<DoneItem> { new DoneItem
            {
                Name = "DoneTest",
                Tags = "test",
                DoneDate = DateTime.Now.ToString("yyyy-MM-dd")
            }});
            var price = 1;
            repository.Setup(_ => _.GetBacklogItems()).Returns(new List<BacklogItem> { new BacklogItem
            {
                Name = "BacklogTest",
                Tags = "test",
                Price = price
            }});
            var service = MakeService(repository.Object);
            var tagScore = 1;
            var expectedScore = tagScore;

            var items = service.CalculateBacklogItemsPriority();

            Assert.AreEqual(expectedScore, items.First().GetFinalScore());
        }

        [Test]
        public void CalculateBacklogItemsPriority_FilterByType_ReturnFiltered()
        {
            var repository = new Mock<IItemRepository>();
            repository.Setup(_ => _.GetDoneItems()).Returns(new List<DoneItem>
            {
                new DoneItem
                {
                    Name = "DoneTest1",
                    Tags = "test/type1",
                    DoneDate = DateTime.Now.ToString("yyyy-MM-dd")
                }
            });
            repository.Setup(_ => _.GetBacklogItems()).Returns(new List<BacklogItem>
            {
                new BacklogItem
                {
                    Name = "BacklogTest1",
                    Tags = "test/type1"
                },
                new BacklogItem
                {
                    Name = "BacklogTest1",
                    Tags = "test/type2",
                }
            });
            var service = MakeService(repository.Object);

            var items = service.CalculateBacklogItemsPriority();

            Assert.AreEqual(1, items.Count());
        }

        [Test]
        public void CalculateBacklogItemsPriority_DoNotFilterByType_ReturnAll()
        {
            var repository = new Mock<IItemRepository>();
            repository.Setup(_ => _.GetDoneItems()).Returns(new List<DoneItem>
            {
                new DoneItem
                {
                    Name = "DoneTest1",
                    Tags = "test/type1",
                    DoneDate = DateTime.Now.ToString("yyyy-MM-dd")
                }
            });
            repository.Setup(_ => _.GetBacklogItems()).Returns(new List<BacklogItem>
            {
                new BacklogItem
                {
                    Name = "BacklogTest1",
                    Tags = "test/type1"
                },
                new BacklogItem
                {
                    Name = "BacklogTest1",
                    Tags = "test/type2",
                }
            });
            var service = MakeService(repository.Object);

            var items = service.CalculateBacklogItemsPriority();

            Assert.AreEqual(2, items.Count());
        }

        private ItemService MakeService(IItemRepository repository)
            => new ItemService(repository, null);
    }
}