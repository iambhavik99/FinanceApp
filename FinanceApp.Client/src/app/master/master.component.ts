import { Component, OnInit } from '@angular/core';
import { GlobalService } from '../services/common/global.service';
import { UsersService } from '../services/users/users.service';
import { Router } from '@angular/router';
import { UserInfoMedia } from '../common/models/users/user-info.model';

@Component({
  selector: 'app-master',
  templateUrl: './master.component.html',
  styleUrls: ['./master.component.scss']
})
export class MasterComponent implements OnInit {


  userInfo: UserInfoMedia = new UserInfoMedia();

  constructor(
    private globalService: GlobalService,
    private usersService: UsersService,
    private router: Router
  ) {

  }

  ngOnInit(): void {
    this.getUserInfo();

  }

  async getUserInfo() {
    try {
      const response = await this.usersService.getUserInfo();

      this.userInfo = response;
      this.globalService.setUserInfoMedia(response);
      this.router.navigate(['/dashboard']);

    }
    catch (ex: any) {
      console.error(ex);
      this.router.navigate(['/login']);
    }
  }

}
