import { Component, Inject } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogActions, MatDialogContent, MatDialogRef } from '@angular/material/dialog';
import { DatePickerFormComponent } from '../../date-picker-form/date-picker-form.component';

@Component({
  selector: 'app-confirmation-dialog',
  templateUrl: './confirmation-booking.component.html',
  styleUrls: ['./confirmation-booking.component.css'],
  standalone: true,
  imports: [
    MatDialogContent,
    MatDialogActions,
    FormsModule,
    ReactiveFormsModule,
    DatePickerFormComponent
  ]
})
export class ConfirmationBookingDialogComponent {
  vehicleName: string | null = null;
  duration: number | undefined;
  startDate: string = '';

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
    if (this.startDate && this.duration) {
      this.dialogRef.close({ startDate: this.startDate, duration: this.duration });
    } else {
      console.error('Error: startDate or duration is undefined.');
    }
  }

  onDateChange(formattedDate: string): void {
    this.startDate = formattedDate;
  }
}
