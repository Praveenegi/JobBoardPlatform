import { CanActivateFn } from '@angular/router';
import { Router } from '@angular/router';
import { inject } from '@angular/core';

export const recruiterGuard: CanActivateFn = () => {

  const router = inject(Router);

  const role =
    localStorage.getItem('role');

  if (role === 'Recruiter') {
    return true;
  }

  return router.createUrlTree(['/jobs']);
};