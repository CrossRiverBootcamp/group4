import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Customer } from '../models/customer';

@Injectable({
  providedIn: 'root'
})
export class OpenAccountService {
  constructor( private http:HttpClient) { }
//   var headers = new HttpHeaders({
//     'Content-Type': 'application/json'});
// var options = { headers: headers };
protected get requestHeaders(): { headers: HttpHeaders | { [header: string]: string | string[]; } } {
  const headers = new HttpHeaders({
    'Content-Type': "application/json",
    Accept: "application/json, text/plain, */*"
  });

  return { headers };
}

  public emailVerification(email:string):Observable<any>{
    const body=JSON.stringify(email);
    return this.http.post("https://localhost:7248/api/EmailVerification",body,this.requestHeaders);
  }
  public emailVerificationAgain(email:string):Observable<any>{
    const body=JSON.stringify(email);
    return this.http.post("https://localhost:7248/api/EmailVerification/ResendCode",body,this.requestHeaders);
  }
 public openAccount(newCustomer: Customer):Observable<boolean>{
    return this.http.post<boolean>("https://localhost:7248/api/Customer",newCustomer);
  }
}