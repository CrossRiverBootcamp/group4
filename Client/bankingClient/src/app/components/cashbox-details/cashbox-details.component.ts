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
     this.per = cbox.percentages; this.dur = cbox.duration },
      err => console.log(err))
  }
  ngOnInit(): void {
  }
  public CreateCashbox(): void {
    this.router.navigateByUrl('create-cashbox', { state: { accountId: this.accountId } });
  }
}
