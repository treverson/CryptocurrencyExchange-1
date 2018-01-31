import {Component, OnInit} from '@angular/core';
import {UserRequest} from '../../transaction/user-request';
import {ExchangeRatesService} from '../../exchange-rates/exchange-rates.service';
import {ExchangeRates} from '../../exchange-rates/exchange-rates';
import {TransactionService} from '../../transaction/service/transaction.service';
import {Transaction} from '../../transaction/transaction';
import {Router} from '@angular/router';
import {OwnedCurrency} from '../../transaction/owned-currency';

@Component({
  selector: 'app-purchases',
  templateUrl: './purchases.component.html',
  styleUrls: ['./purchases.component.css']
})
export class PurchasesComponent implements OnInit {

  constructor(private exchangeRatesService: ExchangeRatesService,
              private transactionService: TransactionService,
              private router: Router) {
  }

  userRequest = new UserRequest();
  availableCurrencies: OwnedCurrency[] = [];
  userOwnedCurrencies: OwnedCurrency[] = [];
  currentPrices = new ExchangeRates();
  transaction = new Transaction();
  depositAmount: number;
  withdrawAmount: number;
  amountToSell: number;
  amountToBuy: number;
  selectedCurrencyToBuy: OwnedCurrency;
  selectedCurrencyToSell: OwnedCurrency;

  ngOnInit() {
    this.exchangeRatesService.getExchangeRates().subscribe(data => this.currentPrices = data);

    this.transactionService.getUserCryptoBalance().subscribe(data => {
      this.userRequest = data;
      this.availableCurrencies = data.OwnedCurrencies.filter(curr => curr.Name !== 'Pln');
      this.userOwnedCurrencies = data.OwnedCurrencies.filter(curr => curr.AvailableAmount > 0 && curr.Name !== 'Pln');
    });
  }

  deposit() {
    this.transaction.CurrencyName = 'Pln';
    this.transaction.Amount = this.depositAmount;
    this.transactionService.registerTransaction(this.transaction).subscribe(resp => {
      this.transaction = new Transaction();
      this.router.navigateByUrl('/transactions');
    });
  }

  withdraw() {
    this.transaction.CurrencyName = 'Pln';
    this.transaction.Amount = -this.withdrawAmount;
    this.transactionService.registerTransaction(this.transaction).subscribe(resp => {
      this.transaction = new Transaction();
      this.router.navigateByUrl('/transactions');
    }, err => alert('Insufficient funds'));
  }

  assignCurrencyExchangeRate() {

    switch (this.transaction.CurrencyName) {
    case 'Btc':
      this.transaction.ExchangeRate = this.currentPrices.BtcPrice;
    break;
    case 'Bcc':
      this.transaction.ExchangeRate = this.currentPrices.BccPrice;
    break;
    case 'Eth':
      this.transaction.ExchangeRate = this.currentPrices.EthPrice;
    break;
    case 'Ltc':
      this.transaction.ExchangeRate = this.currentPrices.LtcPrice;
    break;
  }

  }

  buy() {
    this.transaction.CurrencyName = this.selectedCurrencyToBuy.Name;
    this.transaction.Amount = this.amountToBuy;

    this.assignCurrencyExchangeRate();

    this.transaction.Fiat = -this.transaction.ExchangeRate * this.transaction.Amount;


    this.transactionService.registerBuyTransaction(this.transaction).subscribe(resp => {
      this.transaction = new Transaction();
      this.router.navigateByUrl('/transactions');
    }, err => alert('Insufficient funds'));

  }

  sell() {
    this.transaction.CurrencyName = this.selectedCurrencyToSell.Name;
    this.transaction.Amount = this.amountToSell;

    this.assignCurrencyExchangeRate();

    this.transaction.Fiat = this.transaction.ExchangeRate * this.transaction.Amount;


    this.transactionService.registerSellTransaction(this.transaction).subscribe(resp => {
      this.transaction = new Transaction();
      this.router.navigateByUrl('/transactions');
    }, err => alert('Insufficient funds'));


  }


}
