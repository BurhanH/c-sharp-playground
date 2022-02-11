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
            // Arrange
            var data = new List<StoreItem>{}.AsQueryable();

            var mockRepository = new Mock<DbSet<StoreItem>>();
            mockRepository.As<IQueryable<StoreItem>>().Setup(m => m.Provider).Returns(data.Provider);
            mockRepository.As<IQueryable<StoreItem>>().Setup(m => m.Expression).Returns(data.Expression);
            mockRepository.As<IQueryable<StoreItem>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockRepository.As<IQueryable<StoreItem>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<RepositoryContext>();
            mockContext.Setup(m => m.theStore).Returns(mockRepository.Object);

            var sut = new StoreService(mockContext.Object); // sut - system under test

            // Act
            var item = sut.Stock("Widget", 1);

            // Assert
            Assert.Equal("Widget", item.Name);
            Assert.Equal(1, item.Count);
        }

        [Fact]
        public void Get_stock_item_by_name()
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
            var item = sut.GetByName("Widget");

            // Assert
            Assert.Equal("Widget", item.Name);
            Assert.Equal(1, item.Count);
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
            Assert.Equal("Widget", items[0].Name);
            Assert.Equal(1, items[0].Count);
            Assert.Equal("Juice Box", items[1].Name);
            Assert.Equal(2, items[1].Count);
        }

        [Fact]
        public void Stock_increases_an_item_counts()
        {
            // Arrange
            var data = new List<StoreItem>
            {
                new StoreItem { Name = "Widget", Count = 1 },
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
            var item = sut.Stock("Widget", 1);

            // Assert
            Assert.Equal(2, item.Count);
        }

        [Fact]
        public void Stock_does_not_allow_negatives()
        {
            // Arrange
            var mockContext = new Mock<RepositoryContext>();
            var sut = new StoreService(mockContext.Object); // sut - system under test

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => sut.Stock("Widget", -1));
        }

        [Fact]
        public void Buy_removes_an_item_count()
        {
            // Arrange
            var data = new List<StoreItem>
            {
                new StoreItem { Name = "Widget", Count = 2 },
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
            var item = sut.Buy("Widget", 1);

            // Assert
            Assert.Equal(1, item.Count);
        }

        [Fact]
        public void Buy_does_not_allow_negatives()
        {
            // Arrange
            var data = new List<StoreItem>
            {
                new StoreItem { Name = "Widget", Count = 1 },
            }.AsQueryable();

            var mockRepository = new Mock<DbSet<StoreItem>>();
            mockRepository.As<IQueryable<StoreItem>>().Setup(m => m.Provider).Returns(data.Provider);
            mockRepository.As<IQueryable<StoreItem>>().Setup(m => m.Expression).Returns(data.Expression);
            mockRepository.As<IQueryable<StoreItem>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockRepository.As<IQueryable<StoreItem>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<RepositoryContext>();
            mockContext.Setup(m => m.theStore).Returns(mockRepository.Object);

            var sut = new StoreService(mockContext.Object); // sut - system under test

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => sut.Buy("Widget", -1));
            Assert.Throws<InvalidOperationException>(() => sut.Buy("Widget", 2));
        }
    }
}