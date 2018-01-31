import { Injectable } from '@angular/core';
import {ActivatedRouteSnapshot, Resolve, Router, RouterStateSnapshot} from '@angular/router';
import {Transaction} from '../../transaction/transaction';
import {TransactionService} from '../../transaction/service/transaction.service';
import {Observable} from 'rxjs/Observable';

@Injectable()
export class TransactionsResolver implements Resolve<Transaction[]>{

  constructor(private _transactionService: TransactionService,
              private _router: Router) {
  }

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Transaction[] | Observable<Transaction[]> {
    return this._transactionService.getTransactions();
  }
}
