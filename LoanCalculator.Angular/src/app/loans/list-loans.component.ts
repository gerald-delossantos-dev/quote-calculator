import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';

@Component({ templateUrl: 'list-loans.component.html' })
export class ListLoansComponent implements OnInit {
    users = null;

    constructor() {}

    ngOnInit() {
        
    }
}