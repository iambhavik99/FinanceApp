import { Injectable } from '@angular/core';
import { ApiService } from '../api/api.service';
import { lastValueFrom } from 'rxjs';
import { UserInfoMedia } from 'src/app/common/models/users/user-info.model';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  constructor(private apiService: ApiService) { }

  getUserInfo(): Promise<UserInfoMedia> {
    return new Promise((resolve, reject) => {
      return lastValueFrom(this.apiService.get('/api/users/info'))
        .then(response => resolve(response))
        .catch(err => reject(err));
    });
  }

}
