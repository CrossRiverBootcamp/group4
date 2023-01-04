import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module'
import { AppComponent } from './app.component';
import { OpenAccountComponent } from './components/open-account/open-account.component';
import { AccountDetailsComponent } from './components/account-details/account-details.component';
import { LoginComponent } from './components/login/login.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FlexLayoutModule } from '@angular/flex-layout';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';
import { MatMenuModule } from '@angular/material/menu';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatSelectModule } from '@angular/material/select';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatOptionModule } from '@angular/material/core';
import { RouterTestingModule } from '@angular/router/testing';
import { CreateTransactionComponent } from './components/create-transaction/create-transaction.component';
import { historyComponent } from './components/history/history.component';
import { TransactionDetailsComponent } from './components/transaction-details/transaction-details.component';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { VerificationDialogComponent } from './components/verification-dialog/verification-dialog.component';
import { MatDialogModule } from '@angular/material/dialog';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { HomeComponent } from './components/home/home.component';
import { CreateCashboxComponent } from './components/create-cashbox/create-cashbox.component';
import { CashboxDetailsComponent } from './components/cashbox-details/cashbox-details.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    OpenAccountComponent,
    AccountDetailsComponent,
    CreateTransactionComponent,
    historyComponent,
    TransactionDetailsComponent,
    VerificationDialogComponent,
    PageNotFoundComponent,
    HomeComponent,
    CreateCashboxComponent,
    CashboxDetailsComponent,
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FlexLayoutModule,
    FormsModule,
    ReactiveFormsModule,
    MatToolbarModule,
    MatInputModule,
    MatCardModule,
    MatMenuModule,
    MatIconModule,
    MatSidenavModule,
    MatButtonModule,
    MatTableModule,
    MatSlideToggleModule,
    MatSelectModule,
    MatOptionModule,
    RouterTestingModule,
    MatPaginatorModule,
    MatCheckboxModule,
    MatDialogModule
  ],
  entryComponents: [
    VerificationDialogComponent
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }