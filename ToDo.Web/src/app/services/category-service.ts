import { inject, Service } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { Category } from '../models/Category';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Service()
export class CategoryService {
    private http = inject(HttpClient);
    private apiUrl = `${environment.todoApiUrl}categories`; 

    getCategories(): Observable<Category[]> {
        return this.http.get<Category[]>(this.apiUrl);
    }
}
