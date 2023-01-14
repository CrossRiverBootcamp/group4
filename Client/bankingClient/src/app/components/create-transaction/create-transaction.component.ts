import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Transaction } from 'src/app/models/Transaction';
import { CreateTransactionService } from 'src/app/services/create-transaction.service';

@Component({
  selector: 'app-create-transaction',
  templateUrl: './create-transaction.component.html',
  styleUrls: ['./create-transaction.component.css']
})
export class CreateTransactionComponent{
  tryTransfer:boolean=false;
  transferSuccess:boolean=false;
   accountIdFrom?:Number;
   accountIdTo?:Number;
   amount?:Number;
   transaction?:Transaction;
   
  constructor(private router: Router, private createTransactionService:CreateTransactionService) {
    const extras  = this.router.getCurrentNavigation()?.extras;
    this.accountIdFrom = !!extras && !!extras.state ? extras.state['accountId'] : null;  
    console.log(this.accountIdFrom);
    
  }
  public onSubmit(loginForm: NgForm):void {
     
     this.transaction = {
      fromAccountId:this.accountIdFrom,
      toAccountId:this.accountIdTo,
      amount:this.amount
     }
     this.createTransactionService.createNewTransaction(this.transaction)
     .subscribe(a=>{console.log(a);
      this.tryTransfer = true;
      this.transferSuccess=true;},
     err=>{console.log(err);
      this.tryTransfer = true;
      this.transferSuccess=false;});
    loginForm.reset();
  }

}
