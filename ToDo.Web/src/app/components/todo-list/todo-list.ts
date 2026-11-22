import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { TodoService } from '../../services/todo-service';
import { Todo } from '../../models/Todo';
import { CategorySidebar } from '../category-sidebar/category-sidebar';
import { TodoForm } from '../todo-form/todo-form';
import { Category } from '../../models/Category';
import { CategoryService } from '../../services/category-service';
import { TodoItem } from '../todo-item/todo-item';

@Component({
  imports: [CategorySidebar, TodoForm, TodoItem],
  selector: 'app-todo-list',
  styleUrl: './todo-list.css',
  templateUrl: './todo-list.html',
})
export class TodoList implements OnInit {
  private todoService = inject(TodoService);
  private categoryService = inject(CategoryService);

   // Core application state using Angular Signals
  categories = signal<Category[]>([]); 
  todos = signal<Todo[]>([]);
  selectedCategory = signal<number | null>(null);

  // Deriving filtered lists dynamically
  filteredTodos = computed(() => {
    const categoryId = this.selectedCategory();
    return categoryId ? this.todos().filter(todo => todo.categoryId === categoryId) : this.todos();
  });

  ngOnInit(): void {
    this.loadTodos();
    this.loadCategories();
  }

  loadCategories(): void {
    this.categoryService.getCategories().subscribe((categories) => {
      this.categories.set(categories);
    });
  }
    
  loadTodos(): void {
    this.todoService.getTodos().subscribe((todos) => {
      this.todos.set(todos);
    });
  }

  handleCreateTodo(newTodo: Partial<Todo>): void {
    this.todoService.createTodo(newTodo).subscribe((createdTodo) => {
      this.todos.update(currentTodos => [...currentTodos, createdTodo]);
    });
  }
  
  handleToggleTodo(id: number, updatedFields: Partial<Todo>): void {
    this.todoService.updateTodo(id, updatedFields).subscribe((updatedTodo) => {
      this.todos.update(currentTodos => currentTodos.map(todo => todo.id === id ? updatedTodo : todo));
    });
  }

  handleDeleteTodo(id: number): void {
    this.todoService.deleteTodo(id).subscribe(() => {
      this.todos.update(currentTodos => currentTodos.filter(todo => todo.id !== id));
    });
  }

  handleCategorySelect(categoryId: number | null): void {
    this.selectedCategory.set(categoryId);
  }
}
