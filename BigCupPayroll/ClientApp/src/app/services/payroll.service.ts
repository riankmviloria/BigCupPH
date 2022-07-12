import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Observable } from 'rxjs';
import {IUser} from '../models/user';
import { IPayroll } from '../models/payroll';

@Injectable({
  providedIn: 'root'
})
export class PayrollService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }
  
  computePayroll(oCutOffDate:string): Observable<IPayroll[]> {
    var dateCutOff = {
      oCutOffDate: oCutOffDate
    }
    return this.http.post<IPayroll[]>(this.baseUrl + 'payroll/ComputePayroll', dateCutOff);
  }

  getPayrollHistory(oCutOffDate:string): Observable<IPayroll[]> {
    var dateCutOff = {
      oCutOffDate: oCutOffDate
    }
    return this.http.post<IPayroll[]>(this.baseUrl + 'payroll/GetPayrollHistory',dateCutOff);
  }

  saveSalaryAdjustment(adjustedPayroll: any){
    return this.http.post<IPayroll[]>(this.baseUrl + 'payroll/SaveSalaryAdjustment',adjustedPayroll);
  }
}
