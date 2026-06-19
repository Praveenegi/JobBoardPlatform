import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { DashboardService } from '../../core/services/dashboard.service';
import { JobService } from '../../core/services/job.service';

@Component({
  selector: 'app-recruiter-dashboard',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './recruiter-dashboard.component.html',
  styleUrl: './recruiter-dashboard.component.scss'
})
export class RecruiterDashboardComponent
implements OnInit {

  jobId = 0;

  title = '';
  description = '';
  location = '';
  salary = 0;
  employmentType = '';

  jobs: any[] = [];

  stats: any;

  constructor(
    private jobService: JobService,
    private dashboardService: DashboardService
  ) {}

  ngOnInit(): void {

    this.loadJobs();

    this.dashboardService
      .getStats()
      .subscribe({
        next: (data) => {

          this.stats = data;
        },
        error: (err) => {

          console.log(err);
        }
      });
  }

  loadJobs() {

    this.jobService
      .getJobs()
      .subscribe({
        next: (data: any) => {

          this.jobs = data;
        },
        error: (err) => {

          console.log(err);
        }
      });
  }

  editJob(job: any) {

    this.jobId = job.id;

    this.title = job.title;

    this.description =
      job.description;

    this.location =
      job.location;

    this.salary =
      job.salary;

    this.employmentType =
      job.employmentType;
  }

  createJob() {

    const job = {

      title: this.title,
      description: this.description,
      location: this.location,
      salary: this.salary,
      employmentType: this.employmentType
    };

    if (this.jobId > 0) {

      this.jobService
        .updateJob(
          this.jobId,
          job
        )
        .subscribe({
          next: () => {

            alert('Job Updated Successfully');

            this.resetForm();

            this.loadJobs();

            this.ngOnInit();
          },
          error: (err) => {

            console.log(err);

            alert('Job Update Failed');
          }
        });

    } else {

      this.jobService
        .createJob(job)
        .subscribe({
          next: () => {

            alert('Job Created Successfully');

            this.resetForm();

            this.loadJobs();

            this.ngOnInit();
          },
          error: (err) => {

            console.log(err);

            alert('Job Creation Failed');
          }
        });
    }
  }

  resetForm() {

    this.jobId = 0;

    this.title = '';

    this.description = '';

    this.location = '';

    this.salary = 0;

    this.employmentType = '';
  }

  deleteJob(id: number) {

  const confirmDelete =
    confirm(
      'Are you sure you want to delete this job?'
    );

  if (!confirmDelete)
    return;

  this.jobService
    .deleteJob(id)
    .subscribe({
      next: () => {

        alert(
          'Job Deleted Successfully'
        );

        this.loadJobs();
      },

      error: (err) => {

        console.log(err);

        alert(
          'Job Delete Failed'
        );
      }
    });
}
}