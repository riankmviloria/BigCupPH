import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Observable } from 'rxjs';
import {IUser} from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }
  
  getEmployees(): Observable<IUser[]> {
    return this.http.get<IUser[]>(this.baseUrl + 'employee/GetEmployees');
  }


  updateEmployeeBasicPay(employee:IUser) {
    return this.http.post<IUser[]>(this.baseUrl + 'employee/updateEmployeeBasicPay',employee);
  }
}
