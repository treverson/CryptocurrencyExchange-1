import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
import {UserCredentials} from '../../user-credentials';
import {environment} from '../../../../environments/environment';

@Injectable()
export class RegisterService {

  constructor(private _http: HttpClient) { }

  register(credentials: UserCredentials): Observable<UserCredentials> {
    return this._http.post<UserCredentials>(environment.cryptocurrencyApi + '/registration', credentials);
  }

}
