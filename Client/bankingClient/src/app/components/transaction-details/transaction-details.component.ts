import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DetailsService } from 'src/app/services/details.service';

@Component({
  selector: 'app-transaction-details',
  templateUrl: './transaction-details.component.html',
  styleUrls: ['./transaction-details.component.css']
})
export class TransactionDetailsComponent implements OnInit {
  firstName!:string;
  lastName!:string;
  email!:string;
  accountId!:Number;
  constructor(private router: Router, private detailsService: DetailsService) {
    const extras = this.router.getCurrentNavigation()?.extras;
    this.accountId = !!extras && !!extras.state ? extras.state['accountId'] : null;
    console.log(this.accountId);
    this.detailsService.getCustomer(this.accountId).subscribe(
      res=> {console.log(res);
        this.firstName = res.firstName;
      this.lastName = res.lastName;
      this.email = res.email},
      err=>console.log(err)
    )
  }

  ngOnInit(): void {
  }

}
