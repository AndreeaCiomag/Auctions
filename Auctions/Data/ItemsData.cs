using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using Auctions.Models;

namespace Auctions.Data
{
    public class ItemsData
    {
        readonly SQLiteAsyncConnection _database;

        //constructorul clasei; argument = calea spre baza de date, furnizata de clasa App
        public ItemsData(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Item>().Wait();
        }

        //cod pentru crearea, citirea, scrierea si stergerea datelor
        public Task<List<Item>> GetItemsAsync()
        {
            return _database.Table<Item>().ToListAsync();
        }
        public Task<Item> GetItemAsync(int id)
        {
            return _database.Table<Item>()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();
        }
        public Task<int> SaveItemAsync(Item item)
        {
            if(item.Id != 0)
            {
                return _database.UpdateAsync(item);
            }
            else
            {
                return _database.InsertAsync(item);
            }
        }
        public Task<int> DeleteItemAsync(Item item)
        {
            return _database.DeleteAsync(item);
        }
        public Task<List<Item>> GetItems(string name)
        {
            return _database.Table<Item>().
                Where(i => i.Name.ToLower().
                Contains(name.ToLower())).ToListAsync();
        }
        public Task<List<Item>> GetCategory(string category)
        {
            return _database.Table<Item>().
                Where(i => i.Category.ToLower().
                Contains(category.ToLower())).ToListAsync();
        }
    }
}
