import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';
import * as moment from 'moment';
import { CutOffAttendance, IAttendance } from 'src/app/models/attendance';
import { IAllowance } from 'src/app/models/payroll';
import { IUser } from 'src/app/models/user';
import { AttendanceService } from 'src/app/services/attendance.service';

@Component({
  selector: 'app-attendance',
  templateUrl: './attendance.component.html',
  styleUrls: ['./attendance.component.css']
})
export class AttendanceComponent implements OnInit {
  checkAllIcon = 'check_circle';
  employees:IUser[][] = [];
  // columnNames : any;
  columnNames: string[] = [];
  columns: string[] = [];
  constructor(private attendanceService:AttendanceService) { }
  table : any[] = [];
  cutoffattendances: CutOffAttendance[] = [];
  cutoffdates: any = [];
  employeeAttendances : any = [];
  cuttOffDate:Date = new Date;
  someDateToBlock: number = 3;

  // dateFilter: any;
  @Output()
dateChange: EventEmitter<MatDatepickerInputEvent<any>> = new EventEmitter();
  ngOnInit(): void {
    var oCutOffDate = moment(this.cuttOffDate).format('MM/DD/YYYY');
    this.getAttendancesByCutOffDate(oCutOffDate);
  }

  getAttendancesByCutOffDate(oCutOffDate:string){
    this.columnNames = [];
    this.columns = [];
    this.table = [];

    this.attendanceService.getAttendancesByCutOffDate(oCutOffDate).subscribe(res =>{
      this.cutoffattendances = res as CutOffAttendance[];
      this.attendanceService.CutOffDates(oCutOffDate).subscribe(res=>{

        this.columnNames.push('Employee');
        this.columns.push('Employee');

        this.columnNames = this.columnNames.concat(res);
        this.columns =this.columns.concat(res);
        this.cutoffdates = res;
        this.columnNames.push('CheckAll');

        this.cutoffattendances.forEach(x=>{
          let array :  any[] = [];
          array.push(x.Employee)
          x.Attendances.forEach(y=> {
            array.push(y.isPresent)
          })
          array.push(x.EmployeeId)
          this.table.push(array);
        });
      });
    }, error => console.error(error)
    );
  }
  rowChanged (row:any,i:number,event:any){
    return row[i] = event.checked;
  }

  checkAllCheckBox(row:Array<string>,i:number,icon:any){
    var identifier = 1;
      if(this.cutoffdates.length === 17){
        identifier = 2
      }

    if(icon == "check_circle"){
      for (let x = 1; x < row.length - identifier; x++) {
        this.table[i][x] = true;
      }
      this.checkAllIcon = "cancel";
    }
    else{
      for (let x = 1; x < row.length - identifier; x++) {
        this.table[i][x] = false;
      }
      this.checkAllIcon = "check_circle";
    }
  }

  SaveAttendance(){
    var oDate = moment(this.cuttOffDate).format('MM/DD/YYYY');
    this.employeeAttendances = [];
    this.table.forEach(element => {
      var identifier = 1;
      if(element.length === 17){
        identifier = 2
      }
      
      let emp = {
        Employee: element[0],
        EmployeeId: element[element.length - 1],
        Attendances: {},
        oCutOffDate: ''
      }

      let att = [];
        for(var x = 0; x < element.length - 1; x++){
          if(x!=0){
            var z = {
              Date: this.cutoffdates[x-identifier],
              isPresent: element[x]
            }
            att.push(z);
          }
        };

      emp = {...emp,Attendances:att,oCutOffDate:oDate};
      this.employeeAttendances.push(emp);
    });


    this.attendanceService.updateAttendance(this.employeeAttendances).subscribe(res =>{
      console.log(res);
    },(error)=> console.error(error))
  }

  getCutOffDate(date:Date){
    var oDate = moment(date).format('MM/DD/YYYY');
    this.getAttendancesByCutOffDate(oDate);
  }


}
