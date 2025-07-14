using System.ComponentModel.DataAnnotations;

namespace TodoApi.Api.Models;

public class Todo
{
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [StringLength(1000)]
    public string? Description { get; set; }

    public bool IsCompleted { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    [StringLength(50)]
    public string Priority { get; set; } = "Medium";

    [StringLength(100)]
    public string? Category { get; set; }
}
