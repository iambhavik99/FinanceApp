import { Component } from '@angular/core';
import { GlobalService } from '../services/common/global.service';

@Component({
  selector: 'app-master',
  templateUrl: './master.component.html',
  styleUrls: ['./master.component.scss']
})
export class MasterComponent {

  constructor(private globalService: GlobalService) {
    globalService.setLoggedInUserId(localStorage.getItem("userId") ?? "");
  }

}
