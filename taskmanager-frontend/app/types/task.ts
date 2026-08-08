// C# の TaskItemDto と同じ形の型定義（表示用・受取用）
export interface TaskItemDto {
  id: number;
  title: string;
  status: string;
  createdAt: string;
}

// C# の CreateTaskItemDto と同じ形の型定義（新規作成用）
export interface CreateTaskItemDto {
  title: string;
}
