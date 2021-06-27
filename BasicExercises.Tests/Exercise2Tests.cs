using System;
using System.Linq;
using Xunit;

namespace BasicExercises.Tests
{
    public class Exercise2Tests
    {
        [Fact]
        public void AddsStockItems()
        {
            var inventory = new Exercise2();

            inventory.Stock("Widget", 1);

            var result = inventory.GetAll();
            Assert.Single(result);
            var widget = result.First(n => n.Name.Equals("Widget", StringComparison.OrdinalIgnoreCase));

            Assert.Equal(1, widget.Count);
        }

        [Fact]
        public void GetsStockByName()
        {
            var inventory = new Exercise2();

            inventory.Stock("Widget", 1);
            inventory.Stock("Juice Box", 2);

            var widget = inventory.GetByName("Widget");

            Assert.Equal("Widget", widget.Name);
            Assert.Equal(1, widget.Count);
        }

        [Fact]
        public void GetsAll()
        {
            var inventory = new Exercise2();

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
            var inventory = new Exercise2();

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
            var inventory = new Exercise2();

            Assert.Throws<InvalidOperationException>(() => inventory.Stock("Widget", -1));
        }

        [Fact]
        public void Buy_removes_count()
        {

            var inventory = new Exercise2();

            inventory.Stock("Widget", 1);
            inventory.Buy("Widget", 1);

            var widget = inventory.GetByName("Widget");
            Assert.Equal(0, widget.Count);
        }

        [Fact]
        public void Buy_does_not_allow_negatives()
        {

            var inventory = new Exercise2();

            inventory.Stock("Widget", 1);
            Assert.Throws<InvalidOperationException>(() => inventory.Buy("Widget", -1));
            Assert.Throws<InvalidOperationException>(() => inventory.Buy("Widget", 2));
        }
    }
}
