import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { lastValueFrom } from 'rxjs';
import { AccountResponseMedia } from 'src/app/common/models/accounts/accounts.model';
import { TransactionsRequestMedia } from 'src/app/common/models/transactions/transactions.model';
import { ApiService } from 'src/app/services/api/api.service';

@Component({
  selector: 'app-transaction',
  templateUrl: './transaction.component.html',
  styleUrls: ['./transaction.component.scss']
})
export class TransactionComponent implements OnInit {

  @Input() transactionType!: string;
  @Input() accounts!: AccountResponseMedia;

  @Output() onTransaction = new EventEmitter<boolean>();


  transactionForm!: FormGroup;

  constructor(private apiService: ApiService) {

  }

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {
    this.transactionForm = new FormGroup({
      accountId: new FormControl("", [Validators.required]),
      amount: new FormControl(0, [Validators.required]),
      description: new FormControl("", [Validators.required]),
    });
  }

  async onTransactionSubmit() {
    try {
      let transactionsRequestMedia: TransactionsRequestMedia;

      transactionsRequestMedia = {
        accountId: this.transactionForm.get('accountId')?.value,
        amount: this.transactionForm.get('amount')?.value,
        description: this.transactionForm.get('description')?.value,
        transactionType: this.transactionType
      };

      await lastValueFrom(this.apiService.post(
        `api/transactions`,
        transactionsRequestMedia
      ));

      this.onTransaction.emit(true);

    }
    catch (ex: any) {
      console.error(ex);
    }

  }

}
