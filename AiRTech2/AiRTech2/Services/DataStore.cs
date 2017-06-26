using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AiRTech2.Models;

using Xamarin.Forms;

namespace AiRTech2.Services
{
    public class DataStore
    {
        private bool _isInitialized;
        private List<Category> _items;
        
        public async Task<Category> GetCategoryAsync(string id)
        {
            await InitializeAsync();

            return await Task.FromResult(_items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync(bool forceRefresh = false)
        {
            await InitializeAsync();

            return await Task.FromResult(_items);
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
