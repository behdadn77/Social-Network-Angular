import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';


import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { NavMenuLoginComponent } from './nav-menu/nav-menu-login/nav-menu-login.component';
import { HomeComponent } from './home/home.component';
import { PostComponent } from './post/post.component';
import { SignUpComponent } from './authentication/sign-up/sign-up.component';
import { SignInComponent } from './authentication/sign-in/sign-in.component';
import { PassRecoveryComponent } from './authentication/pass-recovery/pass-recovery.component';
import { ReactiveFormsModule } from '@angular/forms';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { AuthenticationGuardService } from './authentication-guard.service';
import { StatusComponent } from './update-status/update-status.component';
import { AuthenticationService } from './authentication.service';
import { NotificationsComponent } from './notifications/notifications.component';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    NavMenuLoginComponent,
    HomeComponent,
    PostComponent,
    SignUpComponent,
    SignInComponent,
    PassRecoveryComponent,
    PageNotFoundComponent,
    UserProfileComponent,
    StatusComponent,
    NotificationsComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    InfiniteScrollModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      //{ path: '', redirectTo:'home',pathMatch:'full'},
      { path: '', component: HomeComponent },
      { path: 'home/:para', component: HomeComponent },
      { path: '404', component: PageNotFoundComponent },
      { path: 'sign-up', component: SignUpComponent },
      { path: 'sign-in', component: SignInComponent },
      { path: 'recover-my-account', component: PassRecoveryComponent },
      { path: 'my-profile', component: UserProfileComponent, canActivate: [AuthenticationGuardService]},
      { path: 'my-notifications', component: NotificationsComponent, canActivate: [AuthenticationGuardService]},
    ])
  ],
  providers: [AuthenticationGuardService, AuthenticationService],
  bootstrap: [AppComponent]
})
export class AppModule { }
