import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterModule, Routes} from '@angular/router';
import {LoginComponent} from '../security/login/login.component';
import {TransactionComponent} from '../transaction/component/transaction.component';

const routes: Routes = [
  //{path: ''},
  // {path: 'login', component: LoginComponent}
  {path: 'login', component: LoginComponent},
  {path: 'transactions', component: TransactionComponent}
]


@NgModule({
  imports: [
    RouterModule.forRoot(routes),
    CommonModule
  ],
  exports: [RouterModule],
  declarations: []
})
export class AppRoutingModule { }
