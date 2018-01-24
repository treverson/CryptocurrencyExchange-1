import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
import {Transaction} from '../transaction';
import {environment} from '../../../environments/environment';
import {User} from '../../security/user';
import {UserRequest} from '../user-request';
import {ExchangeRates} from '../../exchange-rates/exchange-rates';

@Injectable()
export class TransactionService {

  constructor(private http: HttpClient) { }

  existingEntry: string = localStorage.getItem('user');
  userJson = JSON.parse(this.existingEntry) as User;

  getTransactions(): Observable<Transaction[]> {

    return this.http.get<Transaction[]>(environment.cryptocurrencyApi + '/UserTransactions/' + this.userJson.UserId + '/transactions');
  }

  getUserCryptoBalance(): Observable<UserRequest> {
    return this.http.get<UserRequest>(environment.cryptocurrencyApi + '/UserTransactions/' + this.userJson.UserId + '/balance');
  }



}
