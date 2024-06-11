import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ApiService } from '../services/api/api.service';
import { GlobalService } from '../services/common/global.service';
import { AccountResponseMedia } from '../common/models/accounts/accounts.model';
import { lastValueFrom } from 'rxjs';
import { Transaction, TransactionResponseMedia } from '../common/models/transactions/transactions.model';
import { MatDialog } from '@angular/material/dialog';
import { AddAccountComponent } from './add-account/add-account.component';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  private userId = this.globalService.getLoggedInUserId();

  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  displayedColumns: string[] = ['description', 'amount'];
  dataSource = new MatTableDataSource<Transaction>();

  accountResponseMedia!: AccountResponseMedia;
  transactionResponseMedia!: TransactionResponseMedia;


  constructor(private apiService: ApiService,
    private globalService: GlobalService,
    private matDialog: MatDialog
  ) {
    this.transactionResponseMedia = new TransactionResponseMedia();
  }

  ngOnInit() {
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;

    this.getAccountsList();
  }

  getTotalBalance() {
    return this.accountResponseMedia?.items
      ?.map(item => item.balance)
      ?.reduce((prev, current) => prev + current, 0);
  }


  addAccount() {
    this.matDialog
      .open(
        AddAccountComponent,
        {
          data: {
            userId: this.userId
          },
          width: '400px',
          autoFocus: false,
          disableClose: true
        }
      )
      .afterClosed()
      .subscribe(async result => {
        if (result == true) {
          this.getAccountsList();
        }
      })
  }


  async getAccountsList() {
    try {

      const response = await lastValueFrom(this.apiService.get(`api/accounts?userId=${this.userId}`));
      this.accountResponseMedia = response;

      if (this.accountResponseMedia?.items?.length == 0) {
        return;
      }

      this.transactionResponseMedia.items = [];

      const accountIds = this.accountResponseMedia.items.map(x => x.accountId);
      const allTransactions = await Promise.all([
        ...accountIds.map(x => lastValueFrom(this.apiService.get(`api/transactions?userId=${x}`)))
      ])

      allTransactions.forEach(transactions => {
        for (let transaction of transactions.items) {
          this.transactionResponseMedia.items
            .push({ ...transaction })
        }
      })

      if (this.transactionResponseMedia?.items?.length) {
        this.dataSource.data = this.transactionResponseMedia.items;
      }

    }
    catch (ex: any) {
      console.error(ex);
    }
  }



}
