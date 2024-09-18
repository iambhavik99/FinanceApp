import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { GlobalService } from '../services/common/global.service';
import { TransactionResponseMedia } from '../common/models/transactions/transactions.model';
import { MatDialog } from '@angular/material/dialog';
import { AddAccountComponent } from './add-account/add-account.component';
import { AccountsService } from '../services/accounts/accounts.service';
import { TransactionsService } from '../services/transactions/transactions.service';
import { AccountMetadataMedia } from '../common/models/accounts/accounts-metadata.model';
import { TransactionDialogComponent } from './transaction-dialog/transaction-dialog.component';
import { setLineChart, setPieChart } from './charts-option';
import { AccountTransactionHistoryResponseMedia, AccountTransactionMedia } from '../common/models/accounts/accounts-history.model';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  private userId = this.globalService.getUserInfoMedia().id;

  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  displayedColumns: string[] = ['description', 'amount'];
  dataSource = new MatTableDataSource<AccountTransactionMedia>();

  accountMetadataMedia!: AccountMetadataMedia;
  accountTransactionHistoryResponseMedia!: AccountTransactionHistoryResponseMedia;


  constructor(
    private globalService: GlobalService,
    private matDialog: MatDialog,
    private accountsService: AccountsService,
    private transactionsService: TransactionsService
  ) {
    this.accountTransactionHistoryResponseMedia = new AccountTransactionHistoryResponseMedia();
    this.accountMetadataMedia = new AccountMetadataMedia();
  }

  ngOnInit() {
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;

    this.getDashboardDetails();

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
          this.getDashboardDetails();
        }
      });
  }

  onTransaction(type: string) {
    this.matDialog
      .open(
        TransactionDialogComponent,
        {
          data: { type: type },
          width: '400px',
          autoFocus: false,
          disableClose: true
        }
      )
      .afterClosed()
      .subscribe(async result => {
        if (result == true) {
          this.getDashboardDetails();
        }
      });
  }


  async getDashboardDetails() {
    try {
      const [accountMetadataMedia, accountTransactionHistoryResponseMedia] = await Promise.all([
        this.accountsService.getAccountMetadata(),
        this.accountsService.getAccountTransactionHistory()
      ]);

      this.accountMetadataMedia = accountMetadataMedia;
      this.accountTransactionHistoryResponseMedia = accountTransactionHistoryResponseMedia;

      if (this.accountTransactionHistoryResponseMedia?.items?.length) {
        this.dataSource.data = this.accountTransactionHistoryResponseMedia.items;
      }

      const dateTimeStampList = accountMetadataMedia.transactions.map(x => x.month);
      const incomeAndExpanses = accountMetadataMedia.transactions.map(x => { return { income: x.income, expanse: x.expanse } });
      const expanses = accountMetadataMedia.expanses.map(x => { return { name: x.categoryName, value: x.amount } });

      setLineChart("chart-view", dateTimeStampList, incomeAndExpanses);
      setPieChart("pie-chart-view", expanses);

    }
    catch (ex: any) {
      console.error(ex);
    }
  }



}
