import { Component } from '@angular/core';
import { MatDialogActions, MatDialogContent, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-confirmation-dialog',
  templateUrl: './confirmation.component.html',
  styleUrls: ['./confirmation.component.css'],
  standalone: true,
  imports: [MatDialogContent, MatDialogActions]
})
export class ConfirmationDialogComponent {
  constructor(public dialogRef: MatDialogRef<ConfirmationDialogComponent>) {}

  onCancel(): void {
    this.dialogRef.close(false);
  }

  onConfirm(): void {
    this.dialogRef.close(true);
  }
}
