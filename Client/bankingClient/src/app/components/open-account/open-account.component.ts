import { Component, OnInit } from '@angular/core'
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Customer } from 'src/app/models/Customer';
import { OpenAccountService } from 'src/app/services/open-account.service';

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
  public registrationValid=false;
  public customer?:Customer;

  constructor( private openService:OpenAccountService) {

   }


  ngOnInit(): void {
  }

  onSubmit(){
    this.registrationValid=true;
    this.customer={
      firstName=this.firstName,
      lastName=this.lastName,
      email=this.email,
      password=this.password
    }
    this.openService.openAccount(this.customer).subscribe(
      success => {console.log(success)}
      ,err=>console.log(err)
    );
  }
  
}