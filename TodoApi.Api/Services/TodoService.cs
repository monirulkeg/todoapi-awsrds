using Microsoft.EntityFrameworkCore;
using TodoApi.Api.Data;
using TodoApi.Api.DTOs;
using TodoApi.Api.Models;

namespace TodoApi.Api.Services;

public class TodoService : ITodoService
{
    private readonly TodoDbContext _context;

    public TodoService(TodoDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TodoResponseDto>> GetAllTodosAsync()
    {
        var todos = await _context.Todos
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();

        return todos.Select(MapToResponseDto);
    }

    public async Task<TodoResponseDto?> GetTodoByIdAsync(int id)
    {
        var todo = await _context.Todos.FindAsync(id);
        return todo == null ? null : MapToResponseDto(todo);
    }

    public async Task<TodoResponseDto> CreateTodoAsync(TodoCreateDto todoCreateDto)
    {
        var todo = new Todo
        {
            Title = todoCreateDto.Title,
            Description = todoCreateDto.Description,
            Priority = todoCreateDto.Priority,
            Category = todoCreateDto.Category,
            CreatedAt = DateTime.UtcNow
        };

        _context.Todos.Add(todo);
        await _context.SaveChangesAsync();

        return MapToResponseDto(todo);
    }

    public async Task<TodoResponseDto?> UpdateTodoAsync(int id, TodoUpdateDto todoUpdateDto)
    {
        var todo = await _context.Todos.FindAsync(id);
        if (todo == null)
            return null;

        // Only update provided fields
        if (!string.IsNullOrEmpty(todoUpdateDto.Title))
            todo.Title = todoUpdateDto.Title;

        if (todoUpdateDto.Description != null)
            todo.Description = todoUpdateDto.Description;

        if (!string.IsNullOrEmpty(todoUpdateDto.Priority))
            todo.Priority = todoUpdateDto.Priority;

        if (todoUpdateDto.Category != null)
            todo.Category = todoUpdateDto.Category;

        if (todoUpdateDto.IsCompleted.HasValue)
        {
            var wasCompleted = todo.IsCompleted;
            todo.IsCompleted = todoUpdateDto.IsCompleted.Value;

            // Set completion timestamp when marking as completed
            if (!wasCompleted && todo.IsCompleted)
                todo.CompletedAt = DateTime.UtcNow;
            else if (wasCompleted && !todo.IsCompleted)
                todo.CompletedAt = null;
        }

        todo.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return MapToResponseDto(todo);
    }

    public async Task<bool> DeleteTodoAsync(int id)
    {
        var todo = await _context.Todos.FindAsync(id);
        if (todo == null)
            return false;

        _context.Todos.Remove(todo);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<TodoResponseDto>> GetTodosByStatusAsync(bool isCompleted)
    {
        var todos = await _context.Todos
            .Where(t => t.IsCompleted == isCompleted)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();

        return todos.Select(MapToResponseDto);
    }

    public async Task<IEnumerable<TodoResponseDto>> GetTodosByCategoryAsync(string category)
    {
        var todos = await _context.Todos
            .Where(t => t.Category == category)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();

        return todos.Select(MapToResponseDto);
    }

    private static TodoResponseDto MapToResponseDto(Todo todo)
    {
        return new TodoResponseDto
        {
            Id = todo.Id,
            Title = todo.Title,
            Description = todo.Description,
            IsCompleted = todo.IsCompleted,
            CreatedAt = todo.CreatedAt,
            UpdatedAt = todo.UpdatedAt,
            CompletedAt = todo.CompletedAt,
            Priority = todo.Priority,
            Category = todo.Category
        };
    }
}
