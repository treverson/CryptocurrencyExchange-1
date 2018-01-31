import {Component, OnInit} from '@angular/core';
import {UserCredentials} from '../user-credentials';
import {AuthenticationService} from '../authentication/authentication.service';
import {UserService} from '../user-service/user.service';
import {Router} from '@angular/router';
import {MessageService} from '../../message/service/message.service';
import {NavigationService} from '../../site-navigation/service/navigation.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  userCredentials: UserCredentials = new UserCredentials();

  constructor(public navigationService: NavigationService,
              private _authenticationService: AuthenticationService,
              private _userService: UserService,
              private _messageService: MessageService,
              private _router: Router) {
  }

  ngOnInit() {
    this.navigationService.showLoginButton = false;
  }

  login() {
    this._messageService.messages = [];

    this._authenticationService.login(this.userCredentials)
      .subscribe(user => {
        if (user.IsAuthenticated) {
          this._userService.setActiveUserInLocalStorage(user);

          this._router.navigateByUrl('/transactions');
          this._messageService.messages = [];
        }
        else {
          this._messageService.addMessage('Wrong user name or password');
        }
        this.userCredentials = new UserCredentials();
      });
  }
}
