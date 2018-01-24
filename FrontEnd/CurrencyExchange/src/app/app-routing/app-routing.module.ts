import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RouterModule, Routes} from '@angular/router';
import {LoginComponent} from '../security/login/login.component';
import {TransactionComponent} from '../transaction/component/transaction.component';
import {AuthorizationGuard} from '../security/authorization/authorization.guard';
import {RegisterComponent} from '../security/register/component/register.component';

const routes: Routes = [
  {path: 'transactions', component: TransactionComponent, canActivate: [AuthorizationGuard], canActivateChild: [AuthorizationGuard],
    children:
      [{path: ':id', redirectTo: 'transactions' }]},
  {path: 'login', component: LoginComponent },
  {path: 'register', component: RegisterComponent}

];

@NgModule({
  imports: [
    RouterModule.forRoot(routes),
    CommonModule
  ],
  providers: [AuthorizationGuard],
  exports: [RouterModule],
  declarations: []
})
export class AppRoutingModule {
}
