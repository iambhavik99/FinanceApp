import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { AccountResponseMedia } from 'src/app/common/models/accounts/accounts.model';
import { CategoriesResponseMedia } from 'src/app/common/models/categories/category.model';
import { TransactionsRequestMedia } from 'src/app/common/models/transactions/transactions.model';
import { AccountsService } from 'src/app/services/accounts/accounts.service';
import { CategoriesService } from 'src/app/services/categoies/categories.service';
import { GlobalService } from 'src/app/services/common/global.service';
import { TransactionsService } from 'src/app/services/transactions/transactions.service';

@Component({
  selector: 'app-transaction-dialog',
  templateUrl: './transaction-dialog.component.html',
  styleUrls: ['./transaction-dialog.component.scss']
})
export class TransactionDialogComponent implements OnInit {

  transactionForm!: FormGroup;

  accountResponseMedia!: AccountResponseMedia;
  categoriesResponseMedia!: CategoriesResponseMedia;

  constructor(
    public dialogRef: MatDialogRef<TransactionDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private categoriesService: CategoriesService,
    private transactionsService: TransactionsService,
    private accountsService: AccountsService,
    private globalService: GlobalService
  ) { }


  ngOnInit(): void {
    this.transactionForm = new FormGroup({
      accountId: new FormControl("", Validators.required),
      categoryId: new FormControl("", Validators.required),
      amount: new FormControl(0, Validators.required),
      note: new FormControl("", Validators.required),
    });

    this.getAccountsAndCategories();
  }

  async getAccountsAndCategories() {
    try {
      const userId = this.globalService.getUserInfoMedia().id!;

      const [accountResponseMedia, categoriesResponseMedia] = await Promise.all([
        this.accountsService.getAccounts(userId),
        this.categoriesService.getCategories()
      ])

      this.accountResponseMedia = accountResponseMedia;
      this.categoriesResponseMedia = categoriesResponseMedia;
    }
    catch (ex: any) {
      console.error(ex);
    }
  }

  async saveTransaction() {
    try {
      const transactionsRequestMedia: TransactionsRequestMedia = {
        accountId: this.transactionForm.get('accountId')?.value,
        categoryId: this.transactionForm.get('categoryId')?.value,
        amount: this.transactionForm.get('amount')?.value,
        note: this.transactionForm.get('note')?.value,
        transactionType: this.data.type,
      };

      await this.transactionsService.saveTransaction(transactionsRequestMedia);

      this.dialogRef.close(true);

    }
    catch (ex: any) {
      console.error(ex);
    }


  }



}
