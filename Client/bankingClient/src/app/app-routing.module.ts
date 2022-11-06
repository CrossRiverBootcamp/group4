import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { AccountDetailsComponent } from './components/account-details/account-details.component';
import { CreateTransactionComponent } from './components/create-transaction/create-transaction.component';
import { historyComponent } from './components/history/history.component';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { OpenAccountComponent } from './components/open-account/open-account.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { TransactionDetailsComponent } from './components/transaction-details/transaction-details.component';


const routes: Routes = [
  {
    path: 'home',
    component: HomeComponent
  },
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'signup',
    component: OpenAccountComponent
  },
  {
    path: 'accountDetails/:id',
    component: AccountDetailsComponent
  },
  {
    path: 'newTransaction',
    component: CreateTransactionComponent
  },
  {
    path: 'history',
    component: historyComponent
  },
  {
    path: 'details',
    component: TransactionDetailsComponent
  },
  {
    path: '**',
    component: PageNotFoundComponent,
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }