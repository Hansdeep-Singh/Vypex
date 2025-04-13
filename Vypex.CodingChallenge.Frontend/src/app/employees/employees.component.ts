import { AsyncPipe } from '@angular/common';
import { Component, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzTableModule } from 'ng-zorro-antd/table';
import { EmployeeApiService } from '../api/services/employee-api.service';

@Component({
  selector: 'app-employees',
  providers: [],
  imports: [
    NzTableModule,
    NzButtonModule,
    AsyncPipe,
    RouterLink
  ],
  templateUrl: './employees.component.html',
  styleUrl: './employees.component.scss'
})
export class EmployeesComponent {
  private readonly employeeApiService = inject(EmployeeApiService);
  public readonly employees$ = this.employeeApiService.getEmployees();
}
