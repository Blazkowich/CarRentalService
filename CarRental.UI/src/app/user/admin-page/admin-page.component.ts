import { Component, OnInit } from '@angular/core';
import { IVehicle } from '../../models/vehicle.model';
import { VehicleService } from '../../services/vehicle.service';
import { AuthService } from '../../services/auth.service';
import { CommonModule, formatDate } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { AdminPageService } from '../../services/admin-page.service';
import { IUser } from '../../models/user.model';
import { IChat } from '../../models/chat.model';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from '../../confirmation-popup-window/removing-vehicle/confirmation.component';

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
    private router: Router,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.getVehicles();
    this.getUsersByMessages();
  }

  getVehicles(): void {
    this.vehicleService.getVehicles().subscribe(
      (vehicles: IVehicle[]) => {
        this.vehicles = vehicles;
      },
      (error: any) => {
        console.error('Error fetching vehicles:', error);
      }
    );
  }

  editVehicle(vehicle: IVehicle): void {
    this.vehicleService.updateVehicle(vehicle).subscribe(
      (updatedVehicle: IVehicle) => {
        const index = this.vehicles.findIndex(v => v.id === updatedVehicle.id);
        if (index !== -1) {
          this.vehicles[index] = updatedVehicle;
        }
        console.log('Edited vehicle', updatedVehicle);
      },
      (error: any) => {
        console.error('Error editing vehicle:', error);
      }
    );
  }

  addVehicle(vehicle: IVehicle): void {
    this.vehicleService.addVehicle(vehicle).subscribe(
      (newVehicle: IVehicle) => {
        this.vehicles.push(newVehicle);
        console.log('Added new vehicle', newVehicle);
      },
      (error: any) => {
        console.error('Error adding vehicle:', error);
      }
    );
  }

  deleteVehicle(vehicleId: string): void {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent);

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.vehicleService.deleteVehicle(vehicleId).subscribe(
          () => {
            this.vehicles = this.vehicles.filter(v => v.id !== vehicleId);
            console.log('Deleted vehicle with ID', vehicleId);
          },
          (error: any) => {
            console.error('Error deleting vehicle:', error);
          }
        );
      }
    });
  }

  getChatByUserId(userId: string): void {
    this.adminService.getChatByUserId(userId).subscribe(
      (chats: IChat[]) => {
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
