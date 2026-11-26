using WebApplication2.Models;
namespace WebApplication2.Services
{
    public interface ITodoService
    {
        Task<List<TodoItem>> GetAllAsync();
        Task<TodoItem?> GetByIdAsync(int id);
        Task<TodoItem> CreateAsync(CreateTodoDto dto);
        Task<bool?> UpdateAsync(UpdateTodoDto dto, int id);
        Task<bool> DeleteAsync(int id);
    }
}
