import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { cashbox } from '../models/Cashbox';

@Injectable({
  providedIn: 'root'
})
export class CreateCashboxService {


  constructor(private http:HttpClient) { }

  public createNewCashBox(newCashbox:cashbox):Observable<any>{
       return  this.http.post<boolean>("https://localhost:7248/api/",newCashbox);
  }
}
