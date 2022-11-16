import { Component, Inject, OnInit } from '@angular/core';
import {MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Customer } from 'src/app/models/customer';
import { OpenAccountService } from 'src/app/services/open-account.service';
@Component({
  selector: 'app-verification-dialog',
  templateUrl: './verification-dialog.component.html',
  styleUrls: ['./verification-dialog.component.css']
})
export class VerificationDialogComponent implements OnInit {

  constructor(@Inject(MAT_DIALOG_DATA) public data: any,private openService:OpenAccountService,private dialogRef: MatDialogRef<VerificationDialogComponent>,private router: Router) { 
    dialogRef.disableClose = true;
    console.log(data)
  }
  customer?:Customer;
  code:string='';
  ngOnInit(): void {
  }
  onSubmit(){
    console.log(this.dialogRef.componentInstance.data.customer);
    this.customer={
      firstName: this.dialogRef.componentInstance.data.customer.firstName,
      lastName:this.dialogRef.componentInstance.data.customer.lastName,
      email:this.dialogRef.componentInstance.data.customer.email,
      password:this.dialogRef.componentInstance.data.customer.password,
      verificationCode:this.code
    }
    //change open account func in server to return the id!!!!!!!!!
    this.openService.openAccount(this.customer).subscribe(
      success => {console.log(success)}
      ,err=>console.log(err)
    );

    
  }
  onSubmitAgain(){
    this.openService.emailVerificationAgain(this.dialogRef.componentInstance.data.customer.email).subscribe(
      success => {console.log(success)}
      ,err=>alert(err)
    );
  }
}
