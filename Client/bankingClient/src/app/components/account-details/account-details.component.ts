import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountDetailsService } from 'src/app/services/account-details.service';

@Component({
  selector: 'app-account-details',
  templateUrl: './account-details.component.html',
  styleUrls: ['./account-details.component.css']
})
export class AccountDetailsComponent implements OnInit{
  accountId!:Number; 
  public balance:Number=0;
  public email:string='';
  public firstName:string='';
  public lastName:string='';
  public openDate:Date = new Date();

  constructor(private accountService: AccountDetailsService, private activatedroute: ActivatedRoute,private router: Router) { 
  }
  ngOnInit(){
    this.activatedroute.params.subscribe(params=> {
      this.accountId = parseInt(params['id'])}
      ,err=>console.log(err)
      );
    console.log(this.accountId);
    this.accountService.getAccountInfo(this.accountId).
    subscribe(account=>{
      console.log(account);
      this.balance = account.balance;
      this.email = account.email;
      this.firstName = account.firstName;
      this.lastName = account.lastName;
      this.openDate = account.openDate;
    },err=>console.log(err)
    );
}
public routeTocreateTransaction():void{
  this.router.navigateByUrl('newTransaction',{state: {accountId: this.accountId}});
}
public routeToHistory():void{
  this.router.navigateByUrl('history',{state: {accountId: this.accountId}});
}
public watchCashbox():void{
  this.router.navigateByUrl('view-cashbox',{state: {accountId: this.accountId}});
}
}
