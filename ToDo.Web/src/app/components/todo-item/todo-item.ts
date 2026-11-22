import { Component, EventEmitter, inject, Input, Output } from '@angular/core';
import { Priority, Todo } from '../../models/Todo';

@Component({
  imports: [],
  selector: 'app-todo-item',
  styleUrl: './todo-item.css',
  templateUrl: './todo-item.html',
})
export class TodoItem {
  @Input({required: true}) todo!: Todo;

  @Output() toggle = new EventEmitter<{ id: number; updatedFields: Partial<Todo> }>();

  @Output() deleted = new EventEmitter<number>();

  @Output() edit = new EventEmitter<Partial<Todo>>();
  
  Priority = Priority;

  handleToggle(): void{
    this.toggle.emit({ id: this.todo.id, updatedFields: this.todo });
  }

  handleDelete(): void{
    this.deleted.emit(this.todo.id);
  }
}
