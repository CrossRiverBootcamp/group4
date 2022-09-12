import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { AccountDetailsComponent } from './components/account-details/account-details.component';
import { CreateTransactionComponent } from './components/create-transaction/create-transaction.component';
import { historyComponent } from './components/history/history.component';
import { LoginComponent } from './components/login/login.component';
import { OpenAccountComponent } from './components/open-account/open-account.component';
import { TransactionDetailsComponent } from './components/transaction-details/transaction-details.component';


const routes: Routes = [
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
    path: 'transactionDetails',
    component: TransactionDetailsComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
