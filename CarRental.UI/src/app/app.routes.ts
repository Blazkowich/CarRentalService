import { NgModule } from '@angular/core';
import { RouterModule, Routes, CanActivate } from '@angular/router';
import { LoginComponent } from './user/login/login.component';
import { RegisterComponent } from './user/register/register.component';
import { MainPageComponent } from './main-page/main-page.component';
import { VehicleDetailComponent } from './main-page/page-details/page-details.component';
import { EmailComponent } from './support-pages/support-email/support-email.component';
import { UserChatComponent } from './support-pages/support-chat/userChat/user-support-chat.component';
import { AuthGuard } from './auth-Guard/auth.guard';
import { HistoryRentingPageComponent } from './user/user-page/history-page/history-page.component';
import { ActiveRentingPageComponent } from './user/user-page/active-page/active-page.component';
import { AdminPageComponent } from './user/admin-page/admin-page.component';
import { AdminGuard } from './auth-Guard/admin.guard';
import { MainGuard } from './auth-Guard/main.guard';
import { LoginRegisterGuard } from './auth-Guard/login-register.guard';
import { AdminChatComponent } from './support-pages/support-chat/adminChat/admin-support-chat.component';
import { AddVehiclePageComponent } from './user/admin-page/add-page/add-vehicle.component';
import { EditVehiclePageComponent } from './user/admin-page/edit-page/edit-vehicle.component';
import { BookingComponent } from './booking/booking.component';

export const routes: Routes = [
  {
    path: 'main-page',
    component: MainPageComponent
  },
  {
    path: 'page-details/:id',
    component: VehicleDetailComponent,
  },
  {
    path: 'login',
    component: LoginComponent,
    canActivate: [LoginRegisterGuard]
  },
  {
    path: 'register',
    component: RegisterComponent,
    canActivate: [LoginRegisterGuard]
  },

  {
    path: 'email',
    component: EmailComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'user-chat',
    component: UserChatComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'admin-chat',
    component: AdminChatComponent,
    canActivate: [AdminGuard]
  },
  {
    path: 'admin-page',
    component: AdminPageComponent,
    canActivate: [AdminGuard]
  },
  {
    path: 'history',
    component: HistoryRentingPageComponent,
    canActivate: [MainGuard]
  },
  {
    path: 'active',
    component: ActiveRentingPageComponent,
    canActivate: [MainGuard]
  },
  {
    path: 'add-vehicle',
    component: AddVehiclePageComponent,
    canActivate: [AdminGuard]
  },
  {
    path: 'edit-vehicle/:id',
    component: EditVehiclePageComponent,
    canActivate: [AdminGuard]
  },
  {
    path: 'booking/:id',
    component: BookingComponent,
    canActivate: [AuthGuard]
  },
  { path: '', redirectTo: '/main-page', pathMatch: 'full' }
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
