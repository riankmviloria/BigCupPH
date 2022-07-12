import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { IUser } from 'src/app/models/user';
import { EmployeeService } from 'src/app/services/employee.service';
import { SalaryComponent } from './salary/salary.component';

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.css']
})
export class EmployeeComponent implements OnInit {

  private employees: IUser[] =[];
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  constructor(public dialog: MatDialog,private employeeService:EmployeeService,http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
   
  }
  displayedColumns: string[] = [];
  dataSource!: MatTableDataSource<any>;
  isEdit = false;
  rowIndex = 0;

  ngOnInit(): void {
    this.employeeService.getEmployees().subscribe(res =>{
      this.employees = res;
      console.log(this.employees);
      this. displayedColumns = ['UserName', 'LastName', 'FirstName', 'Type','BasicPay','Actions'];
      this.dataSource = new MatTableDataSource(this.employees);
      this.dataSource.paginator = this.paginator;

    }, error => console.error(error)
    );
  }



  showEmployeeSalary(user: IUser) {
    const dialogRef = this.dialog.open(SalaryComponent, {
      width: '300px',
      data: user
    });
    dialogRef.afterClosed().subscribe(() => {
      //dialogRef.componentInstance.onTaskCreate.unsubscribe();
    })
  }

  editRow(rowNo:number,employee:IUser){
    this.rowIndex = employee.Id;
    if (!this.isEdit) {
      this.isEdit = true;
    }
    else if (this.isEdit) {
      // this.scrapingService.editUsers(iUsers).subscribe(result => {
      //   this.showEdited(iUsers.UserName);
      //   this.ngOnInit();
      // }, error => console.error(error));
      this.isEdit = false;

    }
  }
  

}
