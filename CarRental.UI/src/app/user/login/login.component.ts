import { FormsModule } from '@angular/forms';
import { UserService } from './../../services/user.service';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { Router, RouterModule } from '@angular/router';
import { IUser } from '../../models/user.model';
import { Login } from '../../models/login.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  standalone: true,
  imports: [FormsModule, RouterModule]
})

export class LoginComponent {
  title = 'Login';
  user: Login = { userName: '', password: '' };
  logSub!: Subscription;

  constructor(private userService: UserService, private router: Router) { }

  logIn(): void {
    if (this.user.userName && this.user.password) {
      this.logSub = this.userService.logIn(this.user.userName, this.user.password).subscribe(
        () => {
          this.router.navigate(['/main-page']);
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
