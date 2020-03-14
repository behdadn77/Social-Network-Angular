import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, observable, BehaviorSubject } from 'rxjs';
import { map, retry } from 'rxjs/operators';

@Injectable()
export class AuthenticationService{
  constructor(private http: HttpClient, @Inject('BASE_URL') private url: string) {
  }
  isSignedIn():Observable<boolean> {
  return this.http.get<boolean>(this.url + 'api/Authentication/IsAuthenticated');
  }
}
