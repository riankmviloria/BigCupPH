export interface IPayroll {
    Id: number;
    UserId: string;
    Employee: string;
    GrossPay: number;
    NetPay: number;
    CutOffDate:Date;  
    PresentDay:number;  
    BasicPay:number;  
    allowances: IAllowance[],
    deductions: IDeduction[]
}

export interface IAllowance {
    Description: string,
    Amount: string,
    PayrollId: number
  }

export interface IDeduction {
Description: string,
Amount: string,
PayrollId: number
}
