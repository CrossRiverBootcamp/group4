import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
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
  accountId?: Number;
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
  constructor(private router: Router, private createCashBoxService:CreateCashboxComponent) {
    const extras = this.router.getCurrentNavigation()?.extras;
    this.accountId = !!extras && !!extras.state ? extras.state['accountId'] : null;
    console.log(this.accountId);
  }
   
  savecashbox():void{
    this.createCashBoxService.createNewCashBox()
    .subscribe(a=>{console.log(a);this.tryTransfer = true;this.transferSuccess=true;},
    err=>{console.log(err);this.tryTransfer = true;this.transferSuccess=false;});
  }
  ngOnInit(): void {
  }
  createCashBox():void{
this.createFlag = true;
  }
}
