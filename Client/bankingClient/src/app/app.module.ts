import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { OpenAccountComponent } from './components/open-account/open-account.component';
import { AccountDetailsComponent } from './components/account-details/account-details.component';

@NgModule({
  declarations: [
    AppComponent,
    OpenAccountComponent,
    AccountDetailsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
