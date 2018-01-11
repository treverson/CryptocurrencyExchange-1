import { Component, OnInit } from '@angular/core';
import {Transaction} from '../transaction';
import {TransactionService} from '../service/transaction.service';
import {Router} from '@angular/router';
import {NavigationService} from '../../site-navigation/service/navigation.service';

@Component({
  selector: 'app-transaction',
  templateUrl: './transaction.component.html',
  styleUrls: ['./transaction.component.css']
})
export class TransactionComponent implements OnInit {
  transactions: Transaction[] = [];

  constructor(private _transactionService: TransactionService,
              public navigationService: NavigationService,
              router: Router) { }

  ngOnInit() {
    this.navigationService.showLogoutButton = true;
      this._transactionService.getTransactions().subscribe(data => this.transactions = data);
  }

}
