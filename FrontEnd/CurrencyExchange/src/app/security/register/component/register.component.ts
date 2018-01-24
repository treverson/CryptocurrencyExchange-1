import { Component, OnInit } from '@angular/core';
import {UserCredentials} from '../../user-credentials';
import {RegisterService} from '../service/register.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  userCredentials = new UserCredentials();

  constructor(private _registerService: RegisterService,
              private _router: Router) { }

  ngOnInit() {

  }

  register() {
    if (this.userCredentials.Login && this.userCredentials.Password != null) {
      this._registerService.register(this.userCredentials).subscribe(response => {
        this.userCredentials = new UserCredentials();
        this._router.navigateByUrl('/login');
      }, err => alert('User login taken!')); }
    }
}
