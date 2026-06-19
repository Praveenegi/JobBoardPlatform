import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ApplicationService } from '../../core/services/application.service';

@Component({
  selector: 'app-recruiter-applicants',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './recruiter-applicants.component.html'
})
export class RecruiterApplicantsComponent
implements OnInit {

  applications: any[] = [];

  constructor(
    private applicationService: ApplicationService
  ) {}

  ngOnInit(): void {

    this.loadApplicants();
  }

  loadApplicants() {

    this.applicationService
      .getApplicationsByJob(1)
      .subscribe({
        next: (data: any) => {
          this.applications = data;
        }
      });
  }

  updateStatus(
    applicationId: number,
    status: string
  ) {

    this.applicationService
      .updateStatus(applicationId, status)
      .subscribe({
        next: () => {

          alert('Status Updated');

          this.loadApplicants();
        }
      });
  }
}