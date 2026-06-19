import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ApplicationService } from '../../core/services/application.service';

@Component({
  selector: 'app-my-applications',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './my-applications.component.html',
  styleUrl: './my-applications.component.scss'
})
export class MyApplicationsComponent implements OnInit {

  applications: any[] = [];

  constructor(
    private applicationService: ApplicationService
  ) {}

  ngOnInit(): void {

  const candidateId =
    Number(localStorage.getItem('userId'));

  this.applicationService
    .getApplications(candidateId)
    .subscribe({
      next: (data: any) => {
        this.applications = data;
      }
    });
}
}