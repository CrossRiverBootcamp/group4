import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Transaction } from 'src/app/models/Transaction';
import { CreateTransactionService } from 'src/app/services/create-transaction.service';

@Component({
  selector: 'app-create-transaction',
  templateUrl: './create-transaction.component.html',
  styleUrls: ['./create-transaction.component.css']
})
export class CreateTransactionComponent implements OnInit {

   formValid=true;
   accountIdFrom?:Number;
   accountIdTo?:Number;
   amount?:Number;
   transaction?:Transaction;
   
  constructor(private createTransactionService:CreateTransactionService,private router: Router) {
    
   }

  ngOnInit(): void {
    const state = this.router.getCurrentNavigation().extras.state;
    this.accountIdFrom = state['accountId'];
}
  
  public onSubmit():void {
     this.formValid = true;
     this.transaction = {
      accountIdFrom:this.accountIdFrom,
      accountIdTo:this.accountIdTo,
      amount:this.amount
     }
     this.createTransactionService.createNewTransaction(this.transaction)
     .subscribe(a=>console.log(a),
     err=>console.log(err));
  }

  
    ngOnDestroy() {
      this.sub.unsubscribe();
    }
}
