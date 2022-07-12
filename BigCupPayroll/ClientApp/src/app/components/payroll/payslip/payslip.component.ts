import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import * as moment from 'moment';
import { IPayroll } from 'src/app/models/payroll';

@Component({
  selector: 'app-payslip',
  templateUrl: './payslip.component.html',
  styleUrls: ['./payslip.component.css']
})
export class PayslipComponent implements OnInit {

  constructor(private dialogRef: MatDialogRef<PayslipComponent>,
    @Inject(MAT_DIALOG_DATA) public payroll: IPayroll) { }

  ngOnInit(): void {
    console.log(this.payroll);
  }

   formatDate(date:any){
    return moment(date).format('MM/DD/YYYY');
  }
}
