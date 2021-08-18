import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Options, LabelType } from 'ng5-slider';
import { first } from 'rxjs/operators';

import { AccountService, AlertService } from '@app/services';

@Component({ templateUrl: 'login.component.html' })
export class LoginComponent implements OnInit {
    formLogin: FormGroup;
    formCalculate: FormGroup;
    loading = false;
    submitted = false;
    calculated = false;

    constructor(
        private formBuilder: FormBuilder,
        private route: ActivatedRoute,
        private router: Router,
        private accountService: AccountService,
        private alertService: AlertService
    ) { }

    ngOnInit() {
        this.formLogin = this.formBuilder.group({
            username: ['', Validators.required],
            password: ['', Validators.required]
        });
        this.formCalculate = this.formBuilder.group({
            loanAmount: ['', Validators.required],
            customerTitle: ['Mr.', [Validators.required]],
            firstName: ['', [Validators.required]],
            lastName: ['', [Validators.required]],
            email: ['', [Validators.required]],
            mobile: ['', [Validators.required]],
            term: ['', [Validators.required]],
            interestRate: ['', [Validators.required]]
        });
    }
    
    customerTitles: any = [ 'Mr.', 'Mrs.', 'Ms.' ];
    
    loanAmount: number = 2000;
    highValue: number = 100000;
    options: Options = {
        floor: 2000,
        ceil: 100000,
        step: 500,
        showOuterSelectionBars: true,
        translate: (value: number, label: LabelType): string => {
            if (value == 2000 || value == 100000) return '$' + value;

            if (label == LabelType.Low)  return '<span class="lc-baloon sb">$ ' + value + '</span>';

            return '';
        }
    };
    get formLoginFields() { return this.formLogin.controls; }
    get formCalculateFields() { return this.formCalculate.controls; }

    onSubmitCalculate() {
        this.calculated = true;
        
        this.alertService.clear();
        
        if (this.formCalculate.invalid) {
            return;
        }
    }
    onSubmitLogin() {
        this.submitted = true;
        
        this.alertService.clear();
        
        if (this.formLogin.invalid) {
            return;
        }

        this.loading = true;
        this.accountService.login(this.formLoginFields.username.value, this.formLoginFields.password.value)
            .pipe(first())
            .subscribe({
                next: () => {
                    const returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
                    this.router.navigateByUrl(returnUrl);
                },
                error: error => {
                    this.alertService.error(error);
                    this.loading = false;
                }
            });
    }
}