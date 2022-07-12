import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule,ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { MaterialModule } from './material/material.module';
import { EmployeeComponent } from './components/employee/employee.component';
import { SalaryComponent } from './components/employee/salary/salary.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AttendanceComponent } from './components/attendance/attendance.component';
import { PayrollComponent } from './components/payroll/payroll.component';
import { ModifyComponent } from './components/payroll/modify/modify.component';
import { HashLocationStrategy, LocationStrategy } from '@angular/common';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { PayslipComponent } from './components/payroll/payslip/payslip.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    EmployeeComponent,
    SalaryComponent,
    AttendanceComponent,
    PayrollComponent,
    ModifyComponent,
    PayslipComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'employee', component: EmployeeComponent },
      { path: 'attendance', component: AttendanceComponent },
      { path: 'payroll', component: PayrollComponent },
    ],{useHash:true}),
    MaterialModule,
    BrowserAnimationsModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  providers: [{provide: LocationStrategy, useClass: HashLocationStrategy}],
  bootstrap: [AppComponent],
  entryComponents:[SalaryComponent,ModifyComponent,PayslipComponent]
})
export class AppModule { }
