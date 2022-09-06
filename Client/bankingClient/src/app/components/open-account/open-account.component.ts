import { Component, OnInit } from '@angular/core'
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
// import { CustomButton } from '@okta/okta-signin-widget';
import { Customer } from 'src/app/interfaces/Customer';
import { OpenAccountService } from 'src/app/services/open-account.service';

@Component({
  selector: 'app-open-account',
  templateUrl: './open-account.component.html',
  styleUrls: ['./open-account.component.css']
})
export class OpenAccountComponent implements OnInit {

  openAccountForm:FormGroup;

  constructor(private formBuilder:FormBuilder, private openService:OpenAccountService) {
    this.openAccountForm=this.createFormGroup(formBuilder);
   }
  createFormGroup(formBuilder: FormBuilder) {
    return formBuilder.group({
      firstName:[,[Validators.required],],
      lastName:[,[Validators.required],],
      email:[,[Validators.email,Validators.required],],
      password:[,[Validators.required],],
    });
  }

  ngOnInit(): void {
  }

  onSubmit(){
    const newCustomer:Customer= 
    Object.assign({},this.openAccountForm.value);
    this.openService.openAccount(newCustomer);
    console.log(newCustomer);
    this.openAccountForm.reset();

  }
}