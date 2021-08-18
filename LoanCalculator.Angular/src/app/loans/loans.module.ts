import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { LoansRoutingModule } from './loans-routing.module';
import { LoanLayoutComponent } from './loan-layout.component';
import { ListLoansComponent } from './list-loans.component';
import { SubmitLoanComponent } from './submit-loan.component';

@NgModule({
    imports: [
        CommonModule,
        ReactiveFormsModule,
        LoansRoutingModule
    ],
    declarations: [
        LoanLayoutComponent,
        ListLoansComponent,
        SubmitLoanComponent
    ]
})
export class LoansModule { }