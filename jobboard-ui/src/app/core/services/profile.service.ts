import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {

  constructor(private http: HttpClient) {}

  getProfile(userId: number) {
    return this.http.get(
      `${environment.apiUrl}/CandidateProfiles/${userId}`
    );
  }

  createProfile(profile: any) {
    return this.http.post(
      `${environment.apiUrl}/CandidateProfiles`,
      profile
    );
  }

  updateProfile(id: number, profile: any) {
    return this.http.put(
      `${environment.apiUrl}/CandidateProfiles/${id}`,
      profile
    );
  }
}