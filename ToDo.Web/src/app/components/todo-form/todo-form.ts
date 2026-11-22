import { Component, EventEmitter, inject, Output } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Priority, Todo } from '../../models/Todo';

@Component({
  imports: [ReactiveFormsModule],
  selector: 'app-todo-form',
  styleUrl: './todo-form.css',
  templateUrl: './todo-form.html',
})
export class TodoForm {
  private fb = inject(FormBuilder);

  // Send the valid form state out to the parent component
  @Output() todoCreated = new EventEmitter<Partial<Todo>>();

  // Establish form structure and initial state validations
  todoForm = this.fb.group({
    title: this.fb.nonNullable.control('', [
      Validators.required,
      Validators.minLength(3),
      Validators.maxLength(100)
    ]),

    description: [''],

    categoryId: this.fb.control<number | null>(1, [
      Validators.required
    ]),
    priority: this.fb.control<number | null>(Priority.Low, [
      Validators.required
    ]),
    IsCompleted: this.fb.control<boolean>(false)
  });

  onSubmit(): void{
    if(this.todoForm.invalid){
      this.todoForm.markAllAsTouched(); // Highlight all invalid fields for user feedback
      return;
    }

    // Emit structural form values over to parent Component
    this.todoCreated.emit(this.todoForm.getRawValue() as Partial<Todo>);

    // Reset layout state, fallback back safely to standard baseline configuration
    this.resetForm();
  }

  private resetForm(): void {
     this.todoForm.reset({
      title: '',
      description: '',
      categoryId: 1,
      priority: Priority.Low,
      IsCompleted: false
    });
  }
}
