using WebApplication2.Models;
using Microsoft.EntityFrameworkCore;
namespace WebApplication2.Services
{
    public class TodoService : ITodoService
    {
        private readonly AppDbContext _db;
        public TodoService(AppDbContext db) { _db = db; }

        public async Task<TodoItem?> CreateAsync(CreateTodoDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Title))
                return null;
            var newItem = new TodoItem { Title = dto.Title, IsCompleted = dto.IsCompleted };
            _db.TodoItems.Add(newItem);
            await _db.SaveChangesAsync();
            return newItem;
        }

        public async Task<List<TodoItem>> GetAllAsync()
        {
            return await _db.TodoItems.ToListAsync();
        }

        public async Task<TodoItem?> GetByIdAsync(int id)
        {
            var item = await _db.TodoItems.FirstOrDefaultAsync(t => t.Id == id);
            return item;
        }

        public async Task<bool?> UpdateAsync(UpdateTodoDto dto, int id)
        {
            if (string.IsNullOrWhiteSpace(dto.Title))
                return null;
            var existItem = await _db.TodoItems.FirstOrDefaultAsync(t => t.Id == id);
            if (existItem == null)
            {
                return false;
            }
            existItem.Title = dto.Title;
            existItem.IsCompleted = dto.IsCompleted;
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existItem = await _db.TodoItems.FirstOrDefaultAsync(t => t.Id == id);
            if (existItem == null)
                return false;
            _db.TodoItems.Remove(existItem);
            await _db.SaveChangesAsync();
            return true;
        }

    }
}
