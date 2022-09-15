import { Component, Inject, OnInit } from '@angular/core';
import {MAT_DIALOG_DATA} from '@angular/material/dialog';
import { Customer } from 'src/app/models/Customer';
import { OpenAccountService } from 'src/app/services/open-account.service';
@Component({
  selector: 'app-verification-dialog',
  templateUrl: './verification-dialog.component.html',
  styleUrls: ['./verification-dialog.component.css']
})
export class VerificationDialogComponent implements OnInit {

  constructor(@Inject(MAT_DIALOG_DATA) public data: Customer,private openService:OpenAccountService) { }
  code?:string;
  ngOnInit(): void {
  }
  onSubmit(){
    // this.registrationValid=true;
    // this.customer={
    //   firstName: this.firstName,
    //   lastName:this.lastName,
    //   email:this.email,
    //   password:this.password,
    //   verificationCode:this.verificationCode
    // }
    console.log(this.data);
    // this.data.VerificationCode = this.code!;
    this.openService.openAccount(this.data,this.code!).subscribe(
      success => {console.log(success)}
      ,err=>console.log(err)
    );
  }
}
