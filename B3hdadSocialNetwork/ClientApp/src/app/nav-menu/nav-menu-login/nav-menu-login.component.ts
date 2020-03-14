import { Component, Inject, Input } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { map, retry } from 'rxjs/operators';
import { AuthenticationService } from 'src/app/authentication.service';

@Component({
  selector: 'app-nav-menu-login',
  templateUrl: './nav-menu-login.component.html',
  styleUrls: ['./nav-menu-login.component.css']
})
export class NavMenuLoginComponent {
  isSignedIn$: Observable<boolean> = new BehaviorSubject<boolean>(false).asObservable();
  userName: string = "My Profile";
  constructor(http: HttpClient, @Inject('BASE_URL') url: string, authService: AuthenticationService) {
    this.isSignedIn$ = authService.isSignedIn();
    http.get(url + 'api/Authentication/UserInfo').subscribe(
      result => {
        if (result != null) {
          this.userName = JSON.parse(JSON.stringify(result)).name;
        }
      },
      error => {
        console.error(`Error: ${JSON.stringify(error)}`)
      }
    )
  }
}
