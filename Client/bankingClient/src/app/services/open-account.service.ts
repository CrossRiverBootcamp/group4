import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Customer } from '../interfaces/Customer';

@Injectable({
  providedIn: 'root'
})
export class OpenAccountService {
private baseUrl="https://localhost:7248/api/Customer";
  constructor( private http:HttpClient) { }

  openAccount(newCustomer: Customer):Observable<any>{
    return this.http.post(`${this.baseUrl}`,newCustomer)
  }
}