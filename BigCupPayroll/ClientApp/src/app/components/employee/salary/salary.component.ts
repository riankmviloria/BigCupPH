import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { IUser } from 'src/app/models/user';
import { EmployeeService } from 'src/app/services/employee.service';

@Component({
  selector: 'app-salary',
  templateUrl: './salary.component.html',
  styleUrls: ['./salary.component.css']
})
export class SalaryComponent implements OnInit {
  salary: any;
  isEdit = false;
  constructor(private dialogRef: MatDialogRef<SalaryComponent>,
    @Inject(MAT_DIALOG_DATA) private user: IUser, private employeeService:EmployeeService) { }

  ngOnInit(): void {
    console.log(this.user.BasicPay);
    this.salary = this.user;
  }


  editRow(employee:IUser){
    if (!this.isEdit) {
      this.isEdit = true;
    }
    else if (this.isEdit) {
      this.employeeService.updateEmployeeBasicPay(employee).subscribe(result => {
        this.ngOnInit();
      }, error => console.error(error));
      this.isEdit = false;
    }
  }

}
