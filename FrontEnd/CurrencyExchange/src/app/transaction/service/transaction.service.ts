import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
import {Transaction} from '../../models/transaction';
import {environment} from '../../../environments/environment';
import {User} from '../../models/user';

@Injectable()
export class TransactionService {

  constructor(private http: HttpClient) { }

  getTransactions(): Observable<Transaction[]> {
    const existingEntry: string = localStorage.getItem('user');
    const userJson = JSON.parse(existingEntry) as User;

    return this.http.get<Transaction[]>(environment.cryptocurrencyApi + '/UserTransactions/' + userJson.UserId);
  }

}
