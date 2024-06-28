import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AccountResponseMedia, AccountsList } from '../common/models/accounts/accounts.model';
import { AccountsService } from '../services/accounts/accounts.service';
import { GlobalService } from '../services/common/global.service';
import { AddAccountComponent } from '../dashboard/add-account/add-account.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-accounts',
  templateUrl: './accounts.component.html',
  styleUrls: ['./accounts.component.scss']
})
export class AccountsComponent implements OnInit {

  private userId = this.globalService.getUserInfoMedia().id;
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  displayedColumns: string[] = ['accountName', 'balance'];
  dataSource = new MatTableDataSource<AccountsList>();

  accountResponseMedia!: AccountResponseMedia;

  constructor(
    private matDialog: MatDialog,
    private globalService: GlobalService,
    private accountsService: AccountsService
  ) { }


  ngOnInit(): void {
    this.getAllAccounts();
  }

  getTotalCost() {
    return this.accountResponseMedia
      ?.items
      ?.map(t => t.balance).reduce((acc, value) => acc + value, 0);
  }

  addAccount() {
    this.matDialog
      .open(
        AddAccountComponent,
        {
          data: { userId: this.userId },
          width: '400px',
          autoFocus: false,
          disableClose: true
        }
      )
      .afterClosed()
      .subscribe(async result => {
        if (result == true) {
          this.getAllAccounts();
        }
      });
  }

  async getAllAccounts() {
    try {
      const response = await this.accountsService.getAccounts(this.userId!);
      this.accountResponseMedia = response;
      if (response.items.length > 0) {
        this.dataSource.data = response.items;
      }
    }
    catch (ex: any) {
      console.error(ex);
    }
  }

}
