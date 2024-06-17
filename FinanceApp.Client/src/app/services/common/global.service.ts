import { Injectable } from '@angular/core';
import { UserInfoMedia } from 'src/app/common/models/users/user-info.model';

@Injectable({
  providedIn: 'root'
})
export class GlobalService {

  private userInfoMedia!: UserInfoMedia

  constructor() { }

  getUserInfoMedia(): UserInfoMedia {

    if (!this.userInfoMedia?.email?.trim()) {
      let _userInfoMedia: any = JSON.parse(localStorage.getItem('userInfoMedia') ?? '');

      if (!!_userInfoMedia?.email?.trim()) {
        this.userInfoMedia = _userInfoMedia;
      }

    }

    return this.userInfoMedia;
  }

  setUserInfoMedia(userInfoMedia: UserInfoMedia) {
    localStorage.setItem('userInfoMedia', JSON.stringify(userInfoMedia))
    this.userInfoMedia = userInfoMedia;
  }


}
