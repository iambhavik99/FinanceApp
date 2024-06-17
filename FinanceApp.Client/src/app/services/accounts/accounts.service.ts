import { Injectable } from '@angular/core';
import { ApiService } from '../api/api.service';
import { AccountMetadataMedia } from 'src/app/common/models/accounts/accounts-metadata.model';
import { lastValueFrom } from 'rxjs';
import { AccountRequestMedia, AccountResponseMedia } from 'src/app/common/models/accounts/accounts.model';
import { AccountTransactionHistoryResponseMedia } from 'src/app/common/models/accounts/accounts-history.model';

@Injectable({
  providedIn: 'root'
})
export class AccountsService {

  constructor(private apiService: ApiService) { }


  getAccounts(userId: string): Promise<AccountResponseMedia> {
    return new Promise((resolve, reject) => {
      return lastValueFrom(this.apiService.get('/api/accounts?userId=' + userId))
        .then(response => resolve(response))
        .catch(err => reject(err));
    });
  }

  getAccountMetadata(): Promise<AccountMetadataMedia> {
    return new Promise((resolve, reject) => {
      return lastValueFrom(this.apiService.get('/api/accounts/metadata'))
        .then(response => resolve(response))
        .catch(err => reject(err));
    });
  }

  getAccountTransactionHistory(): Promise<AccountTransactionHistoryResponseMedia> {
    return new Promise((resolve, reject) => {
      return lastValueFrom(this.apiService.get(`/api/transactions/history`))
        .then(response => resolve(response))
        .catch(err => reject(err));
    });
  }

  saveAccount(accountRequestMedia: AccountRequestMedia): Promise<void> {
    return new Promise((resolve, reject) => {
      return lastValueFrom(this.apiService.post('/api/accounts', accountRequestMedia))
        .then(response => resolve(response))
        .catch(err => reject(err));
    });
  }
}
