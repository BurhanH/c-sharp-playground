using System;
using Xunit;

using Microsoft.EntityFrameworkCore;


namespace BasicExercises.Tests
{
    /// <summary>
    /// Unit tests for Exercise4
    /// Uses In-memory database.
    /// </summary>

    // Note: If we will use the same database name for multiple tests, some test may fail.
    //       The main reason is looks like DbContextOptionsBuilder reuses the same database. Instead of creating a new one.

    public class Exercise4Tests
    {
        [Theory]
        [InlineData("Widget", 1)]
        [InlineData("Juice Box", 2)]
        public void Add_stock_item(string name, int count)
        {
            // Arrange
            var options = new DbContextOptionsBuilder<RepositoryContext>()
                .UseInMemoryDatabase("Tests1")
                .Options;
            var dbcontext = new RepositoryContext(options);

            var sut = new StoreService(dbcontext); // SUT - system under test

            // Act
            var item = sut.Stock(name, count);

            // Assert
            Assert.Equal(name, item.Name);
            Assert.Equal(count, item.Count);
        }

        [Theory]
        [InlineData("Widget", 1)]
        [InlineData("Juice Box", 2)]
        public void Get_stock_item_by_name(string name, int count)
        {
            // Arrange
            var options = new DbContextOptionsBuilder<RepositoryContext>()
                .UseInMemoryDatabase(databaseName: "Tests2")
                .Options;
            var dbcontext = new RepositoryContext(options);
            dbcontext.theStore.Add(new StoreItem { Name = "Widget", Count = 1 });
            dbcontext.theStore.Add(new StoreItem { Name = "Juice Box", Count = 2 });
            dbcontext.SaveChanges();

            var sut = new StoreService(dbcontext); // SUT - system under test

            // Act
            var item = sut.GetByName(name);

            // Assert
            Assert.NotNull(item);
            Assert.Equal(name, item.Name);
            Assert.Equal(count, item.Count);
        }

        [Fact]
        public void Get_all_stock_items()
        {
            // Arrange
           var options = new DbContextOptionsBuilder<RepositoryContext>()
                .UseInMemoryDatabase(databaseName: "Tests3")
                .Options;
            var dbcontext = new RepositoryContext(options);
            dbcontext.theStore.Add(new StoreItem { Name = "Widget", Count = 1 });
            dbcontext.theStore.Add(new StoreItem { Name = "Juice Box", Count = 2 });
            dbcontext.SaveChanges();

            var sut = new StoreService(dbcontext); // SUT - system under test

            // Act
            var items = sut.GetAllItems();

            // Assert
            Assert.NotEmpty(items);
            Assert.Equal(2, items.Count);
            Assert.Equal("Widget", items[0].Name);
            Assert.Equal(1, items[0].Count);
            Assert.Equal("Juice Box", items[1].Name);
            Assert.Equal(2, items[1].Count);
        }

        [Theory]
        [InlineData("Widget", 1, 2)]
        [InlineData("Juice Box", 2, 4)]
        public void Stock_increases_an_item_counts(string name, int count, int expectedCount)
        {
            // Arrange
            var options = new DbContextOptionsBuilder<RepositoryContext>()
                .UseInMemoryDatabase(databaseName: "Tests4")
                .Options;
            var dbcontext = new RepositoryContext(options);
            dbcontext.theStore.Add(new StoreItem { Name = "Widget", Count = 1 });
            dbcontext.theStore.Add(new StoreItem { Name = "Juice Box", Count = 2 });
            dbcontext.SaveChanges();

            var sut = new StoreService(dbcontext); // SUT - system under test

            // Act
            var item = sut.Stock(name, count);

            // Assert
            Assert.Equal(expectedCount, item.Count);
        }

        [Theory]
        [InlineData("Widget", -1)]
        [InlineData("Juice Box", 0)]
        public void Stock_does_not_allow_negatives_or_zero(string name, int count)
        {
            // Arrange
            var options = new DbContextOptionsBuilder<RepositoryContext>()
                .UseInMemoryDatabase(databaseName: "Tests4")
                .Options;
            var dbcontext = new RepositoryContext(options);

            var sut = new StoreService(dbcontext); // SUT - system under test

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => sut.Stock(name, count));
        }

        [Theory]
        [InlineData("Widget", 1, 1)]
        [InlineData("Juice Box", 2, 0)]
        public void Buy_removes_an_item_count(string name, int count, int expectedCount)
        {
            // Arrange
            var options = new DbContextOptionsBuilder<RepositoryContext>()
                .UseInMemoryDatabase(databaseName: "Tests5")
                .Options;
            var dbcontext = new RepositoryContext(options);
            dbcontext.theStore.Add(new StoreItem { Name = "Widget", Count = 2 });
            dbcontext.theStore.Add(new StoreItem { Name = "Juice Box", Count = 2 });
            dbcontext.SaveChanges();

            var sut = new StoreService(dbcontext); // SUT - system under test

            // Act
            var item = sut.Buy(name, count);

            // Assert
            Assert.Equal(expectedCount, item.Count);
        }

        [Theory]
        [InlineData("Widget", -1)]
        [InlineData("Juice Box", 2)]
        [InlineData("Juice Box", 0)]
        public void Buy_does_not_allow_negatives_or_zero(string name, int count)
        {
            // Arrange
            var options = new DbContextOptionsBuilder<RepositoryContext>()
                .UseInMemoryDatabase(databaseName: "Tests6")
                .Options;
            var dbcontext = new RepositoryContext(options);
            dbcontext.theStore.Add(new StoreItem { Name = "Widget", Count = 2 });
            dbcontext.theStore.Add(new StoreItem { Name = "Juice Box", Count = 1 });
            dbcontext.SaveChanges();

            var sut = new StoreService(dbcontext); // SUT - system under test

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => sut.Buy(name, count));
        }
        [Theory]
        [InlineData("", 1)]
        [InlineData(" ", 10)]
        [InlineData(null, 5)]
        public void Stock_does_not_allow_empty_names(string name, int count)
        {
            // Arrange
            var options = new DbContextOptionsBuilder<RepositoryContext>()
                .UseInMemoryDatabase("Tests7")
                .Options;
            var dbcontext = new RepositoryContext(options);

            var sut = new StoreService(dbcontext); // SUT - system under test

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => sut.Stock(name, count));
        }
    }
}