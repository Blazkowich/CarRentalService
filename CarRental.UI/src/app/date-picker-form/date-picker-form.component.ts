import { Component, Output, EventEmitter } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-date-picker-form',
  templateUrl: './date-picker-form.component.html',
  styleUrls: ['./date-picker-form.component.css'],
  standalone: true,
  imports: [ReactiveFormsModule]
})
export class DatePickerFormComponent {
  @Output() dateChange = new EventEmitter<string>();

  currentDate: Date = new Date();
  currentDateFormatted: string;

  dateControl = new FormControl();

  constructor() {
    this.currentDateFormatted = this.formatDate(this.currentDate);
    this.dateControl.setValue(this.currentDateFormatted);
  }

  formatDate(date: Date): string {
    const year = date.getFullYear();
    const month = ('0' + (date.getMonth() + 1)).slice(-2);
    const day = ('0' + date.getDate()).slice(-2);
    const hours = ('0' + date.getHours()).slice(-2);
    const minutes = ('0' + date.getMinutes()).slice(-2);
    return `${year}-${month}-${day}T${hours}:${minutes}`;
  }

  onDateChange(event: any): void {
    const date: Date = new Date(event.target.value);
    const formattedDate: string = this.formatDate(date);
    this.dateChange.emit(formattedDate);
  }
}
