import { FormattedError, ThrowStmt } from '@angular/compiler';
import { Component, Inject, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { IAllowance, IPayroll } from 'src/app/models/payroll';


@Component({
  selector: 'app-modify',
  templateUrl: './modify.component.html',
  styleUrls: ['./modify.component.css']
})


export class ModifyComponent implements OnInit {
  adjustmentForm: FormGroup;
  modifyPayroll: any;
  adjustedPay:number = 0;
  constructor(private dialogRef: MatDialogRef<ModifyComponent>,
    @Inject(MAT_DIALOG_DATA) private payroll: IPayroll,private fb:FormBuilder ) 
    { 
      var defaultAdjustment : any[] = [];

      this.payroll.allowances.forEach(element => {
        defaultAdjustment.push(element);
      });

      this.payroll.deductions.forEach(element => {
        defaultAdjustment.push(element);
      });

      this.adjustmentForm =this.fb.group({
        adjustments: this.fb.array(defaultAdjustment.map(adjustment=> this.defaultAdjusment(adjustment)))
      })
    }

  ngOnInit(): void {
    this.adjustedPay = this.payroll.NetPay;
    this.modifyPayroll = this.payroll;
  }

  adjustments():FormArray{
    return this.adjustmentForm.get('adjustments') as FormArray
  }

  defaultAdjusment(adjustment:any):FormGroup{
    return this.fb.group({
      Description:[adjustment.Description],
      Amount: [adjustment.Amount]
    });
  }

  newAdjusment():FormGroup{
    return this.fb.group({
      Description:'',
      Amount: 0
    });
  }

  addAdjustment(){
    this.adjustments().push(this.newAdjusment());
    this.updateNetPay();
  }

  removeAdjustment(i:number){
    this.adjustments().removeAt(i);
    this.updateNetPay();
  }

  onSubmit() {
    var newAdjustment = this.cleanAdjustmentData(this.adjustmentForm.value);
    //var oNewAdjustment = this.computeTotalAllowances(newAdjustment);
    this.dialogRef.close({data:newAdjustment});
  }

  onAdjustmentChanged(){
    this.updateNetPay();
  }

  updateNetPay(){
    this.adjustedPay = this.payroll.GrossPay + this.computeTotalAllowances(this.cleanAdjustmentData(this.adjustmentForm.value))
  }

  computeTotalAllowances(allowances:IAllowance[]):number{
    let totalAllowances:number=0;

    allowances.forEach(x=> {
      totalAllowances = totalAllowances + parseInt(x.Amount);
      x.PayrollId = this.payroll.Id
    })

    return totalAllowances;
  }

  cleanAdjustmentData(allowances:IAllowance[][]):IAllowance[]{
    var adjustment: IAllowance[] = [];

    for(let key in allowances) {
      var oAllowances = allowances[key];
      oAllowances.forEach(x=>{
        adjustment.push({Amount:x.Amount,Description:x.Description,PayrollId:x.PayrollId});
      });
     }

    return adjustment;
  }

}



