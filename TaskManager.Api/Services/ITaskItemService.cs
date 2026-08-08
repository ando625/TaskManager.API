//インターフェースの約束事

using TaskManager.Api.Dtos;

namespace TaskManager.Api.Services;

public interface ITaskItemService
{
    // タスク一覧を取得する約束　非同期処
    Task<List<TaskItemDto>> GetAllAsync();

    //タスク１件を新規作成する約束
    Task<TaskItemDto> CreateAsync(CreateTaskItemDto dto);

    // タスク１件を削除する約束
    Task<bool> DeleteAsync(int id);

}