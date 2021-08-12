using Moq;
using System;
using Xunit;

namespace BasicExercises.Tests
{
    /// <summary>
    /// I have started updating the unit tests from Exercise2 to use
    /// Mocking with the Moq library instead. You will need to finish them :)
    /// </summary>
    public class Exercise3Tests
    {
        [Fact]
        public void AddsStockItems()
        {
            var mockRepository = new Mock<IRepository<InventoryItem>>();

            var inventory = new Exercise3(mockRepository.Object);
            inventory.Stock("Widget", 1);

            mockRepository.Verify(r => r.Add(It.Is<InventoryItem>(i => i.Name.Equals("Wdiget"))));
        }

        [Fact]
        public void GetsStockByName()
        {
            var expected = new InventoryItem
            {
                Name = "Widget",
                Count = 1
            };
            var mockRepository = new Mock<IRepository<InventoryItem>>();
            mockRepository.Setup(r => r.Get(It.IsAny<Func<InventoryItem, bool>>())).Returns(expected);

            var inventory = new Exercise3(mockRepository.Object);

            var widget = inventory.GetByName("Widget");

            Assert.Equal(expected.Name, widget.Name);
            Assert.Equal(expected.Count, widget.Count);
        }

        [Fact]
        public void GetsAll()
        {
            var mockRepository = new Mock<IRepository<InventoryItem>>();
            var inventory = new Exercise3(mockRepository.Object);

            inventory.Stock("Widget", 1);
            inventory.Stock("Juice Box", 2);

            var stock = inventory.GetAll();
            Assert.NotEmpty(stock);
            Assert.Contains(stock, s => s.Name.Equals("Widget") && s.Count == 1);
            Assert.Contains(stock, s => s.Name.Equals("Juice Box") && s.Count == 2);
        }

        [Fact]
        public void Stock_increases_counts()
        {
            var mockRepository = new Mock<IRepository<InventoryItem>>();
            var inventory = new Exercise3(mockRepository.Object);

            inventory.Stock("Widget", 1);
            inventory.Stock("Juice Box", 2);
            //Do a stock again.
            inventory.Stock("Widget", 1);

            var stock = inventory.GetAll();
            Assert.NotEmpty(stock);
            Assert.Contains(stock, s => s.Name.Equals("Widget") && s.Count == 2);
            Assert.Contains(stock, s => s.Name.Equals("Juice Box") && s.Count == 2);
        }

        [Fact]
        public void Stock_does_not_allow_negatives()
        {
            var mockRepository = new Mock<IRepository<InventoryItem>>();
            var inventory = new Exercise3(mockRepository.Object);

            Assert.Throws<InvalidOperationException>(() => inventory.Stock("Widget", -1));
        }

        [Fact]
        public void Buy_removes_count()
        {
            var mockRepository = new Mock<IRepository<InventoryItem>>();
            var inventory = new Exercise3(mockRepository.Object);

            inventory.Stock("Widget", 1);
            inventory.Buy("Widget", 1);

            var widget = inventory.GetByName("Widget");
            Assert.Equal(0, widget.Count);
        }

        [Fact]
        public void Buy_does_not_allow_negatives()
        {
            var mockRepository = new Mock<IRepository<InventoryItem>>();
            var inventory = new Exercise3(mockRepository.Object);

            inventory.Stock("Widget", 1);
            Assert.Throws<InvalidOperationException>(() => inventory.Buy("Widget", -1));
            Assert.Throws<InvalidOperationException>(() => inventory.Buy("Widget", 2));
        }
    }
}
