import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { OpenAccountComponent } from './components/open-account/open-account.component';

const routes: Routes = [
  {path:'Register', component:OpenAccountComponent},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
