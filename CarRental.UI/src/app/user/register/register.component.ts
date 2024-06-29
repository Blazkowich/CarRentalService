import { UserService } from './../../services/user.service';
import { Component, OnDestroy } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { Subscription } from 'rxjs';
import { Registration } from '../../models/registration.model';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
  standalone: true,
  imports: [FormsModule, RouterModule]
})

export class RegisterComponent {
  title = 'Car Rental Service Registration';
  registration: Registration = {
    userName: '',
    firstName: '',
    lastName: '',
    email: '',
    phoneNumber: 0,
    address: '',
    password: ''
  };
  regSub!: Subscription;

  constructor(private userService: UserService) {}

  register(): void {
    if (this.registration.userName && this.registration.password) {
      this.regSub = this.userService.register(this.registration).subscribe(
        (user) => {
          console.log('Registered successfully');
        },
        (error) => {
          console.error('Registration error:', error);
        }
      );
    } else {
      console.error('Username and password are required.');
    }
  }
}
