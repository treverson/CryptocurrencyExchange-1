import { Injectable } from '@angular/core';
import {Router} from '@angular/router';
import {HttpClient} from '@angular/common/http';
import {UserService} from '../user-service/user.service';
import {UserCredentials} from '../user-credentials';
import {User} from '../user';
import {Observable} from 'rxjs/Observable';
import {environment} from '../../../environments/environment';
import {map} from 'rxjs/operators';

@Injectable()
export class AuthenticationService {

  constructor(private http: HttpClient,
              private router: Router,
              private userService: UserService) { }

  login(credentials: UserCredentials): Observable<User> {
    return this.http.post<any>(environment.cryptocurrencyApi + '/authentication', credentials)
                    .pipe( map(response => {
                                                      return new User(response.UserId, response.Login, response.IsAuthenticated);
                                                   } ));
  }

  logout() {
    this.userService.removeUserFromLocalStorage();
    this.router.navigateByUrl('/login');
  }
}
