import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './user/login/login.component';
import { RegisterComponent } from './user/register/register.component';
import { MainPageComponent } from './main-page/main-page.component';
import { VehicleDetailComponent } from './main-page/page-details/page-details.component';
import { EmailComponent } from './support-pages/support-email/support-email.component';
import { ChatComponent } from './support-pages/support-chat/support-chat.component';
import { UserPageComponent } from './user/user-page/user-page.component';
import { AuthGuard } from './authGuard/auth.guard';

export const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'main-page', component: MainPageComponent },
  {
    path: 'page-details/:id',
    component: VehicleDetailComponent,
  },
  {
    path: 'email', component: EmailComponent, canActivate: [AuthGuard]
  },
  {
    path: 'chat', component: ChatComponent, canActivate: [AuthGuard]
  },
  {
    path: 'user-page', component: UserPageComponent, canActivate: [AuthGuard]
  },
  { path: '', redirectTo: '/main-page', pathMatch: 'full' }
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
