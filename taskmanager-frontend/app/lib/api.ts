import { TaskItemDto, CreateTaskItemDto } from "../types/task";

// C# API の URL（ポート番号が 5018 以外だった場合はその数字に変更してください）
const API_BASE_URL = "http://localhost:5018/api/TaskItems";

// ① タスク一覧を取得する関数 (GET)
export async function getTasks(): Promise<TaskItemDto[]> {
  const response = await fetch(API_BASE_URL);
  if (!response.ok) {
    throw new Error("タスク一覧の取得に失敗しました");
  }
  return response.json();
}

// ② タスクを新規作成する関数 (POST)
export async function createTask(dto: CreateTaskItemDto): Promise<TaskItemDto> {
  const response = await fetch(API_BASE_URL, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(dto),
  });
  if (!response.ok) {
    throw new Error("タスクの作成に失敗しました");
  }
  return response.json();
}

// ③ タスクを削除する関数 (DELETE)
export async function deleteTask(id: number): Promise<void> {
  const response = await fetch(`${API_BASE_URL}/${id}`, {
    method: "DELETE",
  });
  if (!response.ok) {
    throw new Error("タスクの削除に失敗しました");
  }
}
