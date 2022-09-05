import {BrowserModule} from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { OpenAccountComponent } from './components/open-account/open-account.component';
import { AccountDetailsComponent } from './components/account-details/account-details.component';
//import { LoginComponent } from './components/login/login.component';
import { NgModule } from '@angular/core';

@NgModule({
  declarations: [
    AppComponent,
    OpenAccountComponent,
    AccountDetailsComponent,
   // LoginComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
