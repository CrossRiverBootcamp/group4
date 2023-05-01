import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { Router } from '@angular/router';
import { cashbox } from 'src/app/models/Cashbox';
import { Duration, Percents } from 'src/app/models/Percent';
import { CashboxService } from 'src/app/services/cashbox.service';


@Component({
  selector: 'app-create-cashbox',
  templateUrl: './create-cashbox.component.html',
  styleUrls: ['./create-cashbox.component.css']
})
export class CreateCashboxComponent implements OnInit {
  accountId!: Number;
  cashbox?:cashbox;
  // @ViewChild('durat') durat!: ElementRef;
  // @ViewChild('percent') percent!: ElementRef;
  percentages: Percents[] = [
    {value: 5, viewValue: '5%'},
    {value: 10, viewValue: '10%'},
    {value: 15, viewValue: '15%'},
  ];
  selectedPercent = this.percentages[1];
  currentDate:Date = new Date(); 
  duration: Duration[] = [
    {value: 2, viewValue: '2 month'},
    {value: 4, viewValue: '4 month'},
    {value: 6, viewValue: '6 month'},
  ];
  selectedDuration = this.duration[1].value; 
  createFlag:Boolean = false;
  constructor(private router: Router, private cashBoxService:CashboxService) {
    console.log("in ctor create cashbox");
    console.log(this.router.getCurrentNavigation()?.extras);
    const extras = this.router.getCurrentNavigation()?.extras;
    this.accountId = !!extras && !!extras.state ? extras.state['accountId'] : null;
    console.log(this.accountId);
  }
   
  savecashbox():void{
    this.cashbox = {
      accountId:this.accountId,
      duration:new Date(this.currentDate.setMonth(this.currentDate.getMonth()+this.selectedDuration)),
      percentages:this.selectedPercent,
      amount:0
     };
    this.cashBoxService.createNewCashBox(this.cashbox)
    .subscribe(()=>this.router.navigateByUrl(`accountDetails/${this.accountId}`,{state: {accountId: this.accountId}}),
    err=>console.log(err));
    
  }
  ngOnInit(): void {
  }
  createCashBox():void{
this.createFlag = true;
  }
    // changeDuration(event){}
}
