import { Component, OnInit } from '@angular/core'
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Customer } from 'src/app/models/Customer';
import { OpenAccountService } from 'src/app/services/open-account.service';
import {MatDialog} from '@angular/material/dialog';
import { VerificationDialogComponent } from '../verification-dialog/verification-dialog.component';

@Component({
  selector: 'app-open-account',
  templateUrl: './open-account.component.html',
  styleUrls: ['./open-account.component.css']
})
export class OpenAccountComponent implements OnInit {
  
  public firstName='';
  public lastName='';
  public email = '';
  public password = '';
  public verificationCode?:string;
  public registrationValid=false;
  public customer?:Customer;

  constructor(public dialog: MatDialog, private openService:OpenAccountService) {

   }


  ngOnInit(): void {
  }
  
  onSubmitForVerification(){
    this.openService.emailVerification(this.email!).subscribe(
      success => {
        this.customer={
          firstName:this.firstName,
          lastName:this.lastName,
          email:this.email,
          password:this.password,
          verificationCode:'aaaa'
        };
        const dialogRef = this.dialog.open(VerificationDialogComponent, {
          data: { customer: this.customer}});
          dialogRef.afterClosed().subscribe(result => console.log(result))
      }
      ,err=>alert(err)
    );
    
  }

}