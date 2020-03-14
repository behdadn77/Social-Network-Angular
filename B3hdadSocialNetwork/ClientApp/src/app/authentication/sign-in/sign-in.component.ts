import { Component, Input , Inject} from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Message } from '@angular/compiler/src/i18n/i18n_ast';



@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent {
  credentials: SignInCredentials=new SignInCredentials();
  signInFormValidators: FormGroup;
  baseUrl: string;
  http: HttpClient;
  constructor(_http: HttpClient, @Inject('BASE_URL') _baseUrl: string, private fb: FormBuilder) {
    this.baseUrl = _baseUrl;
    this.http = _http;
    this.signInFormValidators =
      this.fb.group({
        email: ['', [Validators.pattern(/^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$/),
        Validators.required]],
        password: ['',[Validators.required]],
      });
  }
  signIn(event) {
    event.target.disabled=true;
    event.target.html="Signing in...";
    this.http.post<JSON>(this.baseUrl + 'Api/Authentication/Signin', this.credentials).subscribe(
      result => {
        event.target.disabled = false;
        event.target.html = "Sign in";
        if (result != null) {
          var res = JSON.stringify(result);
          if ( JSON.parse(res).url!= null) {
            window.location.href = JSON.parse(res).url;
          } else {
            alert(result);
          }
        } else {
          alert('Ops! Somthing went wrong!');
        }
      },
      error => {
        alert('Ops! Somthing went wrong!');
        console.error(`Error: ${JSON.stringify(error)}`);
      }
    )
  }
}
class SignInCredentials {
  email: string;
  password: string;
}
