import {Component, OnInit} from '@angular/core';
import {Location} from '@angular/common';
import {Router} from '@angular/router';
import {AuthenticationService} from '../../security/authentication/authentication.service';
import {UserService} from '../../security/user-service/user.service';
import {MessageService} from '../../message/service/message.service';
import {NavigationService} from '../service/navigation.service';
import {TransactionService} from '../../transaction/service/transaction.service';
import {ExchangeRatesService} from '../../exchange-rates/exchange-rates.service';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css'],

})
export class NavigationComponent implements OnInit {

  constructor(public navigationService: NavigationService,
              private _authenticationService: AuthenticationService,
              public userService: UserService,
              public transactionService: TransactionService,
              public exchangeRatesService: ExchangeRatesService,
              private _messageService: MessageService,
              private _location: Location,
              private _router: Router) {
  }

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

  history() {
    this._router.navigateByUrl('/transactions');
  }

  purchase() {
    this._router.navigateByUrl('/purchases');
  }

  buySellCryptos() {
    this._router.navigateByUrl('/cryptos');
  }
  ngOnInit() {

  }

}
