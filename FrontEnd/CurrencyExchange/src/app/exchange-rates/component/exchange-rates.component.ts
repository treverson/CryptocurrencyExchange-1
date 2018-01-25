import { Component, OnInit } from '@angular/core';
import {ExchangeRatesService} from '../exchange-rates.service';
import {ExchangeRates} from '../exchange-rates';
import {UserService} from '../../security/user-service/user.service';

@Component({
  selector: 'app-exchange-rates',
  templateUrl: './exchange-rates.component.html',
  styleUrls: ['./exchange-rates.component.css']
})
export class ExchangeRatesComponent implements OnInit {

  exchangeRates: ExchangeRates = new ExchangeRates();

  constructor(public exchangeRatesService: ExchangeRatesService,
              public userService: UserService) { }

  ngOnInit() {
    this.exchangeRatesService.getExchangeRates().subscribe(data => this.exchangeRates = data);
  }

}
