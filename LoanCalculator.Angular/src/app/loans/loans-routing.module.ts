import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { LoanLayoutComponent } from './loan-layout.component';
import { ListLoansComponent } from './list-loans.component';
import { SubmitLoanComponent } from './submit-loan.component';

const routes: Routes = [
    {
        path: '', component: LoanLayoutComponent,
        children: [
            { path: '', component: ListLoansComponent },
            { path: 'add', component: SubmitLoanComponent },
            { path: 'edit/:id', component: SubmitLoanComponent }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class LoansRoutingModule { }