import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RouterModule, Routes} from '@angular/router';
import {LoginComponent} from '../security/login/login.component';
import {TransactionComponent} from '../transaction/component/transaction.component';
import {AuthorizationGuard} from '../security/authorization/authorization.guard';
import {RegisterComponent} from '../security/register/component/register.component';
import {AccountComponent} from '../account/component/account/account.component';
import {PurchasesComponent} from '../purchases/purchases/purchases.component';
import {TransactionsResolver} from '../resolvers/transactions/transactions-resolver.service';

const routes: Routes = [
  {path: 'transactions', component: TransactionComponent, resolve: {userhistory: TransactionsResolver}},
  {path: 'login', component: LoginComponent },
  {path: 'register', component: RegisterComponent},
  {path: 'account', component: AccountComponent},
  {path: 'purchases', component: PurchasesComponent}
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
