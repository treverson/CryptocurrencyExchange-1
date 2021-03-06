import { Injectable } from '@angular/core';
import {User} from '../user';

@Injectable()
export class UserService {

  constructor() { }

  setActiveUserInLocalStorage(user: User) {
    const userJson = JSON.stringify(user);
    localStorage.setItem('user', userJson);
  }

  getCurrentUserFromLocalStorage() {
    const userJson = localStorage.getItem('user');
    if (userJson) {
      return JSON.parse(userJson) as User;
    }
    return null;
  }

  removeUserFromLocalStorage() {
    localStorage.removeItem('user');
  }
}
