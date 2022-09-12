import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Operation } from '../models/Operation';

@Injectable({
  providedIn: 'root'
})
export class HistoryService {

  constructor(private http:HttpClient) { }
  public getAllOperations():Observable<Operation[]>{
    return this.http.get<Operation[]>("https://localhost:7248/api/Operation");//id and bool
}
}
