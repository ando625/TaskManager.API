using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Data;
using TaskManager.Api.Dtos;
using TaskManager.Api.Models;

namespace TaskManager.Api.Services;

public class TaskItemService : ITaskItemService
{
    private readonly ApplicationDbContext _context;

    // コンストラクター（DbContextをDIコンテナから注入してもらう）
    public TaskItemService(ApplicationDbContext context)
    {
        _context = context;
    }

    // ① タスク一覧の取得処理
    public async Task<List<TaskItemDto>> GetAllAsync()
    {
        return await _context.TaskItems
            .Select(t => new TaskItemDto
            {
                Id = t.Id,
                Title = t.Title,
                Status = t.Status,
                CreatedAt = t.CreatedAt
            })
            .ToListAsync();
    }

    // ② タスクの新規作成処理
    public async Task<TaskItemDto> CreateAsync(CreateTaskItemDto dto)
    {
        // DTO から DB用の Model に詰め替える
        var taskItem = new TaskItem
        {
            Title = dto.Title,
            Status = "todo",
            CreatedAt = DateTime.UtcNow
        };

        _context.TaskItems.Add(taskItem);
        await _context.SaveChangesAsync();

        // 完成した Model を 画面用の DTO に詰め替えて返す
        return new TaskItemDto
        {
            Id = taskItem.Id,
            Title = taskItem.Title,
            Status = taskItem.Status,
            CreatedAt = taskItem.CreatedAt
        };
    }

    // ③ タスクの削除処理
    public async Task<bool> DeleteAsync(int id)
    {
        var taskItem = await _context.TaskItems.FindAsync(id);
        if (taskItem == null)
        {
            return false; // 対象のタスクが見つからなかった
        }

        _context.TaskItems.Remove(taskItem);
        await _context.SaveChangesAsync();
        return true; // 削除成功
    }
}