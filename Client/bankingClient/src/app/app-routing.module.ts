import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { AccountDetailsComponent } from './components/account-details/account-details.component';
import { CreateTransactionComponent } from './components/create-transaction/create-transaction.component';
import { LoginComponent } from './components/login/login.component';
import { OpenAccountComponent } from './components/open-account/open-account.component';


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
    path: 'NewTransaction',
    component: CreateTransactionComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
