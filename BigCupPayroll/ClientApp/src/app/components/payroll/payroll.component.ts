import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import * as moment from 'moment';
import { IAllowance, IPayroll } from 'src/app/models/payroll';
import { PayrollService } from 'src/app/services/payroll.service';
import { ModifyComponent } from './modify/modify.component';
import jsPDF from 'jspdf';
import html2canvas from 'html2canvas';
import { PayslipComponent } from './payslip/payslip.component';

@Component({
  selector: 'app-payroll',
  templateUrl: './payroll.component.html',
  styleUrls: ['./payroll.component.css']
})



export class PayrollComponent implements OnInit {
  @ViewChild(ModifyComponent) modifyComponent!:ModifyComponent;
  payroll : IPayroll[] = [];
  displayedColumns: string[] = [];
  dataSource: any;
  cuttOffDate:Date = new Date;
  
  constructor(private dialog: MatDialog,private payrollService:PayrollService) { }

  ngOnInit(): void {
    var oCutOffDate = moment(this.cuttOffDate).format('MM/DD/YYYY');

    this.payrollService.getPayrollHistory(oCutOffDate).subscribe(res =>{
      this.payroll=res;
      console.log(this.payroll);
      this. displayedColumns = ['UserId','Employee','CutOffDate', 'GrossPay', 'NetPay','Actions'];
      this.dataSource = this.payroll;
    }, error => console.error(error)
    );
  }


  computePayroll(){
    var oCutOffDate = moment(this.cuttOffDate).format('MM/DD/YYYY');
    this.payrollService.computePayroll(oCutOffDate).subscribe(res =>{
      console.log(res)
      this.payroll = res;
      console.log(this.payroll);
    }, error => console.error(error)
    );
  }

  modifyPayroll(payroll: IPayroll){
    const dialogRef = this.dialog.open(ModifyComponent, {
      width: '800px',
      data: payroll
    });
    dialogRef.afterClosed().subscribe(result => {
      console.log(result.data)
      payroll.NetPay = payroll.GrossPay + this.computeTotalAllowances(result.data);
     
     var adjustPayroll = {
       newPay: payroll,
       adjustment: result.data
     }
     
    console.log(adjustPayroll);
     this.payrollService.saveSalaryAdjustment(adjustPayroll).subscribe(result=>{
      this.ngOnInit();
     },error=> console.error(error));
    })
  }
  
  computeTotalAllowances(allowances:IAllowance[]):number{
    let totalAllowances:number=0;

    allowances.forEach(x=> {
      totalAllowances = totalAllowances + parseInt(x.Amount);
    })

    return totalAllowances;
  }

  getCutOffDate(date:Date){
    var oDate = moment(date).format('MM/DD/YYYY');
    // this.getAttendancesByCutOffDate(oDate);
  }
  fomatDate(date:any){
    return moment(date).format('MM/DD/YYYY');
  }

  viewPayslip(payroll: IPayroll){
    const dialogRef = this.dialog.open(PayslipComponent, {
      width: '500px',
      data: payroll
    });
    // dialogRef.afterClosed().subscribe(result => {
    //   console.log(result.data)
    //   payroll.NetPay = payroll.GrossPay + this.computeTotalAllowances(result.data);
     
    //  var adjustPayroll = {
    //    newPay: payroll,
    //    adjustment: result.data
    //  }
     
    // console.log(adjustPayroll);
    //  this.payrollService.saveSalaryAdjustment(adjustPayroll).subscribe(result=>{
    //   this.ngOnInit();
    //  },error=> console.error(error));
    // })
  }

  
}