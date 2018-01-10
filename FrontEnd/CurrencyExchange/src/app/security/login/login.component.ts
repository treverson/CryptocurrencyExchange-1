import {Component, OnInit} from '@angular/core';
import {UserCredentials} from '../../models/user-credentials';
import {AuthenticationService} from '../authentication/authentication.service';
import {UserService} from '../user-service/user.service';
import {Router} from '@angular/router';
import {MessageService} from '../../message/service/message.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  userCredentials: UserCredentials = new UserCredentials();

  constructor(private authenticationService: AuthenticationService,
              private userService: UserService,
              private messageService: MessageService,
              private router: Router) {
  }

  ngOnInit() {
  }

  login() {
    this.authenticationService.login(this.userCredentials)
      .subscribe(user => {
        if (user.IsAuthenticated) {
          this.userService.setActiveUserInLocalStorage(user);
          this.router.navigateByUrl('/transactions');
        }
        else {
          this.messageService.addMessage('User ' + user.Login + ' is not authenticated');
        }
        this.userCredentials = new UserCredentials();
      });

  }
}
