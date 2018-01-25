import { Component, OnInit } from '@angular/core';
import {Transaction} from '../transaction';
import {TransactionService} from '../service/transaction.service';
import {ActivatedRoute, ActivatedRouteSnapshot, Router} from '@angular/router';
import {NavigationService} from '../../site-navigation/service/navigation.service';

import {ExchangeRatesService} from '../../exchange-rates/exchange-rates.service';
import {ExchangeRates} from '../../exchange-rates/exchange-rates';
import {UserRequest} from '../user-request';
import {UserService} from '../../security/user-service/user.service';
import {TransactionsResolver} from '../../resolvers/transactions/transactions-resolver.service';

@Component({
  selector: 'app-transaction',
  templateUrl: './transaction.component.html',
  styleUrls: ['./transaction.component.css']
})
export class TransactionComponent implements OnInit {
  transactions: Transaction[] = [];

  constructor(private _transactionService: TransactionService,
              public navigationService: NavigationService,
              private route: ActivatedRoute) { }

  ngOnInit() {
    this.navigationService.showLoginButton = false;
    this.navigationService.showLogoutButton = true;
    this.transactions = [];
    this.transactions = this.route.snapshot.data['userhistory'];
      //this._transactionService.getTransactions().subscribe(data => this.transactions = data);


  }

}
