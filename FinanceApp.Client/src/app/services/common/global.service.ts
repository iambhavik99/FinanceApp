import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class GlobalService {

  private userId!: string;

  constructor() { }

  getLoggedInUserId() {
    return this.userId;
  }

  setLoggedInUserId(userId: string) {
    this.userId = userId;
  }


}
