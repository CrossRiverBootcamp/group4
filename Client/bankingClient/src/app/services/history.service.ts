import { NumberInput } from '@angular/cdk/coercion';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Operation } from '../models/Operation';

@Injectable({
  providedIn: 'root'
})
export class HistoryService {

  constructor(private http:HttpClient) { }
  public getAllOperations(accountId:Number , isChecked:Boolean ):Observable<Operation[]>{
    return this.http.get<Operation[]>(`https://localhost:7248/api/Operations/id?accountId=${accountId}&&sortByDateDesc=${isChecked}`);
}
public getOperationsByDetails(accountId:Number,isChecked:Boolean,pageNumber:Number,numOfRecords:Number):Observable<Operation[]>{
  return this.http.get<Operation[]>(`https://localhost:7248/api/Operations/filter?accountId=${accountId}&sortByDateDesc=${isChecked}&pageNumber=${pageNumber}&numOfRecords=${numOfRecords}`);
}
public getNumberOfOperations(accountId:Number):Observable<Number>{
  return this.http.get<Number>(`https://localhost:7248/api/Operations/count?accountId=${accountId}`);
}

}
