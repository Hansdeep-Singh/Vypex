import { Routes } from '@angular/router';
import { EMPLOYEES_ROUTES } from './employees/employees.routes';
import { LeaveComponent } from './leave/leave.component';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'employees',
    pathMatch: 'full'
  },
  {
    path: 'leave/:id',
    component: LeaveComponent
  },
  {
    path: 'employees',
    children: EMPLOYEES_ROUTES
  },

];
