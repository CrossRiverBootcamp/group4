import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { cashbox } from 'src/app/models/Cashbox';
import { Percents } from 'src/app/models/Percent';
import { CashboxService } from 'src/app/services/cashbox.service';

@Component({
  selector: 'app-cashbox-details',
  templateUrl: './cashbox-details.component.html',
  styleUrls: ['./cashbox-details.component.css']
})
export class CashboxDetailsComponent implements OnInit {
  accountId!: Number;
  cashboxExists!: Boolean;
  cashbox?: cashbox;
  per?: Percents;
  dur?: Date;
  amount?:Number;
  constructor(private router: Router, private cashboxService: CashboxService) {
    const extras = this.router.getCurrentNavigation()?.extras;
    this.accountId = !!extras && !!extras.state ? extras.state['accountId'] : null;
    this.cashboxService.checkCashboxExists(this.accountId).subscribe(
      flag => {
        this.cashboxExists = flag;
        if (this.cashboxExists)
          this.getCashboxFunc();
      }, err => console.log(err))

  }
  getCashboxFunc(): void {
    this.cashboxService.getCashboxInfo(this.accountId).subscribe(cbox => {console.log(cbox);
     this.per = cbox.percentages; this.dur = cbox.duration;this.amount = cbox.amount },
      err => console.log(err))
  }
  ngOnInit(): void {
  }
  public CreateCashbox(): void {
    this.router.navigateByUrl('create-cashbox', { state: { accountId: this.accountId } });
  }
  public closeCashBox(): void {
    var c = confirm("are you sure you want to close your cashbox?")
    if(c)
      this.cashboxService.closeCashbox(this.accountId).subscribe(flag => {
         if(flag) alert("you closed your cashbox successfully!")
         else alert("there was a problem with closing the cashbox, the action failed")
         this.router.navigateByUrl(`accountDetails/${this.accountId}`,{state: {accountId: this.accountId}});},
       err => alert("there was an error while closing the cashbox: "+err))
  }
}
