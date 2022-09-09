import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Customer } from '../models/Customer';

@Injectable({
  providedIn: 'root'
})
export class OpenAccountService {
  constructor( private http:HttpClient) { }

 public openAccount(newCustomer: Customer):Observable<boolean>{
    return this.http.post<boolean>("https://localhost:7248/api/Customer",newCustomer);
  }
}