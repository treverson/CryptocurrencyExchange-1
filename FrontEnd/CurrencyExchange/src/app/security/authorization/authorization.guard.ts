import { Injectable } from '@angular/core';
import {CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router, CanActivateChild} from '@angular/router';
import { Observable } from 'rxjs/Observable';
import {UserService} from '../user-service/user.service';

@Injectable()
export class AuthorizationGuard implements CanActivate, CanActivateChild {

  constructor(private _userService: UserService,
              private _router: Router) {}

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {

    const authenticatedUser = this._userService.getCurrentUserFromLocalStorage();

    if (authenticatedUser) {
      return true;
    }

    this._router.navigateByUrl('');

    return false;
  }

  canActivateChild(childRoute: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
    return this.canActivate(childRoute, state);
  }



}
