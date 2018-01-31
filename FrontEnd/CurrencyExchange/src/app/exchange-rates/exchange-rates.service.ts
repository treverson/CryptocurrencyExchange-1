import { Injectable } from '@angular/core';
import {environment} from '../../environments/environment';
import {Observable} from 'rxjs/Observable';
import {ExchangeRates} from './exchange-rates';
import {HttpClient} from '@angular/common/http';

@Injectable()
export class ExchangeRatesService {

  constructor(private http: HttpClient) { }

  getExchangeRates(): Observable<ExchangeRates> {
    return this.http.get<ExchangeRates>(environment.cryptocurrencyApi + '/ExchangeRates');
  }

}
