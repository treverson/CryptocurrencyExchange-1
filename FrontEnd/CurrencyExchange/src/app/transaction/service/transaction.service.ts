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

  existingEntry: string;
  userJson: any;
  //existingEntry: string = localStorage.getItem('user');
  //userJson = JSON.parse(this.existingEntry) as User;

  getTransactions(): Observable<Transaction[]> {
    this.existingEntry = localStorage.getItem('user');
    this.userJson = JSON.parse(this.existingEntry) as User;
   // const existingEntry = localStorage.getItem('user');
   // const userJson = JSON.parse(existingEntry) as User;
    return this.http.get<Transaction[]>(environment.cryptocurrencyApi + '/UserTransactions/' + this.userJson.UserId);
  }

  getUserCryptoBalance(): Observable<UserRequest> {
   // const existingEntry = localStorage.getItem('user');
  //  const userJson = JSON.parse(existingEntry) as User;
  //  return this.http.get<UserRequest>(environment.cryptocurrencyApi + '/UserTransactions/' + this.userJson.UserId + '/balance');
    return this.http.get<UserRequest>(environment.cryptocurrencyApi + '/account/' + this.userJson.UserId );

  }

  registerTransaction(transaction: Transaction): Observable<Transaction> {
    this.existingEntry = localStorage.getItem('user');
    this.userJson = JSON.parse(this.existingEntry) as User;
    transaction.UserId = this.userJson.UserId;
    return this.http.post<Transaction>(environment.cryptocurrencyApi + '/UserTransactions/' + this.userJson.UserId + '/fiat', transaction);
  }

  BuyTransaction(transaction: Transaction): Observable<Transaction> {
    this.existingEntry = localStorage.getItem('user');
    this.userJson = JSON.parse(this.existingEntry) as User;
    transaction.UserId = this.userJson.UserId;
    return this.http.post<Transaction>(environment.cryptocurrencyApi + '/UserTransactions/' + this.userJson.UserId + '/buy', transaction);
  }

  registerSellTransaction(transaction: Transaction): Observable<Transaction> {
    this.existingEntry = localStorage.getItem('user');
    this.userJson = JSON.parse(this.existingEntry) as User;
    transaction.UserId = this.userJson.UserId;
    return this.http.post<Transaction>(environment.cryptocurrencyApi + '/UserTransactions/' + this.userJson.UserId + '/sell', transaction);
  }

}
