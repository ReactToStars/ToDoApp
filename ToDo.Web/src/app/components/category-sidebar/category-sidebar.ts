import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Category } from '../../models/Category';

@Component({
  imports: [],
  selector: 'app-category-sidebar',
  styleUrl: './category-sidebar.css',
  templateUrl: './category-sidebar.html',
})
export class CategorySidebar {
  @Input() categories: Category[] = [];

  @Output() categorySelected = new EventEmitter<number | null>();

  selectedCategoryId: number | null = null;

  selectCategory(categoryId: number | null): void {
    this.selectedCategoryId = categoryId;
    this.categorySelected.emit(categoryId);
  }
}
