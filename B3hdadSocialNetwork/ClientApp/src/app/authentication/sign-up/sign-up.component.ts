import { Component, Input , Inject} from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Message } from '@angular/compiler/src/i18n/i18n_ast';



@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent {
  credentials: SignUpCredentials=new SignUpCredentials();
  signUpFormValidators: FormGroup;
  baseUrl: string;
  http: HttpClient;
  constructor(_http: HttpClient, @Inject('BASE_URL') _baseUrl: string, private fb: FormBuilder) {
    this.baseUrl = _baseUrl;
    this.http = _http;
    this.signUpFormValidators =
      this.fb.group({
        email: ['', [Validators.pattern(/^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$/),
        Validators.required]],
        name: ['', [Validators.required]],
        family: ['', [Validators.required]],
        password: ['', [Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W)[a-zA-Z0-9\S]{9,}$/),
        Validators.required]],
      });
  }
  signUp(event) {
    event.target.disabled=true;
    event.target.html="Signing up...";
    this.http.post(this.baseUrl + 'Api/Authentication/Signup', this.credentials).subscribe(
      result => {
        event.target.html = "Sign up";
        event.target.disabled = false;
        alert(JSON.stringify(result));
        this.credentials = new SignUpCredentials();
      },
      error => {
        alert('Ops! Somthing went wrong!');
        console.log(`Error: ${JSON.stringify(error)}`)
      }
    )
  }
}
class SignUpCredentials {
  name: string;
  family: string;
  email: string;
  password: string;

}
