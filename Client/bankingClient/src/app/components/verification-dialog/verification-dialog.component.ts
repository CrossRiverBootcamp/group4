import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroupDirective, NgForm } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
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
      async success => {console.log("get the id of new account");
       console.log(success);
      await this.router.navigateByUrl('create-cashbox',{state: {accountId: success}});
    this.dialogRef.close()}
      ,err=>{console.log(err);GlobalConstants.validCode = false;console.log('this.validCode:',GlobalConstants.validCode);
      this.code ='';}
    );
  }
  onSubmitAgain(){
    this.openService.emailVerificationAgain(this.dialogRef.componentInstance.data.customer.email).subscribe(
      success => {console.log(success)}
      ,err=>alert(err)
    );
  }
  matcher = new MyErrorStateMatcher();
}
//1 to make variable global 2. to make validCode:boolean part of the form validation.
export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    return !GlobalConstants.validCode;
  }
}
export class GlobalConstants {
  public static validCode: boolean = true;
}
