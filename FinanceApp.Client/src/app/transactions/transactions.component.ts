import { Component, OnInit, ViewChild } from '@angular/core';
import { Transaction, TransactionResponseMedia } from '../common/models/transactions/transactions.model';
import { MatTableDataSource } from '@angular/material/table';
import { GlobalService } from '../services/common/global.service';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { TransactionsService } from '../services/transactions/transactions.service';

@Component({
  selector: 'app-transactions',
  templateUrl: './transactions.component.html',
  styleUrls: ['./transactions.component.scss']
})
export class TransactionsComponent implements OnInit {

  private userId = this.globalService.getUserInfoMedia().id;
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  displayedColumns: string[] = ['transactionDate', 'description', 'amount'];
  dataSource = new MatTableDataSource<Transaction>();

  transactionResponseMedia!: TransactionResponseMedia;

  constructor(
    private globalService: GlobalService,
    private transactionsService: TransactionsService
  ) {
    this.getAllTransactions();
  }


  ngOnInit(): void {

  }

  async getAllTransactions() {
    try {
      const response = await this.transactionsService.getTransactions();
      this.transactionResponseMedia = response;
      if (response.items.length > 0) {
        this.dataSource.data = response.items;
      }
    }
    catch (ex: any) {
      console.error(ex);
    }
  }

}
