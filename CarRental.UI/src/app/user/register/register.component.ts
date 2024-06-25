import { UserService } from './../../services/user.service';
import { Component, OnDestroy } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
  standalone: true,
  imports: [FormsModule, RouterModule]
})

export class RegisterComponent {
  title = 'Car Rental Service Registration';
  UserName: string = '';
  FirstName: string = '';
  LastName: string = '';
  Email: string = '';
  PhoneNumber: number = 0;
  Address: string = '';
  Password: string = '';
  regSub!: Subscription;

  constructor(private userService: UserService) {}

  register(): void {
    if (this.UserName && this.Password) {
      this.regSub = this.userService
        .register(
          this.UserName,
          this.FirstName,
          this.LastName,
          this.Email,
          this.PhoneNumber,
          this.Address,
          this.Password
        ).subscribe(
          (user) => {
            console.log('Registered successfully:', user);
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
