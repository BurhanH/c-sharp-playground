using System;
using System.Collections.Generic;

namespace BasicExercises
{
    public class Exercise2 : IStoreInventory
    {
        private readonly List<InventoryItem> theStore = new List<InventoryItem>();

        public InventoryItem Buy(string name, int count)
        {
            if ( count < 0 ) {
                    throw new InvalidOperationException("Item count can not be negative!");
                }
            var newItem = GetByName(name);
            var previousCount = 0;
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
    }

    public struct InventoryItem : IEquatable<InventoryItem>
    {
        public string Name { get; set; }

        public int Count { get; set; }

        public bool Equals(InventoryItem other) =>
            string.Equals(Name, other.Name, StringComparison.OrdinalIgnoreCase);

        public override bool Equals(object obj) =>
            obj is InventoryItem i && Equals(i);

        public override int GetHashCode() => Name.GetHashCode();

    }
}
