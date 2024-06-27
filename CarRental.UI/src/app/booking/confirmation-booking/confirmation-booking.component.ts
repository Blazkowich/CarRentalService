import { Component, Inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogActions, MatDialogContent, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-confirmation-dialog',
  templateUrl: './confirmation-booking.component.html',
  styleUrls: ['./confirmation-booking.component.css'],
  standalone: true,
  imports: [MatDialogContent, MatDialogActions, FormsModule]
})
export class ConfirmationBookingDialogComponent {
  startDate: Date = new Date();
  duration: number = 1;
  vehicleName: string | null = null;

  constructor(
    public dialogRef: MatDialogRef<ConfirmationBookingDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { vehicleId: string, vehicleName: string }
  ) {
    this.vehicleName = data.vehicleName;
  }

  onCancel(): void {
    this.dialogRef.close(false);
  }

  onConfirm(): void {
    this.dialogRef.close({ startDate: this.startDate, duration: this.duration });
  }

  formatDate(date: Date): string {
    const year = date.getFullYear();
    const month = ('0' + (date.getMonth() + 1)).slice(-2);
    const day = ('0' + date.getDate()).slice(-2);
    const hours = ('0' + date.getHours()).slice(-2);
    const minutes = ('0' + date.getMinutes()).slice(-2);
    return `${year}-${month}-${day}T${hours}:${minutes}`;
  }
}
