import { Component, OnInit } from '@angular/core';
import {UserRequest} from '../../transaction/user-request';
import {ActivatedRoute} from '@angular/router';
import {TransactionService} from '../../transaction/service/transaction.service';
import {UserService} from '../../security/user-service/user.service';

@Component({
  selector: 'app-user-account',
  templateUrl: './user-account.component.html',
  styleUrls: ['./user-account.component.css']
})
export class UserAccountComponent implements OnInit {

  userRequest: UserRequest = new UserRequest();

  constructor(public userService: UserService,
              public transactionService: TransactionService,
              private route: ActivatedRoute) { }

  ngOnInit() {
    this.userRequest = new UserRequest();
    this.transactionService.getUserCryptoBalance().subscribe( values => this.userRequest = values);

    //this.userRequest = this.route.snapshot.data.subscribe((data) => this.userRequest = data['userRequest']);
  }

}
