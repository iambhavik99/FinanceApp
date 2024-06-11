import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { MasterRoutingModule } from './master-routing.module';
import { DashboardComponent } from '../dashboard/dashboard.component';

import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatSortModule } from '@angular/material/sort';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatTabsModule } from '@angular/material/tabs';
import { MatDialogModule } from '@angular/material/dialog';

import { TransactionComponent } from '../dashboard/transaction/transaction.component';
import { TransactionsPreviewComponent } from '../dashboard/transactions-preview/transactions-preview.component';
import { AddAccountComponent } from '../dashboard/add-account/add-account.component';


@NgModule({
  declarations: [
    DashboardComponent,
    TransactionComponent,
    TransactionsPreviewComponent,
    AddAccountComponent
  ],
  imports: [
    CommonModule,
    MasterRoutingModule,

    ReactiveFormsModule, FormsModule,

    MatFormFieldModule,
    MatInputModule, MatSelectModule,
    MatButtonModule, MatIconModule,
    MatTableModule, MatSortModule, MatPaginatorModule,
    MatTabsModule,
    MatDialogModule
  ]
})
export class MasterModule { }
