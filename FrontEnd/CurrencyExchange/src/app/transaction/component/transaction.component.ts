import { Component, OnInit } from '@angular/core';
import {Transaction} from '../../models/transaction';
import {TransactionService} from '../service/transaction.service';

@Component({
  selector: 'app-transaction',
  templateUrl: './transaction.component.html',
  styleUrls: ['./transaction.component.css']
})
export class TransactionComponent implements OnInit {

  transactions: Transaction[] = [];

  constructor(private transactionService: TransactionService) { }

  ngOnInit() {
      this.transactionService.getTransactions().subscribe(data => this.transactions = data);

  }



}
