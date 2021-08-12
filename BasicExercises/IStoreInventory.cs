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
}
