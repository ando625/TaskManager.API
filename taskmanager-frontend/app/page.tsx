"use client";

import { useEffect, useState } from "react";
import { TaskItemDto } from "./types/task";
import { getTasks, createTask, deleteTask } from "./lib/api";

export default function Home() {
  // ① ステート（画面の状態管理）の準備
  const [tasks, setTasks] = useState<TaskItemDto[]>([]);
  const [title, setTitle] = useState("");
  const [loading, setLoading] = useState(true);

  // ② 画面が初期表示された時にタスク一覧を取得する
  useEffect(() => {
    fetchTasks();
  }, []);

  const fetchTasks = async () => {
    try {
      const data = await getTasks();
      setTasks(data);
    } catch (error) {
      alert("タスクの取得に失敗しました");
    } finally {
      setLoading(false);
    }
  };

  // ③ タスク追加ボタンが押された時の処理
  const handleCreate = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!title.trim()) return;

    try {
      await createTask({ title });
      setTitle(""); // 入力欄を空にする
      fetchTasks(); // 最新の一覧を再取得
    } catch (error) {
      alert("タスクの追加に失敗しました");
    }
  };

  // ④ 削除ボタンが押された時の処理
  const handleDelete = async (id: number) => {
    try {
      await deleteTask(id);
      fetchTasks(); // 最新の一覧を再取得
    } catch (error) {
      alert("タスクの削除に失敗しました");
    }
  };

  return (
    <main className="max-w-2xl mx-auto p-8">
      <h1 className="text-3xl font-bold mb-8 text-center">TaskManager</h1>

      {/* タスク追加フォーム */}
      <form onSubmit={handleCreate} className="flex gap-2 mb-8">
        <input
          type="text"
          value={title}
          onChange={(e) => setTitle(e.target.value)}
          placeholder="新しいタスクを入力..."
          className="flex-1 border p-2 rounded text-black"
        />
        <button
          type="submit"
          className="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700"
        >
          追加
        </button>
      </form>

      {/* タスク一覧表示 */}
      {loading ? (
        <p className="text-center">読み込み中...</p>
      ) : (
        <div className="space-y-4">
          {tasks.length === 0 ? (
            <p className="text-center text-gray-500">タスクがありません</p>
          ) : (
            tasks.map((task) => (
              <div
                key={task.id}
                className="flex items-center justify-between border p-4 rounded shadow-sm"
              >
                <div>
                  <h2 className="font-semibold text-lg">{task.title}</h2>
                  <p className="text-sm text-gray-500">
                    ステータス: {task.status} | 作成日:{" "}
                    {new Date(task.createdAt).toLocaleDateString()}
                  </p>
                </div>
                <button
                  onClick={() => handleDelete(task.id)}
                  className="bg-red-500 text-white px-3 py-1 rounded hover:bg-red-600 text-sm"
                >
                  削除
                </button>
              </div>
            ))
          )}
        </div>
      )}
    </main>
  );
}
