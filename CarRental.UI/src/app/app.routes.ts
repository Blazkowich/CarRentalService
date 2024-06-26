import { NgModule } from '@angular/core';
import { RouterModule, Routes, CanActivate } from '@angular/router';
import { LoginComponent } from './user/login/login.component';
import { RegisterComponent } from './user/register/register.component';
import { MainPageComponent } from './main-page/main-page.component';
import { VehicleDetailComponent } from './main-page/page-details/page-details.component';
import { EmailComponent } from './support-pages/support-email/support-email.component';
import { ChatComponent } from './support-pages/support-chat/support-chat.component';
import { UserPageComponent } from './user/user-page/user-page.component';
import { AuthGuard } from './authGuard/auth.guard';
import { HistoryRentingPageComponent } from './user/user-page/history-page/history-page.component';
import { ActiveRentingPageComponent } from './user/user-page/active-page/active-page.component';
import { AdminPageComponent } from './user/admin-page/admin-page.component';
import { AdminGuard } from './authGuard/admin.guard';
import { MainGuard } from './authGuard/main.guard';
import { LoginRegisterGuard } from './authGuard/login-register.guard';

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
    path: 'chat',
    component: ChatComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'user-page',
    component: UserPageComponent,
    canActivate: [AuthGuard]
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
    path: 'admin-chat',
    component: AdminPageComponent,
    canActivate: [AdminGuard]
  },
  { path: '', redirectTo: '/main-page', pathMatch: 'full' }
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
