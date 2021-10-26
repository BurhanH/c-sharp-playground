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
        public void Add_stock_item()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<InventoryItem>>();
            var sut = new Exercise3(mockRepository.Object);
            
            // Act
            sut.Stock("Widget", 1);

            // Assert
            mockRepository.Verify(r => r.Add(It.Is<InventoryItem>(i => i.Name.Equals("Widget"))));
        }

        [Fact]
        public void Get_stock_item_by_name()
        {
            // Arrange
            var expected = new InventoryItem
            {
                Name = "Widget",
                Count = 1
            };
            var mockRepository = new Mock<IRepository<InventoryItem>>();
            mockRepository.Setup(r => r.Get(It.IsAny<Func<InventoryItem, bool>>())).Returns(expected);
            var sut = new Exercise3(mockRepository.Object);

            // Act
            var widget = sut.GetByName("Widget");

            // Assert
            Assert.Equal(expected.Name, widget.Name);
            Assert.Equal(expected.Count, widget.Count);
        }

        [Fact]
        public void Get_all_stock_items()
        {
            // Arrange
            var expected = new []{
                new InventoryItem
                    {
                        Name = "Widget",
                        Count = 1
                    },
                new InventoryItem
                    {
                        Name = "Juice Box",
                        Count = 2
                    }
            };
            var mockRepository = new Mock<IRepository<InventoryItem>>();
            mockRepository.Setup(r => r.GetAll()).Returns(expected);
            var sut = new Exercise3(mockRepository.Object);

            // Act
            var stock = sut.GetAll();

            // Assert
            Assert.NotEmpty(stock);
            Assert.Contains(stock, s => s.Name.Equals("Widget") && s.Count == 1);
            Assert.Contains(stock, s => s.Name.Equals("Juice Box") && s.Count == 2);
        }

        [Fact]
        public void Stock_increases_an_item_counts()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<InventoryItem>>();
            mockRepository.Setup(r => r.Get(It.IsAny<Func<InventoryItem, bool>>())).Returns(
                new InventoryItem
                {
                    Name = "Widget",
                    Count = 1
                });
            var sut = new Exercise3(mockRepository.Object);

            // Act
            sut.Stock("Widget", 1);

            // Assert
            mockRepository.Verify(r => r.Update(It.IsAny<Func<InventoryItem, bool>>(), It.Is<InventoryItem>(i => i.Count == 2)));
        }

        [Fact]
        public void Stock_does_not_allow_negatives()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<InventoryItem>>();
            var inventory = new Exercise3(mockRepository.Object);

            // Act
            Assert.Throws<InvalidOperationException>(() => inventory.Stock("Widget", -1));
        }

        [Fact]
        public void Buy_removes_an_item_count()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<InventoryItem>>();
            mockRepository.Setup(r => r.Get(It.IsAny<Func<InventoryItem, bool>>())).Returns(
                new InventoryItem
                {
                    Name = "Widget",
                    Count = 1
                });
            var sut = new Exercise3(mockRepository.Object);

            // Act
            sut.Buy("Widget", 1);

            // Assert
            mockRepository.Verify(r => r.Update(It.IsAny<Func<InventoryItem, bool>>(), It.Is<InventoryItem>(i => i.Count == 0)));
        }

        [Fact]
        public void Buy_does_not_allow_negatives()
        {
            // Arrange
            var expected = new InventoryItem
            {
                Name = "Widget",
                Count = 1
            };
            var mockRepository = new Mock<IRepository<InventoryItem>>();
            mockRepository.Setup(r => r.Get(It.IsAny<Func<InventoryItem, bool>>())).Returns(expected);
            var sut = new Exercise3(mockRepository.Object);

            // Act
            Assert.Throws<InvalidOperationException>(() => sut.Buy("Widget", -1));
            Assert.Throws<InvalidOperationException>(() => sut.Buy("Widget", 2));
        }
    }
}
