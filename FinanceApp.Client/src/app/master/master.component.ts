import { AfterViewInit, ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BreakpointObserver } from '@angular/cdk/layout';
import { GlobalService } from '../services/common/global.service';
import { UsersService } from '../services/users/users.service';
import { UserInfoMedia } from '../common/models/users/user-info.model';
import { MatSidenav } from '@angular/material/sidenav';

@Component({
  selector: 'app-master',
  templateUrl: './master.component.html',
  styleUrls: ['./master.component.scss']
})
export class MasterComponent implements OnInit, AfterViewInit {

  public isMobile: boolean = false;
  public isCollapsed: boolean = true;
  userInfo: UserInfoMedia = new UserInfoMedia();

  @ViewChild(MatSidenav) matSidenav!: MatSidenav;

  constructor(
    private globalService: GlobalService,
    private usersService: UsersService,
    private router: Router,
    private activeRoute: ActivatedRoute,
    private observer: BreakpointObserver,
    private cdf: ChangeDetectorRef
  ) {

  }

  ngOnInit(): void {
    this.getUserInfo();
  }

  ngAfterViewInit(): void {
    this.observer.observe(['(max-width: 800px)']).subscribe((screenSize) => {
      if (screenSize.matches) {
        this.isMobile = true;
        this.isCollapsed = true;
      } else {
        this.isMobile = false;
        this.isCollapsed = false;
      }
      this.cdf.detectChanges();
    });
  }

  toggleMenu() {
    if (this.isMobile) {
      this.matSidenav.toggle();
      this.isCollapsed = false; // On mobile, the menu can never be collapsed
    } else {
      this.matSidenav.open(); // On desktop/tablet, the menu can never be fully closed
      this.isCollapsed = !this.isCollapsed;
    }
  }

  async getUserInfo() {
    try {
      const response = await this.usersService.getUserInfo();

      this.userInfo = response;
      this.globalService.setUserInfoMedia(response);

      const url = this.router.url == "/" ? "/dashboard" : this.router.url;
      this.router.navigate([url]);

    }
    catch (ex: any) {
      console.error(ex);
      this.router.navigate(['/login']);
    }
  }

}
