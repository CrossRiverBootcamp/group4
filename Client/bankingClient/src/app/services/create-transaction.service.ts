import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Transaction } from '../interfaces/Transaction';

@Injectable({
  providedIn: 'root'
})
export class CreateTransactionService {

  private baseUrl="https://localhost:7248/api";
  constructor(private http:HttpClient) { }

  public createNewTransaction(newTransaction:Transaction):Observable<any>{
       return this.http.post("https://localhost:7248/api/Transaction",newTransaction);
  }
}
