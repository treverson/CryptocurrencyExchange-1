import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
import {Transaction} from '../transaction';
import {environment} from '../../../environments/environment';
import {User} from '../../security/user';
import {UserRequest} from '../user-request';

@Injectable()
export class TransactionService {

  constructor(private http: HttpClient) {
  }

  existingEntry: string;
  userJson: any;

  getUserFromLocalStorage() {
    this.existingEntry = localStorage.getItem('user');
    this.userJson = JSON.parse(this.existingEntry) as User;
  }


  getTransactions(): Observable<Transaction[]> {
    this.getUserFromLocalStorage();

    return this.http.get<Transaction[]>(environment.cryptocurrencyApi + '/UserTransactions/' + this.userJson.UserId);
  }

  getUserCryptoBalance(): Observable<UserRequest> {

    return this.http.get<UserRequest>(environment.cryptocurrencyApi + '/account/' + this.userJson.UserId);

  }

  registerTransaction(transaction: Transaction): Observable<Transaction> {

    this.getUserFromLocalStorage();

    transaction.UserId = this.userJson.UserId;
    return this.http.post<Transaction>(environment.cryptocurrencyApi + '/UserTransactions/' + this.userJson.UserId + '/fiat', transaction);
  }

  registerBuyTransaction(buyTransaction: Transaction): Observable<Transaction> {
    this.getUserFromLocalStorage();

    buyTransaction.UserId = this.userJson.UserId;
    return this.http.post<Transaction>(
      environment.cryptocurrencyApi + '/UserTransactions/' + this.userJson.UserId + '/buy', buyTransaction);
  }

  registerSellTransaction(sellTransaction: Transaction): Observable<Transaction> {
    this.getUserFromLocalStorage();

    sellTransaction.UserId = this.userJson.UserId;
    return this.http.post<Transaction>(
      environment.cryptocurrencyApi + '/UserTransactions/' + this.userJson.UserId + '/sell', sellTransaction);
  }

}
