import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { Employee } from '../models/employee';
import { Leave } from '../models/leave';

@Injectable({ providedIn: 'root' })
export class EmployeeApiService {
  private readonly httpClient = inject(HttpClient);

  private readonly baseUrl = 'https://localhost:5000';

  public getEmployees(): Observable<Array<Employee>> {
    return this.httpClient.get<Array<Employee>>(`${this.baseUrl}/api/employees/GetEmployees`);
  }
  public getLeaves(): Observable<Array<Leave>> {
    return this.httpClient.get<Array<Leave>>(`${this.baseUrl}/api/employees/GetLeaves`);
  }

  public getLeavesByEmployeeId(id: string): Observable<Array<Leave>> {
    return this.httpClient.get<Array<Leave>>(`${this.baseUrl}/api/employees/GetLeavesByEmployeeId/${id}`);
  }

  public getEmployeeById(id: string): Observable<Employee> {
    return this.httpClient.get<Employee>(`${this.baseUrl}/api/employees/GetEmployeeById/${id}`);
  }

  public postLeave(model: any): Observable<boolean> {
    return this.httpClient.post<boolean>(`${this.baseUrl}/api/employees/Leave`, model);
  }

  public deleteEmployee(model: any): Observable<boolean> {
    return this.httpClient.post<boolean>(`${this.baseUrl}/api/employees/DeleteEmployeeLeave`, model);
  }

  public deleteEmployees(): Observable<boolean> {
    return this.httpClient.delete<boolean>(`${this.baseUrl}/api/employees/DeleteEmployeesLeave`);
  }
}
