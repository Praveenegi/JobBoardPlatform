import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule, RouterLink],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {

  firstName = '';
  lastName = '';
  email = '';
  password = '';
  role = 'Candidate';

  constructor(private authService: AuthService) {}

  register() {

    const registerData = {
      firstName: this.firstName,
      lastName: this.lastName,
      email: this.email,
      password: this.password,
      role: this.role
    };

    this.authService.register(registerData)
      .subscribe({
        next: () => {
          alert('Registration Successful');
        },
        error: (err) => {
          console.log(err);
          if (typeof err.error === 'string') {
            alert(err.error);
          } else {
            alert('Registration Failed');
          }
        }
      });
  }
}