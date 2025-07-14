using System.ComponentModel.DataAnnotations;

namespace TodoApi.Api.DTOs;

public class TodoCreateDto
{
    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [StringLength(1000)]
    public string? Description { get; set; }

    [StringLength(50)]
    public string Priority { get; set; } = "Medium";

    [StringLength(100)]
    public string? Category { get; set; }
}

public class TodoUpdateDto
{
    [StringLength(200)]
    public string? Title { get; set; }

    [StringLength(1000)]
    public string? Description { get; set; }

    public bool? IsCompleted { get; set; }

    [StringLength(50)]
    public string? Priority { get; set; }

    [StringLength(100)]
    public string? Category { get; set; }
}

public class TodoResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public string Priority { get; set; } = "Medium";
    public string? Category { get; set; }
}
