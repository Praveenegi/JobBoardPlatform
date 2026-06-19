import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class StorageService {

  getUserId(): number {

    return Number(
      localStorage.getItem('userId')
    );
  }

  getRole(): string {

    return localStorage.getItem('role')
      || '';
  }

  getToken(): string {

    return localStorage.getItem('token')
      || '';
  }

  logout() {

    localStorage.clear();
  }
}