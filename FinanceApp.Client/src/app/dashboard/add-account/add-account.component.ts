import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { lastValueFrom } from 'rxjs';
import { AccountRequestMedia } from 'src/app/common/models/accounts/accounts.model';
import { AccountsService } from 'src/app/services/accounts/accounts.service';
import { GlobalService } from 'src/app/services/common/global.service';

@Component({
  selector: 'app-add-account',
  templateUrl: './add-account.component.html',
  styleUrls: ['./add-account.component.scss']
})
export class AddAccountComponent implements OnInit {

  accountForm!: FormGroup;

  constructor(private accountsService: AccountsService,
    private readonly globalService: GlobalService,
    public dialogRef: MatDialogRef<AddAccountComponent>
  ) { }

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {
    this.accountForm = new FormGroup({
      accountName: new FormControl("", Validators.required),
      balance: new FormControl(0, [Validators.required, Validators.min(1)])
    });
  }

  async saveAccount() {
    try {

      let accountRequestMedia: AccountRequestMedia;
      accountRequestMedia = {
        accountName: this.accountForm.get("accountName")?.value,
        balance: this.accountForm.get("balance")?.value,
        userId: this.globalService.getUserInfoMedia().id!
      }

      await this.accountsService.saveAccount(accountRequestMedia);

      this.dialogRef.close(true);

    } catch (ex: any) {
      console.error(ex);

    }
  }


}
