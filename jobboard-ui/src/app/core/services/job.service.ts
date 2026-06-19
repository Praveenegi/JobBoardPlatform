import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class JobService {

  constructor(private http: HttpClient) {}

  getJobs() {
    return this.http.get(
      `${environment.apiUrl}/Jobs`
    );
  }

  createJob(job: any) {

  return this.http.post(
    `${environment.apiUrl}/Jobs`,
    job
  );
}
}