using System;

using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace BasicExercises
{
    public class RepositoryContext: DbContext
    {
        public virtual DbSet<StoreItem> theStore { get; set; }
    }

    public class StoreItem
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }

    }

    public class StoreService
    {
        private readonly RepositoryContext _context;

        public StoreService(RepositoryContext context)
        {
            _context = context;
        }

        public StoreItem Stock(string name, int count)
        {
            if ( count <= 0 )
            {
                throw new InvalidOperationException("Item count can not be negative or zero!");
            }
            var item = GetByName(name);
            if (item.Name != null)
            {
                item.Count += count;
                _context.Entry(item).State = EntityState.Modified;
            } 
            else 
            {
                item = new StoreItem
                {
                    Name = name,
                    Count = count
                };
                _context.theStore.Add(item);
            }
            _context.SaveChanges();

            return item;
        }

        public List<StoreItem> GetAllItems()
        {
            var query = from item in _context.theStore
                        orderby item.Name
                        select item;

            return query.ToList();
        }

        public StoreItem GetByName(string name)
        {
            var query = from item in _context.theStore
                        where item.Name.Equals(name, StringComparison.OrdinalIgnoreCase)
                        select item;

            return query.FirstOrDefault(); // Possible that can return more than one item!
        }

        public StoreItem Buy(string name, int count)
        {
            var item = _context.theStore.FirstOrDefault(i => i.Name == name); // Possible that can return more than one item!
            if (item == null)
            {
                throw new InvalidOperationException("Item not found!");
            }
            if (count > item.Count)
            {
                throw new InvalidOperationException("Don't have enough item/s in the stock to buy!");
            }
            item.Count -= count;
            _context.SaveChanges();

            return item;
        }
    }
}
