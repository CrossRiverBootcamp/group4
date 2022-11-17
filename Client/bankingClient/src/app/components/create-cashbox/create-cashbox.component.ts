import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { cashbox } from 'src/app/models/Cashbox';
import { CreateCashboxService } from 'src/app/services/create-cashbox.service';

interface Percents {
  value: Number;
  viewValue: string;
}
interface Duration {
  value: Number;
  viewValue: string;
}
@Component({
  selector: 'app-create-cashbox',
  templateUrl: './create-cashbox.component.html',
  styleUrls: ['./create-cashbox.component.css']
})
export class CreateCashboxComponent implements OnInit {
  accountId!: Number;
  cashbox?:cashbox;
  @ViewChild('durat') durat!: ElementRef;
  @ViewChild('percent') percent!: ElementRef;
  percentages: Percents[] = [
    {value: 5, viewValue: '5%'},
    {value: 10, viewValue: '10%'},
    {value: 15, viewValue: '15%'},
  ];
  duration: Duration[] = [
    {value: 2, viewValue: '2 month'},
    {value: 4, viewValue: '4 month'},
    {value: 6, viewValue: '6 month'},
  ];
  createFlag:Boolean = false;
  constructor(private router: Router, private createCashBoxService:CreateCashboxService) {
    console.log("in ctor create cashbox");
    console.log(this.router.getCurrentNavigation()?.extras);
    const extras = this.router.getCurrentNavigation()?.extras;
    this.accountId = !!extras && !!extras.state ? extras.state['accountId'] : null;
    console.log(this.accountId);
  }
   
  savecashbox():void{
    this.cashbox = {
      accountId:this.accountId,
      duration:this.durat.nativeElement.value,
      percentages:this.percent.nativeElement.value
     };
    this.createCashBoxService.createNewCashBox(this.cashbox)
    .subscribe(()=>this.router.navigateByUrl(`accountDetails/${this.accountId}`,{state: {accountId: this.accountId}}),
    err=>console.log(err));
    
  }
  ngOnInit(): void {
  }
  createCashBox():void{
this.createFlag = true;
  }
}
