import { Component, OnInit } from '@angular/core';
import { IVehicle } from '../../models/vehicle.model';
import { VehicleService } from '../../services/vehicle.service';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { AdminPageService } from '../../services/admin-page.service';
import { IUser } from '../../models/user.model';
import { IChat } from '../../models/chat.model';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from '../../confirmation-popup-window/removing-cancel-confirmation/confirmation.component';
import { ChatDataService } from '../../services/chat-data.service';
import { ChatSignalRService } from '../../services/signalR.service';

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
  messages: IChat[] = [];

  constructor(
    private vehicleService: VehicleService,
    private adminService: AdminPageService,
    private chatDataService: ChatDataService,
    private chatService: ChatSignalRService,
    private router: Router,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.getVehicles();
    this.getUsersByMessages();

    this.adminService.getUsersByMessages();

    this.chatService.users$.subscribe((users: IUser[]) => {
      this.users = users.filter(user => user.role !== 'Admin');
    });

    this.chatService.message$.subscribe((message: IChat | null) => {
      if (message) {
        this.messages.push(message);
      }
    });
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
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      data: { message: "Do you really want to delete vehicle?" }
    });

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
        this.chatDataService.setChats(chats);
        this.router.navigate([`/admin-chat/${userId}`]);
      },
      (error: any) => {
        console.error('Error fetching chat:', error);
      }
    );
  }

  getUsersByMessages(): void {
    this.chatService.getUsersByMessages();
  }

  onBack(): void {
    this.router.navigate(['/user-page']);
  }
}
