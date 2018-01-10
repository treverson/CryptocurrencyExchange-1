import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';


import { AppComponent } from './app.component';
import { NavigationComponent } from './site-navigation/navigation/navigation.component';
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



@NgModule({
  declarations: [
    AppComponent,
    NavigationComponent,
    LoginComponent,
    MessageComponent,
    TransactionComponent,
      ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    FormsModule
  ],
  providers: [
    MessageService,
    AuthenticationService,
    TransactionService,
    UserService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
