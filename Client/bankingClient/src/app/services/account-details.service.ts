import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Login } from '../models/Login';
import { AccountInfo } from '../models/AccountInfo';

@Injectable({
  providedIn: 'root'
})
export class AccountDetailsService {

  public Login(loginDTO:Login):Observable<Number>{
    return this.http.post<Number>("https://localhost:7248/api/Account",loginDTO);
  }
  public getAccountInfo(accountId: Number):Observable<AccountInfo>{
    return this.http.get<AccountInfo>(`https://localhost:7248/api/Account/${accountId}`);
  }

    constructor(private http: HttpClient) { }
  }
