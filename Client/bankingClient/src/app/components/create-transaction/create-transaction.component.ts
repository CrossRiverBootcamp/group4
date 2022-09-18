import { Component, OnInit } from '@angular/core';
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
   formValid=true;
   accountIdFrom?:Number;
   accountIdTo?:Number;
   amount?:Number;
   transaction?:Transaction;
   
  constructor(private router: Router, private createTransactionService:CreateTransactionService) {
    const extras  = this.router.getCurrentNavigation()?.extras;
    this.accountIdFrom = !!extras && !!extras.state ? extras.state['accountId'] : null;  
    console.log(this.accountIdFrom);
    
  }
  public onSubmit():void {
     this.formValid = true;
     this.tryTransfer = true;
     this.transaction = {
      fromAccountId:this.accountIdFrom,
      toAccountId:this.accountIdTo,
      amount:this.amount
     }
     this.createTransactionService.createNewTransaction(this.transaction)
     .subscribe(a=>{console.log(a);this.transferSuccess=true;},
     err=>console.log(err));
     this.clearForm();
  }
  public clearForm():void{
    this.accountIdTo=undefined;
    this.amount=undefined;
    this.formValid=false;
  }

}
