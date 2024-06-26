import { Component, OnInit } from '@angular/core';
import { IVehicle } from '../../models/vehicle.model';
import { VehicleService } from '../../services/vehicle.service';
import { AuthService } from '../../services/auth.service';
import { CommonModule, formatDate } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { AdminPageService } from '../../services/admin-page.service';
import { IUser } from '../../models/user.model';
import { IChat } from '../../models/chat.model';

@Component({
  selector: 'app-admin-page',
  templateUrl: './admin-page.component.html',
  styleUrls: ['./admin-page.component.css'],
  standalone: true,
  imports: [CommonModule, RouterModule]
})
export class AdminPageComponent implements OnInit {
  title = 'Cabinet';
  availableVehicles: IVehicle[] = [];
  vehicles: IVehicle[] = []
  users: IUser[] = [];

  constructor(
    private vehicleService: VehicleService,
    private authService: AuthService,
    private adminService: AdminPageService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.getUsersByMessages();
  }

  getChatByUserId(userId: string): void {
    this.adminService.getChatByUserId(userId).subscribe(
      (chats: IChat[]) => {
        console.log(userId, chats);
        const encodedChats = encodeURIComponent(JSON.stringify(chats));
        this.router.navigate([`/admin-chat`, { chats: encodedChats }]);
      },
      (error: any) => {
        console.error('Error fetching chat:', error);
      }
    );
  }


  getUsersByMessages(): void {
    this.adminService.getUsersByMessages().subscribe(
      (users: IUser[]) => {
        this.users = users;
      },
      (error: any) => {
        console.error('Error fetching users:', error);
      }
    )
  }

  onBack(): void {
    this.router.navigate(['/user-page']);
  }
}
