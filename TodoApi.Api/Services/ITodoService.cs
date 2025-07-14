using TodoApi.Api.DTOs;
using TodoApi.Api.Models;

namespace TodoApi.Api.Services;

public interface ITodoService
{
    Task<IEnumerable<TodoResponseDto>> GetAllTodosAsync();
    Task<TodoResponseDto?> GetTodoByIdAsync(int id);
    Task<TodoResponseDto> CreateTodoAsync(TodoCreateDto todoCreateDto);
    Task<TodoResponseDto?> UpdateTodoAsync(int id, TodoUpdateDto todoUpdateDto);
    Task<bool> DeleteTodoAsync(int id);
    Task<IEnumerable<TodoResponseDto>> GetTodosByStatusAsync(bool isCompleted);
    Task<IEnumerable<TodoResponseDto>> GetTodosByCategoryAsync(string category);
}
