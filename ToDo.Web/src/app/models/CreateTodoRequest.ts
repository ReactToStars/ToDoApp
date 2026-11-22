import { Priority } from "./Todo";

export interface CreateTodoRequest {
  title: string;
  description: string;
  categoryId: number | null;
  priority: Priority;
  IsCompleted: boolean;
}
