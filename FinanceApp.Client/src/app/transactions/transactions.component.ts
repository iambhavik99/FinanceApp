import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { Transaction, TransactionResponseMedia } from '../common/models/transactions/transactions.model';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { TransactionsService } from '../services/transactions/transactions.service';
import { PaginationModel } from '../common/models/pagination.model';

@Component({
  selector: 'app-transactions',
  templateUrl: './transactions.component.html',
  styleUrls: ['./transactions.component.scss']
})
export class TransactionsComponent implements OnInit, AfterViewInit {

  displayedColumns: string[] = ['transactionDate', 'description', 'amount'];
  dataSource = new MatTableDataSource<Transaction>();

  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  transactionResponseMedia = new TransactionResponseMedia();
  paginationModel!: PaginationModel


  constructor(
    private transactionsService: TransactionsService
  ) {
    this.paginationModel = new PaginationModel();
    this.paginationModel.pageIndex = 0;
    this.paginationModel.pageSize = 10;
    this.paginationModel.sortDirection = 'asc';
    this.paginationModel.sortBy = 'transactionDate';
  }


  ngOnInit(): void {
    this.getAllTransactions();
  }

  ngAfterViewInit(): void {
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
  }

  sortData($event: Sort) {
    this.paginationModel.sortBy = $event.active;
    this.paginationModel.sortDirection = $event.direction;
    this.getAllTransactions();
  }

  onPageChange($event: PageEvent) {
    this.paginationModel.pageIndex = $event.pageIndex;
    this.paginationModel.pageSize = $event.pageSize;
    this.getAllTransactions();
  }


  async getAllTransactions() {
    try {
      const response = await this.transactionsService.getTransactions(this.paginationModel);
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
