import { Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { RegisterComponent } from './pages/register/register.component';
import { JobsComponent } from './pages/jobs/jobs.component';
import { MyApplicationsComponent } from './pages/my-applications/my-applications.component';
import { ProfileComponent } from './pages/profile/profile.component';
import { authGuard } from './core/guards/auth.guard';
import { RecruiterDashboardComponent }
from './pages/recruiter-dashboard/recruiter-dashboard.component';
import { RecruiterApplicantsComponent } from './pages/recruiter-applicants/recruiter-applicants.component';
import { recruiterGuard }
from './core/guards/recruiter.guard';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'register',
    component: RegisterComponent
  },
  {
    path: 'jobs',
    component: JobsComponent,
    canActivate: [authGuard]
  },
  {
    path: 'my-applications',
    component: MyApplicationsComponent,
    canActivate: [authGuard]
  },
  {
    path: 'profile',
    component: ProfileComponent,
    canActivate: [authGuard]
  },
  {
  path: 'recruiter-applicants',
  component: RecruiterApplicantsComponent,
  canActivate: [authGuard, recruiterGuard]
},
{
  path: 'recruiter-dashboard',
  component: RecruiterDashboardComponent,
  canActivate: [authGuard, recruiterGuard]
}
];