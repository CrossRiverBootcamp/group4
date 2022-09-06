import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { filter, Subject, take, takeUntil } from 'rxjs';
import { Login } from 'src/app/interfaces/Login';
import { AccountDetailsService } from 'src/app/services/account-details.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  public loginValid = true;
  public email = '';
  public password = '';
  public accountId?:Number;
  public login?:Login;

  constructor(private accountService: AccountDetailsService,private router: Router ){}


  public onSubmit(): void {
    this.loginValid = true;
    this.login = 
    {
      email:this.email,
      password: this.password
    }
    this.accountService.Login(this.login!).subscribe(
        account => {
          this.accountId = account;
          this.loginValid = true;
          console.log(this.accountId);
          this.router.navigateByUrl(`/accountDetails/${this.accountId}`);
        },
        error=> {this.loginValid = false;
          console.log(error);}
      );
    
  }
}
