import { Injectable } from '@angular/core';
import { ApiService } from '../api/api.service';
import { CategoriesResponseMedia } from 'src/app/common/models/categories/category.model';
import { lastValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CategoriesService {

  constructor(private apiService: ApiService) { }

  getCategories(): Promise<CategoriesResponseMedia> {
    return new Promise((resolve, reject) => {
      return lastValueFrom(this.apiService.get(`/api/category`))
        .then(response => resolve(response))
        .catch(err => reject(err));
    });
  }
}
