import {Component, OnInit} from '@angular/core';
import {Location} from '@angular/common';
import {Router} from '@angular/router';
import {AuthenticationService} from '../../security/authentication/authentication.service';
import {UserService} from '../../security/user-service/user.service';
import {MessageService} from '../../message/service/message.service';
import {NavigationService} from '../service/navigation.service';
import {TransactionService} from '../../transaction/service/transaction.service';
import {UserRequest} from '../../transaction/user-request';
import {ExchangeRatesService} from '../../exchange-rates/exchange-rates.service';
import {ExchangeRates} from '../../exchange-rates/exchange-rates';
import {OwnedCurrency} from '../../transaction/owned-currency';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css'],

})
export class NavigationComponent implements OnInit {

  constructor(public navigationService: NavigationService,
              private _authenticationService: AuthenticationService,
              public _userService: UserService,
              public transactionService: TransactionService,
              public exchangeRatesService: ExchangeRatesService,
              private _messageService: MessageService,
              private _location: Location,
              private _router: Router) {
  }


  userRequest = new UserRequest();
  exchangeRates = new ExchangeRates();
  fiat = new OwnedCurrency();// = this.userRequest.OwnedCurrencies[0];

  //fiatBalance = this.userRequest.OwnedCurrencies[0];

  login() {
    //this.navigationService.showLoginButton = false;
    this._router.navigateByUrl('/login');
  }

  logout() {
    //this.navigationService.showLoginButton = true;
    //this.navigationService.showLogoutButton = false;
    this._authenticationService.logout();
  }

  register() {
    this._router.navigateByUrl('/register');
  }

  ngOnInit() {
    // this.navigationService.showLoginButton = true;
    this.transactionService.getUserCryptoBalance().subscribe(data => {
      this.userRequest = data;
     // this.fiat = this.userRequest.OwnedCurrencies;
    });
    this.exchangeRatesService.getExchangeRates().subscribe(data => this.exchangeRates = data);
  }

}
