import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { DashboardRoutingModule } from './dashboard-routing.module';

import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSelectModule } from '@angular/material/select';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { TextFieldModule } from '@angular/cdk/text-field';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';

import { DashboardComponent } from './dashboard.component';
import { AddAccountComponent } from './add-account/add-account.component';
import { TransactionDialogComponent } from './transaction-dialog/transaction-dialog.component';
import { TimeDiffPipe } from '../pipe/time-diff.pipe';


@NgModule({
  declarations: [
    DashboardComponent,
    AddAccountComponent,
    TransactionDialogComponent,
    TimeDiffPipe
  ],
  imports: [
    CommonModule,
    DashboardRoutingModule,
    ReactiveFormsModule, FormsModule,

    MatFormFieldModule,
    MatInputModule, MatSelectModule, TextFieldModule,
    MatTableModule, MatSortModule, MatPaginatorModule,
    MatDialogModule,
    MatButtonModule, MatIconModule,
  ]
})
export class DashboardModule { }
