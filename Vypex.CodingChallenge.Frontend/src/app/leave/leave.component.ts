import { CommonModule } from '@angular/common';
import { Component, inject, Input, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzDatePickerModule } from 'ng-zorro-antd/date-picker';
import { en_US, NzI18nService, zh_CN } from 'ng-zorro-antd/i18n';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { EmployeeApiService } from '../api';
import { Leave } from '../api/models/leave';

@Component({
  selector: 'app-leave',
  providers: [],
  imports: [FormsModule, NzButtonModule, NzDatePickerModule, CommonModule, NzSelectModule, RouterModule],
  templateUrl: './leave.component.html',
  styleUrl: './leave.component.scss',
})

export class LeaveComponent {
  private readonly i18n = inject(NzI18nService);
  private readonly employeeApiService = inject(EmployeeApiService);
  public readonly leaves$ = this.employeeApiService.getLeaves();
  selectedLeaves: string[] = [];
  public employeeLeaves: Array<Leave> | undefined;
  date = null;
  employee: any;
  dateRange: Date | undefined;
  isEnglish = false;
  @Input()
  set id(id: string) {
    this.empId = id;
    this.employeeApiService.getEmployeeById(id).subscribe((data: any) => {
      this.employee = data;
    })
    this.leaves$.subscribe(() =>
      this.getEmployeeLeave()
    )
  }
  empId: string = '';
  onChange(result: Date): void {
    this.dateRange = result;
    this.getEmployeeLeave();
  }
  getEmployeeLeave() {
    this.employeeApiService.getLeavesByEmployeeId(this.empId).subscribe((data) => {
      this.employeeLeaves = data
      let allLeaveDates = [];
      for (const element of data) {
        for (const leaveDates of this.getDates(element.leaveFrom, element.leaveTo)) {
          allLeaveDates.push(leaveDates)
        }
      }
      leaveDates.set(JSON.stringify(allLeaveDates));
    });
  }
  disabledDate(current: Date): boolean {
    if (!leaveDates()) return current === null;

    return JSON.parse(leaveDates()).includes(current.toDateString());
  }
  getDates(s: any, e: any) { const a = []; for (const d = new Date(s); d <= new Date(e); d.setDate(d.getDate() + 1)) { a.push(new Date(d).toDateString()); } return a; };
  saveLeave() {
    const dates = JSON.parse(JSON.stringify(this.dateRange));
    const payload =
    {
      Id: this.empId,
      LeaveFrom: dates[0],
      LeaveTo: dates[1]
    }
    this.employeeApiService.postLeave(payload).subscribe()
  }
  changeLanguage(): void {
    this.i18n.setLocale(this.isEnglish ? zh_CN : en_US);
    this.isEnglish = !this.isEnglish;
  }
  deleteEmployee(id: string) {
    const payload =
    {
      Id: id,
      Name: ""
    }
    this.employeeApiService.deleteEmployee(payload).subscribe();
  }
  deleteEmployees() {
    this.employeeApiService.deleteEmployees().subscribe();
  }
  deleteRequests() {
    this.employeeApiService.deleteLeaveRequests(this.selectedLeaves).subscribe();
  }
}
const leaveDates = signal('');
