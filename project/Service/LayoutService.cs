using Microsoft.EntityFrameworkCore;
using OganiShoppingProject.Data;

namespace OganiShoppingProject.Service
{
    public class LayoutService
    {
        private readonly ShoppingDbContext _context;
        public LayoutService(ShoppingDbContext context)
        {
            _context = context;
        }
        public async Task<Dictionary<string, string>> GetSettingsAsync()
        {
            Dictionary<string, string> settings = await _context.Settingss.ToDictionaryAsync(s => s.Key, s => s.Value);
            return settings;
        }
        public async Task<Dictionary<int, string>> GetCategoryAsync()
        {
            Dictionary<int, string> category = await _context.Categories.ToDictionaryAsync(c => c.Id, c => c.Name);
            return category;
        }
    }
}
