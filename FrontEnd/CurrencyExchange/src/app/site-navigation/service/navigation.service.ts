import { Injectable } from '@angular/core';

@Injectable()
export class NavigationService {

  public showLoginButton: boolean;
  public showLogoutButton: boolean;

  constructor() { }
}
