import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { NavigationComponent } from './site-navigation/component/navigation.component';
import {HttpClientModule} from '@angular/common/http';
import { LoginComponent } from './security/login/login.component';
import {MessageComponent} from './message/component/message.component';
import {AppRoutingModule} from './app-routing/app-routing.module';
import {FormsModule} from '@angular/forms';
import {MessageService} from './message/service/message.service';
import {AuthenticationService} from './security/authentication/authentication.service';
import {UserService} from './security/user-service/user.service';
import { TransactionComponent } from './transaction/component/transaction.component';
import {TransactionService} from './transaction/service/transaction.service';
import {RouterModule} from '@angular/router';
import {NavigationService} from './site-navigation/service/navigation.service';
import {RegisterComponent} from './security/register/component/register.component';
import {RegisterService} from './security/register/service/register.service';
import {ExchangeRatesService} from './exchange-rates/exchange-rates.service';
import { PurchasesComponent } from './purchases/purchases/purchases.component';
import { ExchangeRatesComponent } from './exchange-rates/component/exchange-rates.component';
import {TransactionsResolver} from './resolvers/transactions/transactions-resolver.service';
import { UserAccountComponent } from './user-account/user-account/user-account.component';

@NgModule({
  declarations: [
    AppComponent,
    NavigationComponent,
    LoginComponent,
    MessageComponent,
    TransactionComponent,
    RegisterComponent,
    PurchasesComponent,
    ExchangeRatesComponent,
    UserAccountComponent,
      ],
  imports: [
    RouterModule,
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    FormsModule
  ],
  providers: [
    NavigationService,
    MessageService,
    AuthenticationService,
    TransactionService,
    UserService,
    RegisterService,
    ExchangeRatesService,
    TransactionsResolver
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
