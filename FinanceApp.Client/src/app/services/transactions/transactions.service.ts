import { Injectable } from '@angular/core';
import { TransactionResponseMedia, TransactionsRequestMedia } from 'src/app/common/models/transactions/transactions.model';
import { ApiService } from '../api/api.service';
import { lastValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TransactionsService {

  constructor(private apiService: ApiService) { }

  getTransactions(limit: number = 10): Promise<TransactionResponseMedia> {
    return new Promise((resolve, reject) => {
      return lastValueFrom(this.apiService.get(`/api/transactions?limit=${limit}`))
        .then(response => resolve(response))
        .catch(err => reject(err));
    });
  }

  saveTransaction(transactionsRequestMedia: TransactionsRequestMedia): Promise<void> {
    return new Promise((resolve, reject) => {
      return lastValueFrom(this.apiService.post('/api/transactions', transactionsRequestMedia))
        .then(response => resolve(response))
        .catch(err => reject(err));
    });
  }

}
