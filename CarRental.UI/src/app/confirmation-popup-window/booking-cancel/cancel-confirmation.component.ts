import { Component } from '@angular/core';
import { MatDialogActions, MatDialogContent, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-cancel-confirmation-dialog',
  templateUrl: './cancel-confirmation.component.html',
  styleUrls: ['./cancel-confirmation.component.css'],
  standalone: true,
  imports: [MatDialogContent, MatDialogActions]
})
export class CancelConfirmationDialogComponent {
  constructor(public dialogRef: MatDialogRef<CancelConfirmationDialogComponent>) {}

  onCancel(): void {
    this.dialogRef.close(false);
  }

  onConfirm(): void {
    this.dialogRef.close(true);
  }
}
