using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Dtos;
using TaskManager.Api.Services;

namespace TaskManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskItemsController : ControllerBase
{
    private readonly ITaskItemService _taskItemService;

    // コンストラクター（Program.cs の道具箱から ITaskItemService を注入してもらう）
    public TaskItemsController(ITaskItemService taskItemService)
    {
        _taskItemService = taskItemService;
    }

    // ① タスク一覧取得 API: GET /api/taskitems
    [HttpGet]
    public async Task<ActionResult<List<TaskItemDto>>> GetAll()
    {
        var tasks = await _taskItemService.GetAllAsync();
        return Ok(tasks);
    }

    // ② タスク新規作成 API: POST /api/taskitems
    [HttpPost]
    public async Task<ActionResult<TaskItemDto>> Create(CreateTaskItemDto dto)
    {
        var createdTask = await _taskItemService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetAll), new { id = createdTask.Id }, createdTask);
    }

    // ③ タスク削除 API: DELETE /api/taskitems/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _taskItemService.DeleteAsync(id);
        if (!result)
        {
            return NotFound(); // 該当するタスクが存在しない場合は 404 Not Found を返す
        }

        return NoContent(); // 削除成功時は 204 No Content を返す
    }
}