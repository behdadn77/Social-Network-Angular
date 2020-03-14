import { Component, Input , Inject} from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Message } from '@angular/compiler/src/i18n/i18n_ast';



@Component({
  selector: 'app-pass-recovery',
  templateUrl: './pass-recovery.component.html',
  styleUrls: ['./pass-recovery.component.css']
})
export class PassRecoveryComponent {
  //recoverFormValidators: FormGroup;
  //baseUrl: string;
  //http: HttpClient;
  //constructor(_http: HttpClient, @Inject('BASE_URL') _baseUrl: string, private fb: FormBuilder) {
  //  this.baseUrl = _baseUrl;
  //  this.http = _http;
  //  this.recoverFormValidators =
  //    this.fb.group({
  //      email: ['', [Validators.pattern(/^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$/),
  //      Validators.required]],
  //    });
  //}
}
