import { Component, OnInit } from '@angular/core';
import { Transaction } from 'src/app/interfaces/Transaction';
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
   
  constructor(private createTransactionService:CreateTransactionService) { }

  ngOnInit(): void {
  }

  public onSubmit():void {
     
  }

}
