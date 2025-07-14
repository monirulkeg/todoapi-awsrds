using TodoApi.Api.DTOs;
using TodoApi.Api.Models;

namespace TodoApi.Tests;

public class TodoModelTests
{
    [Fact]
    public void Todo_ShouldCreateWithDefaultValues()
    {
        // Arrange & Act
        var todo = new Todo();

        // Assert
        Assert.False(todo.IsCompleted);
        Assert.Equal("Medium", todo.Priority);
        Assert.NotEqual(default(DateTime), todo.CreatedAt);
    }

    [Fact]
    public void TodoCreateDto_ShouldHaveRequiredProperties()
    {
        // Arrange & Act
        var dto = new TodoCreateDto
        {
            Title = "Test Todo",
            Description = "Test Description",
            Priority = "High",
            Category = "Testing"
        };

        // Assert
        Assert.Equal("Test Todo", dto.Title);
        Assert.Equal("Test Description", dto.Description);
        Assert.Equal("High", dto.Priority);
        Assert.Equal("Testing", dto.Category);
    }

    [Fact]
    public void TodoUpdateDto_ShouldAllowPartialUpdates()
    {
        // Arrange & Act
        var dto = new TodoUpdateDto
        {
            Title = "Updated Title",
            IsCompleted = true
        };

        // Assert
        Assert.Equal("Updated Title", dto.Title);
        Assert.True(dto.IsCompleted);
        Assert.Null(dto.Description);
    }

    [Fact]
    public void TodoResponseDto_ShouldMapCorrectly()
    {
        // Arrange
        var now = DateTime.UtcNow;
        
        // Act
        var dto = new TodoResponseDto
        {
            Id = 1,
            Title = "Test Todo",
            Description = "Test Description",
            IsCompleted = true,
            CreatedAt = now,
            Priority = "High",
            Category = "Testing"
        };

        // Assert
        Assert.Equal(1, dto.Id);
        Assert.Equal("Test Todo", dto.Title);
        Assert.Equal("Test Description", dto.Description);
        Assert.True(dto.IsCompleted);
        Assert.Equal(now, dto.CreatedAt);
        Assert.Equal("High", dto.Priority);
        Assert.Equal("Testing", dto.Category);
    }
}