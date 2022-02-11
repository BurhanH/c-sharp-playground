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
        [Fact]
        public void Add_stock_item()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<RepositoryContext>()
                .UseInMemoryDatabase("Tests1")
                .Options;
            var dbcontext = new RepositoryContext(options);

            var sut = new StoreService(dbcontext); // SUT - system under test

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
            var options = new DbContextOptionsBuilder<RepositoryContext>()
                .UseInMemoryDatabase(databaseName: "Tests2")
                .Options;
            var dbcontext = new RepositoryContext(options);
            dbcontext.theStore.Add(new StoreItem { Name = "Widget", Count = 1 });
            dbcontext.theStore.Add(new StoreItem { Name = "Juice Box", Count = 2 });
            dbcontext.SaveChanges();

            var sut = new StoreService(dbcontext); // SUT - system under test

            // Act
            var item = sut.GetByName("Widget");

            // Assert
            Assert.NotNull(item);
            Assert.Equal("Widget", item.Name);
            Assert.Equal(1, item.Count);
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

        [Fact]
        public void Stock_increases_an_item_counts()
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
            var item = sut.Stock("Widget", 1);

            // Assert
            Assert.Equal(2, item.Count);
        }

        [Fact]
        public void Stock_does_not_allow_negatives()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<RepositoryContext>()
                .UseInMemoryDatabase(databaseName: "Tests4")
                .Options;
            var dbcontext = new RepositoryContext(options);

            var sut = new StoreService(dbcontext); // SUT - system under test

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => sut.Stock("Widget", -1));
        }

        [Fact]
        public void Buy_removes_an_item_count()
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
            var item = sut.Buy("Widget", 1);

            // Assert
            Assert.Equal(1, item.Count);
        }

        [Fact]
        public void Buy_does_not_allow_negatives()
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
            Assert.Throws<InvalidOperationException>(() => sut.Buy("Widget", -1));
            Assert.Throws<InvalidOperationException>(() => sut.Buy("Juice Box", 2));
        }
    }
}