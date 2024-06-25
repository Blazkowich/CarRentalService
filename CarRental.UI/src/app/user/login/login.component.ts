import { FormsModule } from '@angular/forms';
import { UserService } from './../../services/user.service';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  standalone: true,
  imports: [FormsModule, RouterModule]
})

export class LoginComponent {
  title = 'Login';
  UserName: string = '';
  Password: string = '';
  logSub!: Subscription;

  constructor(private userService: UserService) { }

  logIn(): void {
    if (this.UserName && this.Password) {
      this.logSub = this.userService.logIn(this.UserName, this.Password).subscribe(
        (user) => {
          console.log('Logged in successfully:', user);
        },
        (error) => {
          console.error('Login error:', error);
        }
      );
    } else {
      console.error('Username and password are required.');
    }
  }
}
