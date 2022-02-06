using Moq;
using System;
using Xunit;

using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace BasicExercises.Tests
{
    /// <summary>
    /// Unit tests for Exercise4
    /// Uses Moq library and In-memory database.
    /// </summary>
    public class Exercise4Tests
    {
        [Fact]
        public void Add_stock_item()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void Get_stock_item_by_name()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void Get_all_stock_items()
        {
            // Arrange
            var data = new List<StoreItem>
            {
                new StoreItem { Name = "Widget", Count = 1 },
                new StoreItem { Name = "Juice Box", Count = 2 },
            }.AsQueryable();

            var mockRepository = new Mock<DbSet<StoreItem>>();
            mockRepository.As<IQueryable<StoreItem>>().Setup(m => m.Provider).Returns(data.Provider);
            mockRepository.As<IQueryable<StoreItem>>().Setup(m => m.Expression).Returns(data.Expression);
            mockRepository.As<IQueryable<StoreItem>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockRepository.As<IQueryable<StoreItem>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<RepositoryContext>();
            mockContext.Setup(m => m.theStore).Returns(mockRepository.Object);

            var sut = new StoreService(mockContext.Object); // sut - system under test

            // Act
            var items = sut.GetAllItems();

            // Assert
            Assert.Equal(2, items.Count);
            Assert.Equal("Juice Box", items[0].Name);
            Assert.Equal(2, items[0].Count);
            Assert.Equal("Widget", items[1].Name);
            Assert.Equal(1, items[1].Count);

            // NOTE! It was a surprise that the order in the data list is not the same as in the in-memory database,
            //       looks like the order by name in the result data set.
            // TODO - fix this!
        }

        [Fact]
        public void Stock_increases_an_item_counts()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void Stock_does_not_allow_negatives()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void Buy_removes_an_item_count()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void Buy_does_not_allow_negatives()
        {
            throw new NotImplementedException();
        }
    }
}