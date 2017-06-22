using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AiRTech2.Models;

using Xamarin.Forms;

[assembly: Dependency(typeof(AiRTech2.Services.MockDataStore))]
namespace AiRTech2.Services
{
    public class MockDataStore : IDataStore<Category>
    {
        private bool _isInitialized;
        private List<Category> _items;

        public async Task<bool> AddItemAsync(Category category)
        {
            await InitializeAsync();

            _items.Add(category);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Category category)
        {
            await InitializeAsync();

            var _item = _items.FirstOrDefault(arg => arg.Id == category.Id);
            _items.Remove(_item);
            _items.Add(category);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(Category category)
        {
            await InitializeAsync();

            var _item = _items.FirstOrDefault(arg => arg.Id == category.Id);
            _items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Category> GetItemAsync(string id)
        {
            await InitializeAsync();

            return await Task.FromResult(_items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Category>> GetItemsAsync(bool forceRefresh = false)
        {
            await InitializeAsync();

            return await Task.FromResult(_items);
        }

        public Task<bool> PullLatestAsync()
        {
            return Task.FromResult(true);
        }


        public Task<bool> SyncAsync()
        {
            return Task.FromResult(true);
        }

        public async Task InitializeAsync()
        {
            if (_isInitialized)
            { return; }

            _items = new List<Category>
            {
                new Category { Id = Guid.NewGuid().ToString(), Text = "Buy some cat food", Description="The cats are hungry"},
                new Category { Id = Guid.NewGuid().ToString(), Text = "Learn F#", Description="Seems like a functional idea"},
                new Category { Id = Guid.NewGuid().ToString(), Text = "Learn to play guitar", Description="Noted"},
                new Category { Id = Guid.NewGuid().ToString(), Text = "Buy some new candles", Description="Pine and cranberry for that winter feel"},
                new Category { Id = Guid.NewGuid().ToString(), Text = "Complete holiday shopping", Description="Keep it a secret!"},
                new Category { Id = Guid.NewGuid().ToString(), Text = "Finish a todo list", Description="Done"},
            };

            _isInitialized = true;
        }
    }
}
