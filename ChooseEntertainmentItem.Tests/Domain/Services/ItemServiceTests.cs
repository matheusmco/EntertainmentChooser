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
            var backlogRepository = new Mock<IBacklogItemRepository>();
            var price = 1;
            backlogRepository.Setup(_ => _.Get()).Returns(new List<BacklogItem> { new BacklogItem
            {
                Name = "BacklogTest",
                Tags = "test",
                Price = price
            }});
            var service = CreateService(backlogRepository.Object);
            var tagScore = 1;
            var expectedScore = tagScore + price;

            var items = service.CalculateBacklogItemsPriority(true, "ALL");

            Assert.AreEqual(expectedScore, items.First().Score);
        }

        [Test]
        public void CalculateBacklogItemsPriority_ShouldNotIncludePrice_ReturnScoreWithoutPrice()
        {
            var repository = new Mock<IBacklogItemRepository>();
            var price = 1;
            repository.Setup(_ => _.Get()).Returns(new List<BacklogItem> { new BacklogItem
            {
                Name = "BacklogTest",
                Tags = "test",
                Price = price
            }});
            var service = CreateService(repository.Object);
            var tagScore = 1;
            var expectedScore = tagScore;

            var items = service.CalculateBacklogItemsPriority(false, "ALL");

            Assert.AreEqual(expectedScore, items.First().Score);
        }

        [Test]
        public void CalculateBacklogItemsPriority_FilterByType_ReturnFiltered()
        {
            var repository = new Mock<IBacklogItemRepository>();
            repository.Setup(_ => _.Get()).Returns(new List<BacklogItem>
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
            var service = CreateService(repository.Object);

            var items = service.CalculateBacklogItemsPriority(false, "type1");

            Assert.AreEqual(1, items.Count());
        }

        [Test]
        public void CalculateBacklogItemsPriority_DoNotFilterByType_ReturnAll()
        {
            var repository = new Mock<IBacklogItemRepository>();
            repository.Setup(_ => _.Get()).Returns(new List<BacklogItem>
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
            var service = CreateService(repository.Object);

            var items = service.CalculateBacklogItemsPriority(false, "ALL");

            Assert.AreEqual(2, items.Count());
        }

        public ItemService CreateService(IBacklogItemRepository backlogRepository = null, IDoneItemRepository doneRepository = null)
        {
            backlogRepository = backlogRepository ?? new Mock<IBacklogItemRepository>().Object;

            if (doneRepository == null)
            {
                var doneRepositoryMock = new Mock<IDoneItemRepository>();
                doneRepositoryMock.Setup(_ => _.Get()).Returns(new List<DoneItem>
                {
                    new DoneItem
                    {
                        Name = "DoneTest1",
                        Tags = "test/type1",
                        DoneDate = DateTime.Now.ToString("yyyy-MM-dd")
                    }
                });
                doneRepository = doneRepositoryMock.Object;
            }

            return new ItemService(backlogRepository, doneRepository);
        }
    }
}