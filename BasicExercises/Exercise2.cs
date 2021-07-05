using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicExercises
{
    public interface IStoreInventory
    {
        /// <summary>
        /// Return all Inventory Items that have been registered with the Inventory.
        /// </summary>
        /// <returns></returns>
        InventoryItem[] GetAll();

        /// <summary>
        /// Return just the Inventory Item that was registered by name.
        /// </summary>
        /// <param name="name">The name of the Inventory Item</param>
        /// <returns></returns>
        InventoryItem GetByName(string name);

        /// <summary>
        /// Stock items in the Inventory.
        /// </summary>
        /// <param name="name">The name of the Inventory Item</param>
        /// <param name="count">How much to increment or decrement the stock.</param>
        /// <returns></returns>
        InventoryItem Stock(string name, int count);

        /// <summary>
        /// Removes stock from the Inventory.
        /// </summary>
        /// <param name="name">The name of the Inventory Item</param>
        /// <param name="count">How much to decrement the stock.</param>
        /// <returns></returns>
        InventoryItem Buy(string name, int count);
    }

    public class Exercise2 : IStoreInventory
    {
        private List<InventoryItem> theStore = new List<InventoryItem>();

        public InventoryItem Buy(string name, int count)
        {
            if ( count < 0 ) {
                    throw new InvalidOperationException("Item count can not be negative!");
                }
            var newItem = GetByName(name);
            var previousCount = 0;
            try
            {
                if (newItem.Name != null) {
                    if ( newItem.Count < count ) {
                        throw new InvalidOperationException("Don't have enough item/s in the stock to buy!");
                }
                    previousCount = newItem.Count;
                    theStore.Remove(newItem);
                    newItem = new InventoryItem
                        {
                            Name = name,
                            Count = previousCount - count
                        };
                    theStore.Add(newItem);
                }
                return newItem;
            }
                catch (Exception e)
            {
                throw e;
            }
        }

        public InventoryItem[] GetAll()
        {
            return theStore.ToArray();
        }

        public InventoryItem GetByName(string name)
        {
            return theStore.Find( item => item.Name.Equals(name, StringComparison.OrdinalIgnoreCase) );
        }

        public InventoryItem Stock(string name, int count)
        {
            if ( count < 0 ) {
                    throw new InvalidOperationException("Item count can not be negative!");
                }
            var newItem = GetByName(name);
            var previousCount = 0;
            try
            {
                if (newItem.Name != null) {
                    previousCount = newItem.Count;
                    theStore.Remove(newItem);
                }
                newItem = new InventoryItem
                        {
                            Name = name,
                            Count = count + previousCount
                        };
                theStore.Add(newItem);
                return newItem;
            }
                catch (Exception e)
            {
                throw e;
            }
        }
    }

    public struct InventoryItem
    {
        public string Name { get; set; }

        public int Count { get; set; }
    }
}
