import { inject, Service } from '@angular/core';
import { Observable } from 'rxjs';
import { Todo } from '../models/Todo';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Service()
export class TodoService {
    private http = inject(HttpClient);
    private apiUrl = `${environment.todoApiUrl}todoitems`;

    getTodos(): Observable<Todo[]> {
        return this.http.get<Todo[]>(this.apiUrl);
    }

    createTodo(todo: Partial<Todo>): Observable<Todo> {
        return this.http.post<Todo>(this.apiUrl, todo);
    }

    updateTodo(id: number, todo: Partial<Todo>): Observable<Todo> {
        return this.http.put<Todo>(`${this.apiUrl}/${id}`, todo);
    }

    deleteTodo(id: number): Observable<void> {
        return this.http.delete<void>(`${this.apiUrl}/${id}`);
    }
}
