import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { cashbox } from '../models/Cashbox';

@Injectable({
  providedIn: 'root'
})
export class CashboxService {


  constructor(private http:HttpClient) { }

  public createNewCashBox(newCashbox:cashbox):Observable<boolean>{
       return  this.http.post<boolean>("https://localhost:7248/api/Cashbox",newCashbox);
  }
  public getCashboxInfo(accountId:Number):Observable<cashbox>{
    return  this.http.get<cashbox>(`https://localhost:7248/api/Cashbox/details/${accountId}`);
}
public checkCashboxExists(accountId:Number):Observable<boolean>{
  return  this.http.get<boolean>(`https://localhost:7248/api/Cashbox/exist/${accountId}`);
}
public updateCashbox(accountId:Number,newCashbox:cashbox):Observable<any>{
  return  this.http.put(`https://localhost:7248/api/Cashbox/${accountId}`,newCashbox);
}
}
