import { Category } from "./Category";
import { User } from "./User";

export enum Priority {
    Low = 1,
    Medium = 2,
    High = 3
}

export interface Todo {
    id: number;
    title: string;
    description?: string;
    isCompleted: boolean;
    priority: Priority;
    
    // Handled as ISO strings or Date objects coming from JSON
    dueDate?: string | Date;
    createdDate?: string | Date;
    updatedDate?: string | Date;
    
    userId?: string; // Guid maps directly to string in TypeScript
    categoryId?: number;
    user?: User;
    category?: Category;
}