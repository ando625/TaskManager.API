namespace TaskManager.Api.Dtos;

public class CreateTaskItemDto
{
    public string Title { get; set; } = string.Empty;

}

public class TaskItemDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Status { get; set; } = "todo";
    public DateTime CreatedAt { get; set; }
}