
namespace TaskManager.Api.Models;

public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Status { get; set; } = "todo";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}