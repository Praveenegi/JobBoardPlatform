import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApplicationService {

  constructor(private http: HttpClient) {}

  apply(jobId: number, candidateId: number) {
    return this.http.post(
      `${environment.apiUrl}/Applications`,
      {
        jobId,
        candidateId
      }
    );
  }

  getApplications(candidateId: number) {
  return this.http.get(
    `${environment.apiUrl}/Applications/candidate/${candidateId}`
  );
}

getApplicationsByJob(jobId: number) {
  return this.http.get(
    `${environment.apiUrl}/Applications/job/${jobId}`
  );
}

updateStatus(
  applicationId: number,
  status: string
) {
  return this.http.put(
    `${environment.apiUrl}/Applications/${applicationId}/status`,
    { status }
  );
}
}

