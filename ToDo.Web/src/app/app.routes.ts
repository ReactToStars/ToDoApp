import { Routes } from '@angular/router';
import { TodoList } from './components/todo-list/todo-list';

export const routes: Routes = [
	{ path: '', pathMatch: 'full', redirectTo: 'todos' },
	{ path: 'todos', component: TodoList, title: 'To-do list' },
	{ path: '**', redirectTo: 'todos' },
];
