import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { DashboardService } from '../../core/services/dashboard.service';
import { JobService } from '../../core/services/job.service';

@Component({
  selector: 'app-recruiter-dashboard',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './recruiter-dashboard.component.html',
  styleUrl: './recruiter-dashboard.component.scss'
})
export class RecruiterDashboardComponent
implements OnInit {

  title = '';
  description = '';
  location = '';
  salary = 0;
  employmentType = '';

  stats: any;

  constructor(
    private jobService: JobService,
    private dashboardService: DashboardService
  ) {}

  ngOnInit(): void {

    this.dashboardService
      .getStats()
      .subscribe({
        next: (data) => {

          this.stats = data;

          console.log(data);
        },
        error: (err) => {

          console.log(err);
        }
      });
  }

  createJob() {

    const job = {
      title: this.title,
      description: this.description,
      location: this.location,
      salary: this.salary,
      employmentType: this.employmentType
    };

    this.jobService
      .createJob(job)
      .subscribe({
        next: () => {

          alert('Job Created Successfully');

          this.title = '';
          this.description = '';
          this.location = '';
          this.salary = 0;
          this.employmentType = '';

          this.ngOnInit();
        },
        error: (err) => {

          console.log(err);

          alert('Job Creation Failed');
        }
      });
  }
}