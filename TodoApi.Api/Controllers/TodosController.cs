using Microsoft.AspNetCore.Mvc;
using TodoApi.Api.DTOs;
using TodoApi.Api.Services;

namespace TodoApi.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodosController : ControllerBase
{
    private readonly ITodoService _todoService;
    private readonly ILogger<TodosController> _logger;

    public TodosController(ITodoService todoService, ILogger<TodosController> logger)
    {
        _todoService = todoService;
        _logger = logger;
    }

    /// <summary>
    /// Get all todos
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoResponseDto>>> GetTodos()
    {
        try
        {
            var todos = await _todoService.GetAllTodosAsync();
            return Ok(todos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching todos");
            return StatusCode(500, "An error occurred while processing your request");
        }
    }

    /// <summary>
    /// Get a specific todo by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<TodoResponseDto>> GetTodo(int id)
    {
        try
        {
            var todo = await _todoService.GetTodoByIdAsync(id);
            if (todo == null)
                return NotFound($"Todo with ID {id} not found");

            return Ok(todo);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching todo {TodoId}", id);
            return StatusCode(500, "An error occurred while processing your request");
        }
    }

    /// <summary>
    /// Create a new todo
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<TodoResponseDto>> CreateTodo(TodoCreateDto todoCreateDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var todo = await _todoService.CreateTodoAsync(todoCreateDto);
            return CreatedAtAction(nameof(GetTodo), new { id = todo.Id }, todo);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating todo");
            return StatusCode(500, "An error occurred while processing your request");
        }
    }

    /// <summary>
    /// Update an existing todo
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<TodoResponseDto>> UpdateTodo(int id, TodoUpdateDto todoUpdateDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedTodo = await _todoService.UpdateTodoAsync(id, todoUpdateDto);
            if (updatedTodo == null)
                return NotFound($"Todo with ID {id} not found");

            return Ok(updatedTodo);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating todo {TodoId}", id);
            return StatusCode(500, "An error occurred while processing your request");
        }
    }

    /// <summary>
    /// Delete a todo
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodo(int id)
    {
        try
        {
            var result = await _todoService.DeleteTodoAsync(id);
            if (!result)
                return NotFound($"Todo with ID {id} not found");

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting todo {TodoId}", id);
            return StatusCode(500, "An error occurred while processing your request");
        }
    }

    /// <summary>
    /// Get todos by completion status
    /// </summary>
    [HttpGet("status/{isCompleted}")]
    public async Task<ActionResult<IEnumerable<TodoResponseDto>>> GetTodosByStatus(bool isCompleted)
    {
        try
        {
            var todos = await _todoService.GetTodosByStatusAsync(isCompleted);
            return Ok(todos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching todos by status");
            return StatusCode(500, "An error occurred while processing your request");
        }
    }

    /// <summary>
    /// Get todos by category
    /// </summary>
    [HttpGet("category/{category}")]
    public async Task<ActionResult<IEnumerable<TodoResponseDto>>> GetTodosByCategory(string category)
    {
        try
        {
            var todos = await _todoService.GetTodosByCategoryAsync(category);
            return Ok(todos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching todos by category");
            return StatusCode(500, "An error occurred while processing your request");
        }
    }

    /// <summary>
    /// Mark a todo as completed
    /// </summary>
    [HttpPatch("{id}/complete")]
    public async Task<ActionResult<TodoResponseDto>> CompleteTodo(int id)
    {
        try
        {
            var updateDto = new TodoUpdateDto { IsCompleted = true };
            var updatedTodo = await _todoService.UpdateTodoAsync(id, updateDto);
            
            if (updatedTodo == null)
                return NotFound($"Todo with ID {id} not found");

            return Ok(updatedTodo);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while completing todo {TodoId}", id);
            return StatusCode(500, "An error occurred while processing your request");
        }
    }

    /// <summary>
    /// Mark a todo as incomplete
    /// </summary>
    [HttpPatch("{id}/incomplete")]
    public async Task<ActionResult<TodoResponseDto>> IncompleteTodo(int id)
    {
        try
        {
            var updateDto = new TodoUpdateDto { IsCompleted = false };
            var updatedTodo = await _todoService.UpdateTodoAsync(id, updateDto);
            
            if (updatedTodo == null)
                return NotFound($"Todo with ID {id} not found");

            return Ok(updatedTodo);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while marking todo as incomplete {TodoId}", id);
            return StatusCode(500, "An error occurred while processing your request");
        }
    }
}
