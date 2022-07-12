import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Observable } from 'rxjs';
import {IUser} from '../models/user';
import { IPayroll } from '../models/payroll';
import { CutOffAttendance } from '../models/attendance';

@Injectable({
  providedIn: 'root'
})
export class AttendanceService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }
  
  CutOffDates(oCutOffDate:string): Observable<any[]> {
    var dateCutOff = {
      oCutOffDate: oCutOffDate
    }
    return this.http.post<any[]>(this.baseUrl + 'attendance/CutOffDates',dateCutOff);
  }

  getAttendancesByCutOffDate(oCutOffDate:string): Observable<any[]> {
    var dateCutOff = {
      oCutOffDate: oCutOffDate
    }
    return this.http.post<any[]>(this.baseUrl + 'attendance/GetAttendanceByCutOffDate', dateCutOff);
  }

  // saveSalaryAdjustment(adjustedPayroll: any){
  //   return this.http.post<IPayroll[]>(this.baseUrl + 'payroll/SaveSalaryAdjustment',adjustedPayroll);
  // }

  updateAttendance(updatedAttendance:any[]){
    return this.http.post(this.baseUrl + 'attendance/UpdateAttendance',updatedAttendance);
    
  }
}
