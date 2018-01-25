import { Component, OnInit } from '@angular/core';
import {UserService} from '../../../security/user-service/user.service';
import {ExchangeRatesService} from '../../../exchange-rates/exchange-rates.service';
import {ExchangeRates} from '../../../exchange-rates/exchange-rates';
import {UserRequest} from '../../../transaction/user-request';
import {TransactionService} from '../../../transaction/service/transaction.service';
import {User} from '../../../security/user';
import {ActivatedRoute} from '@angular/router';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
export class AccountComponent implements OnInit {
  userRequest: UserRequest = new UserRequest();

  constructor(public userService: UserService,
              public transactionService: TransactionService,
              private route: ActivatedRoute) { }

  ngOnInit() {
    this.userRequest = new UserRequest();
    this.transactionService.getUserCryptoBalance().subscribe( values => this.userRequest = values);

    //this.userRequest = this.route.snapshot.data.subscribe((data) => this.userRequest = data['userRequest']);
  }

}
