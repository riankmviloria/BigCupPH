export interface CutOffAttendance {
    EmployeeId: number;
    Employee: string;
    Attendances: IAttendance[];
}

export interface IAttendance {
    Date: string,
    isPresent: string,
  }

export interface IDeduction {
Description: string,
Amount: string,
PayrollId: number
}
