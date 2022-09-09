import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
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
   
  constructor(private createTransactionService:CreateTransactionService,private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.accountIdFrom = this.route
    .data
    .subscribe(v => console.log(v)
    ,err=>console.log(err)
    );
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
