import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, RouterLink],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {

  email = '';
  password = '';

  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  login() {

    const loginData = {
      email: this.email,
      password: this.password
    };

    this.authService.login(loginData)
      .subscribe({
        next: (response: any) => {

          localStorage.setItem(
            'token',
            response.token
          );

          localStorage.setItem(
              'role',
              response.role
          );

          localStorage.setItem(
            'userId',
             response.userId
          );
          if (response.role === 'Recruiter') 
          {

          this.router.navigate([
            '/recruiter-dashboard'
          ]);

        } else{

          this.router.navigate([
            '/jobs'
          ]);
        }
          
        },
        error: (error) => {
          console.error(error);
          alert('Login Failed');
        }
      });
  }
}