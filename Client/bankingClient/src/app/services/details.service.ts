import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Customer } from '../models/customer';
@Injectable({
  providedIn: 'root'
})
export class DetailsService {

  constructor(private http:HttpClient) { }

  public getCustomer(accountId:Number):Observable<Customer>{
       return this.http.get<Customer>(`https://localhost:7248/api/Customer/${accountId}`);
  }
}