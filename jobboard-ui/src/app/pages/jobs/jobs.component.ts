import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { JobService } from '../../core/services/job.service';
import { ApplicationService } from '../../core/services/application.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-jobs',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './jobs.component.html',
  styleUrl: './jobs.component.scss'
})
export class JobsComponent implements OnInit {

searchTerm = '';
selectedLocation = '';
selectedEmploymentType = '';

  jobs: any[] = [];
  allJobs: any[] = [];

  constructor(
    private jobService: JobService,
    private applicationService: ApplicationService
  ) {}

  ngOnInit(): void {

    this.jobService.getJobs()
      .subscribe({
        next: (data: any) => {
          this.jobs = data;
          this.allJobs = data;
        },
        error: (err) => {
          console.log(err);
        }
      });
  }

  apply(jobId: number) {

  const candidateId =
    Number(localStorage.getItem('userId'));

  this.applicationService
    .apply(jobId, candidateId)
    .subscribe({
      next: () => {
        alert('Application Submitted');
      },
      error: (err) => {
        console.log(err);
        if (typeof err.error === 'string') {
          alert(err.error);
        }
        else {
          alert('Application Failed');
        }
    }
  });
}

filterJobs() {

  this.jobs =
    this.allJobs.filter(job => {

      const matchesSearch =
        job.title
          .toLowerCase()
          .includes(
            this.searchTerm
            .toLowerCase()
          );

      const matchesLocation =
        !this.selectedLocation ||
        job.location ===
        this.selectedLocation;

      const matchesType =
        !this.selectedEmploymentType ||
        job.employmentType ===
        this.selectedEmploymentType;

      return matchesSearch &&
             matchesLocation &&
             matchesType;
    });
}
}