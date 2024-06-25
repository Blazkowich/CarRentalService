import { Component } from '@angular/core';
import { Subscription } from 'rxjs';
import { EmailService } from '../../services/email.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-support-email',
  templateUrl: './support-email.component.html',
  styleUrls: ['./support-email.component.css'],
  standalone: true,
  imports: [FormsModule]
})
export class EmailComponent {
  title = 'Support';
  subject: string = '';
  message: string = '';
  emSub!: Subscription;

  constructor(private emailService: EmailService) {}

  sendEmail(): void {
    if (this.subject && this.message) {
      this.emSub = this.emailService.sendEmail(this.subject, this.message).subscribe(
        (response) => {
          console.log('Email sent successfully:', response);
        },
        (error) => {
          console.error('Error sending email:', error);
        }
      );
    } else {
      console.error('Subject and message are required.');
    }
  }
}
