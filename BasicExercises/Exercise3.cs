using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace BasicExercises
{
    /// <summary>
    /// This new Exercise is to show WHY we want to use Interfaces. 
    /// Replace theStore in your original implementation from Exercise2 with the new
    /// <see cref="IRepository{T}"/>
    /// </summary>
    public class Exercise3: IStoreInventory
    {
        private readonly IRepository<InventoryItem> theStore;
        public Exercise3(IRepository<InventoryItem> repository)
        {
            theStore = repository;
        }
        
        public InventoryItem Buy(string name, int count)
        {
            throw new NotImplementedException();
        }

        public InventoryItem[] GetAll()
        {
            throw new NotImplementedException();
        }

        public InventoryItem GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public InventoryItem Stock(string name, int count)
        {
            throw new NotImplementedException();
        }
    }

    public interface IRepository<T>
    {
        /// <summary>
        /// Retrieve from the repository an item using a <see cref="Predicate{T}"/>
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        T Get(Func<T, bool> predicate);
        /// <summary>
        /// Retrieve all items from the Repository
        /// </summary>
        /// <returns></returns>
        T[] GetAll();
        /// <summary>
        /// Update an item in the Repository
        /// </summary>
        /// <param name="predicate">A  <see cref="Predicate{T}"/> to identify the item to update.</param>
        /// <param name="item">The item to update</param>
        /// <returns>The original value</returns>
        T Update(Func<InventoryItem, bool> predicate, T item);
        /// <summary>
        /// Add an item to the Repository
        /// </summary>
        /// <param name="item"></param>
        void Add(T item);
    }

}
